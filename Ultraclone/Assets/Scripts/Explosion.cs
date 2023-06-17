using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    float
        radius = 1,
        force = 20,
        damage;
    private void Awake()
    {
        foreach(Collider g in Physics.OverlapSphere(transform.position, radius, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();
            if(rb != null)
            {
                float distance = Vector3.Distance(rb.transform.position, transform.position);
                rb.AddForce((rb.transform.position - transform.position).normalized * force / distance, ForceMode.Impulse);
            }
            Health health = g.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }
        
    }
   
}
