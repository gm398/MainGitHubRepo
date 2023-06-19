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
        b.transform.localScale = transform.localScale;
        Rigidbody rb = b.GetComponent<Rigidbody>();

        Vector3 playerVel = Vector3.zero;
        Rigidbody charRB = GetComponentInParent<Rigidbody>();
        if(charRB != null)
        {
            playerVel += charRB.velocity;
        }

        Vector3 spread = 
            muzzel.right * 
            Random.Range(
                -(spreadRange + playerVel.magnitude / movementEffect), 
                spreadRange + playerVel.magnitude / movementEffect);

        rb.AddForce((muzzel.forward + spread).normalized * shotForce, ForceMode.Impulse);


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
