using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] float attacksPerSec = 5f;
    [SerializeField] float shotForce = 30f;

    bool canShoot = true;
    bool isShooting = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = Input.GetKey(KeyCode.Mouse0);
        if (isShooting && canShoot)
        {
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            b.GetComponent<Rigidbody>().AddForce(muzzle.forward * shotForce, ForceMode.Impulse);
            canShoot = false;
            anim.SetBool("isShooting", true);
            Invoke("ResetShot", 1 / attacksPerSec);
        }
    }



    void ResetShot()
    {
        canShoot = true;
        if (!isShooting)
        {
            
            anim.SetBool("isShooting", false);
        }
    }
}
