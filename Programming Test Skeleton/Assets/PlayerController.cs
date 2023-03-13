using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float force = 5f;

    Rigidbody rb;

    float hInput, vInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");


    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * vInput + transform.right * hInput;

        rb.AddForce(direction.normalized * force * Time.fixedDeltaTime);
        LimitVelocity();
    }



    void LimitVelocity()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, rb.velocity.z);
        flatVel = Vector2.ClampMagnitude(flatVel, maxSpeed);
        rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.y);
    }
}
