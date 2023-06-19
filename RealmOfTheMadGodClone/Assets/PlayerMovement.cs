using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float
        maxSpeed = 10f,
        groundDrag = 1f,
        accelaration = 10f;

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
            camHolder = GameObject.FindGameObjectWithTag("CamHolder").transform;
        }
    }

    private void FixedUpdate()
    {
        Vector3 lookAt = MouseAim.aimingAt;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    
        
        if (!rolling)
        {
            
            Vector3 direction = InputController.xAxis * camHolder.right + InputController.zAxis * camHolder.forward;
            direction = direction.normalized;
            rb.AddForce(direction * accelaration * 100 * Time.fixedDeltaTime);
            

            if (Input.GetKey(KeyCode.Space) && rb.velocity.magnitude > .5f)
            {
                rolling = true;
                rollDirection = rb.velocity.normalized * 5000;
                Invoke("StopRoll", 1);
                
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



  
}
