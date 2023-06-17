using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : Gun
{
    [SerializeField]
    GameObject Projectile;
    [SerializeField]
    Transform barrels;
    [SerializeField]
    float numOfBarrels;

    public float shotForce = 10f;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }

        BarrelRotation();        
    }

    protected override void Fire()
    {
        GameObject bullet = Instantiate(Projectile, Muzzle.position, Muzzle.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(Muzzle.forward * shotForce, ForceMode.Impulse);
        }
        Destroy(bullet, 3f);
    }


    void BarrelRotation()
    {
        if (!canShoot && barrels != null)
        {
            barrels.Rotate(0, 0, (360 / numOfBarrels) * AttacksPerSecond * Time.deltaTime);
        }
        else if (barrels != null)
        {
            barrels.localRotation = Quaternion.identity;
        }
    }

   
}
