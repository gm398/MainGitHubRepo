using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public float Health { get; set; }
    public float Armour { get; set; }
    
    public void TakeDamage(float damage)
    {
        damage = damage / Armour;
        Health -= damage;
    }
    
}
