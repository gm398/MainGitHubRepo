using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    float
        damage = 10,
        range = 2;

    [SerializeField]
    LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.primaryFire)
        {
            Collider[] hit = Physics.OverlapSphere(attackPoint.position, range, enemyLayers);
            foreach(Collider h in hit){
                h.BroadcastMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
