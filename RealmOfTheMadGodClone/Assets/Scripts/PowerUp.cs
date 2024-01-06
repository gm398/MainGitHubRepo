using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    int
        extraShots;
    [SerializeField]
    float
        extraAttacksPerSec,
        extraDamage;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
        { return; }

        Gun[] guns = other.GetComponentsInChildren<Gun>();
        foreach(Gun g in guns)
        {
            g.AddAttackPerShot(extraShots);
            g.IncreaseAttacksPerSecond(extraAttacksPerSec);
            g.IncreaseDamage(extraDamage);
        }

        Destroy(this.gameObject);
    }

}
