using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float
        attacksPerSecond = 10,
        attacksPerShot = 1;
        
    [SerializeField]
    protected Transform
        muzzel;
    float
        timeOfNextShot = 0;
    public delegate void Shot();
    public event Shot onShoot;


    protected Transform target;
    protected Vector3 targetPoint;
    [SerializeField]
    [Range(0, 1)]
    protected float spreadRange = 0;
    [SerializeField]
    protected float movementEffect = 100;//lower number = higher effect
    private void Update()
    {
        if (InputController.primaryFire)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        if (timeOfNextShot < Time.time)
        {
            timeOfNextShot = Time.time + 1 / attacksPerSecond;

            for(int i = 0; i < attacksPerShot; i++)
            {
                ShotFired();
            }
            

            if(onShoot != null)
                onShoot();
        }
        target = null;
        targetPoint = Vector3.zero;
    }
    public void Shoot(Transform target)
    {
        this.target = target;
        Shoot();
    }
    public void Shoot(Vector3 targetPoint)
    {
        this.targetPoint = targetPoint;
        Shoot();
    }

    protected virtual void ShotFired()
    {

    }

    public float GetAttacksPerSecond() { return attacksPerSecond; }
}
