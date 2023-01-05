using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float
        attacksPerSecond;
    [SerializeField]
    Transform
        muzzel;
    float
        timeOfNextShot = 0;

   
    public void Shoot()
    {
        if (timeOfNextShot < Time.time)
        {
            timeOfNextShot = Time.time + 1 / attacksPerSecond;
            BroadcastMessage("ShotFired", muzzel, SendMessageOptions.DontRequireReceiver);
        }
    }

    public float GetAttacksPerSecond() { return attacksPerSecond; }
}
