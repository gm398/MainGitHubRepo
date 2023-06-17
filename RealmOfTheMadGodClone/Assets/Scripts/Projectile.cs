using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float
        lifeTime = 5;

    [SerializeField]
    GameObject
        explosion,
        particles;

    Rigidbody
        rb;

    [SerializeField]
    bool guided = false;
    [SerializeField]
    float guidenceForce = 10f;
    Transform target;
    Vector3 targetPoint = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
        CheckCollisions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (guided)
        {
            GoToTarget();
        }
        CheckCollisions();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        { return; }
        Collide(transform.position);
    }

    public void CheckCollisions()
    {
        transform.LookAt(this.transform.position + rb.velocity);
        if (Physics.Raycast(
            transform.position, 
            rb.velocity, 
            out RaycastHit hit, 
            rb.velocity.magnitude * Time.fixedDeltaTime, 
            Physics.AllLayers, 
            QueryTriggerInteraction.Ignore))
        {
            Collide(hit.point);
        }
    }
    void Collide(Vector3 point)
    {
        if (explosion != null)
        {

            Instantiate(explosion, point, transform.rotation);
        }
        if (particles != null)
        {
            particles.transform.parent = null;
            particles.GetComponent<ParticleSystem>().Stop();
            Destroy(particles, 2f);
        }
        Destroy(this.gameObject);
    }

    void GoToTarget()
    {
        if(target != null)
        { targetPoint = target.position; }
        else if (targetPoint.Equals(Vector3.zero))
        {
            return;
        }
        rb.AddForce((-transform.position + targetPoint).normalized * guidenceForce);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public void SetTarget(Vector3 targetPoint)
    {
        this.targetPoint = targetPoint;
    }

    

}
