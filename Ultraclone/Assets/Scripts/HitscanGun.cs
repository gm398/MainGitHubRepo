using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : MonoBehaviour
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

    public void ShotFired(Transform muz)
    {
        if(Physics.Raycast(muz.position, muz.forward, out RaycastHit hit, range))
        {
            if(trail != null)
            {
                SpawnBulletTrail(hit.point, muz.position);
            }
            if(explosion != null)
            {
                Instantiate(explosion, hit.point, Quaternion.identity);
            }
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce(muz.forward * impactForce, ForceMode.Impulse);
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
