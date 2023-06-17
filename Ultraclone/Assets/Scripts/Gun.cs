using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float
        attacksPerSecond = 10;
    [SerializeField]
    protected Transform
        muzzel;
    float
        timeOfNextShot = 0;
    public delegate void Shot();
    public event Shot onShoot;


    protected Transform target;
    protected Vector3 targetPoint;
    public void Shoot()
    {
        if (timeOfNextShot < Time.time)
        {
            timeOfNextShot = Time.time + 1 / attacksPerSecond;
            ShotFired();

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
