using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float
        maxSpeed = 10f,
        rotationSpeed = 1f,//time to turn 360 in seconds
        groundDrag = 1f,
        accelaration = 10f,
        rollForce = 500f,
        rollDuration = .3f;

    [SerializeField]
    Transform
        camHolder;
    Rigidbody rb;


    bool rolling = false;
    Vector3 rollDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(camHolder == null)
        {
            camHolder = GameObject.FindGameObjectWithTag("CamHolder")?.transform ?? null; ;
        }
    }

    private void FixedUpdate()
    {
        RotateTowardsMouse();
    
        
        if (!rolling)
        {
            
            Vector3 direction = InputController.xAxis * (camHolder?.right ?? Vector3.right) + InputController.zAxis * (camHolder?.forward ?? Vector3.forward);
            direction = direction.normalized;
            rb.AddForce(direction * accelaration * 100 * Time.fixedDeltaTime);
            

            if (Input.GetKey(KeyCode.Space) && rb.velocity.magnitude > .5f)
            {
                rolling = true;
                rollDirection = rb.velocity.normalized * rollForce;
                Invoke("StopRoll", rollDuration);
                
            }
        }
        else
        {
            rb.AddForce(rollDirection * Time.fixedDeltaTime);
            
        }
    }
    private void Update()
    {
        if (!rolling)
        {
            LimitVelocity();
        }
    }
    void StopRoll()
    {
        rolling = false;
    }
    void LimitVelocity()
    {
        Vector2 speed = new Vector2(rb.velocity.x, rb.velocity.z);
        speed = Vector2.ClampMagnitude(speed, maxSpeed);
        rb.velocity = new Vector3(speed.x, rb.velocity.y, speed.y);
    }

    void RotateTowardsMouse()
    {
        Vector3 lookAt = MouseAim.aimingAt - transform.position;
        lookAt.y = transform.position.y;
        

        Quaternion targetRotation = Quaternion.LookRotation(lookAt);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (360 / rotationSpeed) * Time.fixedDeltaTime);
    }
}
