using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    float maxHealth = 100;

    float currentHealth;

    public delegate void Death();
    public event Death Die;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Die += Des;
    }

    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            if (Die != null)
            {
                Die();
            }
        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
    }

    public void Des()
    {
        //Destroy(this.gameObject, .5f);
    }

}
