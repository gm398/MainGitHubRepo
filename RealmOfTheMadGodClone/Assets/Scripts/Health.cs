using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{


    [SerializeField]
    float 
        maxHealth = 100f,
        armor = 0;

    float
        currentHealth;


    bool canTakeDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    //
    // Summary:
    //     Reduces currentHealth by damage - armor

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            return;
        }
        damage -= armor;
        if(damage <= 0)
        {
            damage = 0;
        }
        currentHealth -= damage;
        CheckHealth();
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        CheckHealth();
       
    }

    void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //
    // Summary:
    //     calls the Dead method on attached scripts
    public void Die()
    {
        BroadcastMessage("Dead", null, SendMessageOptions.DontRequireReceiver);
    }

    float GetCurrentHealth()
    {
        return currentHealth;
    }
    //
    // Summary:
    //     Sets the currentHealth and optionally changes the maxHealth

    void SetCurrentHealth(float health, bool changeMaxHealth)
    {
        currentHealth = health;
        if (changeMaxHealth)
        {
            maxHealth = health;
        }
    }
    

    public void CanTakeDamage(bool setCanTakeDamage)
    {
        canTakeDamage = setCanTakeDamage;
    }
    
    public bool CanTakeDamage()
    {
        return canTakeDamage;
    }
}
