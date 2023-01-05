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
        foreach(Collider g in Physics.OverlapSphere(transform.position, radius))
        {
            Rigidbody rb = g.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce((rb.transform.position - transform.position).normalized * force, ForceMode.Impulse);
            }
        }
    }
   
}
