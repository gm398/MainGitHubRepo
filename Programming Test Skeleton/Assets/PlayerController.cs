using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float force = 5f;
    [SerializeField] float turningLerpSpeed = 10f;
    Rigidbody rb;

    float hInput, vInput;

    Vector3 aimingAt;

    bool isDead = false;
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
        if (isDead)
        { return; }

        FindAimingAt();

        //faces where the mouse is pointing to
        transform.LookAt(new Vector3(aimingAt.x, transform.position.y, aimingAt.z));

        //uses inputs to decide on a direction to move
        Vector3 direction = Vector3.forward * vInput + Vector3.right * hInput;
        rb.AddForce(direction.normalized * force * Time.fixedDeltaTime);
        LimitVelocity();
    }
    
    // Limits the velocity of the player along the X/Z plane
    void LimitVelocity()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, rb.velocity.z);
        flatVel = Vector2.ClampMagnitude(flatVel, maxSpeed);
        rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.y);
    }

    
    
    
    //Finds where the mouse is pointing to in world space
    void FindAimingAt()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 100;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            aimingAt = ray.GetPoint(distance);
        }
    }


    public Vector3 GetAimingAt()
    {
        return aimingAt;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            isDead = true;
           
            IKFootSolver[] IKLegs = GetComponentsInChildren<IKFootSolver>();
            foreach(IKFootSolver ik in IKLegs)
            {
                ik.gameObject.SetActive(false);
            }
            GetComponent<Animator>().SetBool("isDead", true);
            Camera.main.transform.parent.BroadcastMessage("ShakeCamera", SendMessageOptions.DontRequireReceiver);
        }
    }
}
