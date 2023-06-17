using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] Transform facing;

    Rigidbody rb;

    float xInput = 0;
    float zInput = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        LimitVelocity();
    }


    private void FixedUpdate()
    {
        Vector3 direction = facing.forward * zInput + facing.right * xInput;
        rb.AddForce(direction.normalized * 10000 * Time.fixedDeltaTime);

        LimitVelocity();
    }



    void LimitVelocity()
    {
        Vector2 HVel = new Vector2(rb.velocity.x, rb.velocity.z);
        HVel = Vector2.ClampMagnitude(HVel, maxSpeed);
        rb.velocity = new Vector3(HVel.x, rb.velocity.y, HVel.y);
    }
}
