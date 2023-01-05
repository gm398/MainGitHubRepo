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
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(this.transform.position + rb.velocity);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if(particles != null)
        {
            particles.transform.parent = null;
            particles.GetComponent<ParticleSystem>().Stop();
            Destroy(particles, 2f);
        }
        Destroy(this.gameObject);
    }
}
