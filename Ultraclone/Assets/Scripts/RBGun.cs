using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBGun : MonoBehaviour
{

    [SerializeField]
    float
        shotForce;
    [SerializeField]
    GameObject
        projectile;
  
    
    public void ShotFired(Transform muz)
    {
        GameObject b = Instantiate(projectile, muz.position, muz.rotation);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.AddForce(muz.forward * shotForce, ForceMode.Impulse);
    }
}
