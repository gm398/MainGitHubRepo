using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttackController : MonoBehaviour
{
    public float Damage = 50f;
    [SerializeField]
    Collider SwordCollider;


    public void SwingHit(int damage)
    {
        Debug.Log("hit");
    }
}
