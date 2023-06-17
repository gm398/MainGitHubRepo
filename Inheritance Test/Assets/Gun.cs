using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected float AttacksPerSecond = 5;
    [SerializeField]
    protected Transform Muzzle;
    protected bool canShoot = true;

    
    protected virtual void Fire()
    {

    }
    public void Shoot()
    {
        if (canShoot)
        {
            Fire();
            canShoot = false;
            Invoke("ResetAttack", 1 / AttacksPerSecond);
        }
    }

    void ResetAttack()
    {
        canShoot = true;
    }
}
