using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float 
        maxSpeed = 10f,
        accelaration = 100f,
        groundDrag = 2f,
        airDrag = .5f;

    [SerializeField]
    Transform camHolder;


    Rigidbody rb;

    MovementState state;
    enum MovementState
    {
        Ground,
        Air,
        Roll
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case MovementState.Ground:
                break;
            case MovementState.Air:
                break;
            case MovementState.Roll:
                break;
            default:
                break;
        }
        LimitSpeed();
    }

    private void FixedUpdate()
    {
        Vector3 direction = 
            camHolder.forward * InputController.zAxis + 
            camHolder.right * InputController.xAxis;

        rb.AddForce(direction * accelaration * Time.deltaTime);
    }


    void LimitSpeed()
    {
        Vector2 speed = new Vector2(rb.velocity.x, rb.velocity.z);
        speed = Vector2.ClampMagnitude(speed, maxSpeed);
        rb.velocity = new Vector3(speed.x, rb.velocity.y, speed.y);
    }



    void SwitchToGround()
    {
        state = MovementState.Ground;
        rb.drag = groundDrag;
    }
    void SwitchToAir()
    {
        state = MovementState.Air;
        rb.drag = airDrag;
    }
    void SwitchToRoll()
    {
        state = MovementState.Roll;
    }
}
