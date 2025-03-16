using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float
        maxSpeed = 10f,
        accelaration = 10f,
        rollPower = 500f,
        rollDuration = .2f;

    PlayerController _playerController;
    PlayerController PlayerController
    {
        get
        {
            _playerController ??= GetComponent<PlayerController>();
            if (_playerController == null)
            {
                Debug.Log("PlayerController not attached");
            }
            return _playerController;
        }
    }

    [SerializeField]
    float groundDrag
    {
        get
        {
            return _groundDrag;
        }
        set
        {
            if(_groundDrag != value)
            {
                _groundDrag = value;
                groundDrag = value;
                rb.drag = value;
            }
        }
    }
    [SerializeField]
    private float _groundDrag;
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
        rb.drag = groundDrag;
    }

    private void Update()
    {
        float time = Time.deltaTime;
        Vector3 lookAt = MouseAim.aimingAt;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    
        
        if (!rolling)
        {
            
            Vector3 direction = InputController.xAxis * camHolder.right + InputController.zAxis * camHolder.forward;
            direction = direction.normalized;
            rb.AddForce(direction * accelaration * 100 * time);
            LimitVelocity();

            if (Input.GetKey(KeyCode.Space) && rb.velocity.magnitude > .5f)
            {
                rolling = true;
                rollDirection = rb.velocity.normalized * rollPower;
                rb.drag = 0;
                Invoke("StopRoll", rollDuration);
                
            }
        }
        else
        {
            rb.AddForce(rollDirection * time);
            
        }
        if (!rolling)
        {
            LimitVelocity();
        }
    }
    void StopRoll()
    {
        rolling = false;
        rb.drag = groundDrag;
    }
    void LimitVelocity()
    {
        Vector2 speed = new Vector2(rb.velocity.x, rb.velocity.z);
        speed = Vector2.ClampMagnitude(speed, maxSpeed);
        rb.velocity = new Vector3(speed.x, rb.velocity.y, speed.y);
    }
  
}
