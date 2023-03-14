using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] AudioSource hitSound;
    [SerializeField] GameObject bloodSplatter;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsVisible())
        {
            this.gameObject.SetActive(false);
        }
    }

    bool IsVisible()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0);
    }

   
    private void OnTriggerEnter(Collider other)
    {
        InmateController inmate = other.gameObject.GetComponent<InmateController>();
        if (inmate != null)
        {
            GameObject blood = Instantiate(bloodSplatter, transform.position, transform.rotation);
            Destroy(blood, 3f);
            inmate.Die();
            this.gameObject.SetActive(false);
        }
    }
}
