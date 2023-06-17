using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : Gun
{
    [SerializeField]
    float
        range = 100f,
        impactForce = 1f;
    [SerializeField]
    GameObject
        explosion;
    [SerializeField]
    GameObject
        trail;

    protected override void ShotFired()
    {
        if(Physics.Raycast(muzzel.position, muzzel.forward, out RaycastHit hit, range,Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if(trail != null)
            {
                SpawnBulletTrail(hit.point, muzzel.position);
            }
            if(explosion != null)
            {
                Instantiate(explosion, hit.point, Quaternion.identity);
            }
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce(muzzel.forward * impactForce, ForceMode.Impulse);
            }
        }
        else
        {
            if (trail != null)
            {
                SpawnBulletTrail(muzzel.position + muzzel.forward * range, muzzel.position);
            }
        }
    }

    private void SpawnBulletTrail(Vector3 hitPoint, Vector3 attackPoint)
    {
        if(trail.GetComponent<LineRenderer>() == null)
        { return; }
        GameObject bulletTrailEffect = Instantiate(trail, attackPoint, Quaternion.identity);

        LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
        lineR.SetPosition(0, attackPoint);
        lineR.SetPosition(1, hitPoint);
        Destroy(bulletTrailEffect, 10f);


    }
}
