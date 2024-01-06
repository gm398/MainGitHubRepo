using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    
    [SerializeField]
    float
        lifeTime = 5;

    float
        damage = 5f;

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


    [SerializeField]
    bool hitTriggers = false;
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
        if (other.isTrigger && !hitTriggers)
        { return; }
        Collide(transform.position, other.gameObject);
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
            hitTriggers ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore))
        {
            Collide(hit.point, hit.transform.gameObject);
        }
    }
    void Collide(Vector3 point, GameObject objectHit)
    {
        if (explosion != null)
        {
            Instantiate(explosion, point, transform.rotation).transform.localScale = transform.localScale;
        }
        if (particles != null)
        {
            particles.transform.parent = null;
            particles.GetComponent<ParticleSystem>().Stop();
            Destroy(particles, 2f);
        }

        Health h = objectHit.GetComponent<Health>();
        if(h != null)
        {
            h.TakeDamage(damage);
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

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    

}
