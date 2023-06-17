using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBGun : Gun
{

    [SerializeField]
    float
        shotForce;
    [SerializeField]
    GameObject
        projectile;
  
    
    protected override void ShotFired()
    {
        GameObject b = Instantiate(projectile, muzzel.position, muzzel.rotation);
        
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.AddForce(muzzel.forward * shotForce, ForceMode.Impulse);


        Projectile p = b.GetComponent<Projectile>();
        if (p != null)
        {
            if (target != null)
            {
                p.SetTarget(target);
            }
            else
            {
                p.SetTarget(targetPoint);
            }
        }
        
    }
}
