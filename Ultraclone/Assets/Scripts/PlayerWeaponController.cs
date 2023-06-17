using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField]
    List<Gun> 
        weapons;

    int 
        currentWeaponIndex;

    bool
        canShoot = true,
        isShooting = false,
        isAiming = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Gun g in GetComponentsInChildren<Gun>())
        {
            weapons.Add(g);
        }
        currentWeaponIndex = -1;
        NextWeapon();
    }
    private void FixedUpdate()
    {
        if (isShooting && isAiming)
        {
            if(Physics.Raycast(
                Camera.main.transform.GetChild(0).position, 
                Camera.main.transform.forward,
                out RaycastHit hit,
                100,
                Physics.AllLayers))
                //QueryTriggerInteraction.Ignore))
            {
                Debug.Log(hit.transform.name);
                weapons[currentWeaponIndex].Shoot(hit.transform);
                
            }
            else
            {
                weapons[currentWeaponIndex].Shoot(
                    Camera.main.transform.position + 
                    Camera.main.transform.forward * 10);
            }
             
        }
        else if(isShooting)
        {
            weapons[currentWeaponIndex].Shoot();
        }
    }
    // Update is called once per frame
    void Update()
    {
        isShooting = false;
        isAiming = false;
        if (InputController.secondaryFire)
        {
            isAiming = true;
        }
        if (InputController.primaryFire && currentWeaponIndex >= 0 && canShoot)
        {
            isShooting = true;
        }
        if (InputController.switchWeapon)
        {
            NextWeapon();
        }
    }

    void NextWeapon()
    {
        weapons.RemoveAll(item => item == null);
        weapons.RemoveAll(item => item.GetComponent<Gun>() == null);
        if(weapons.Count <= 0)
        { return; }
        currentWeaponIndex++;
        if(currentWeaponIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }
        foreach(Gun w in weapons)
        {
            w.gameObject.SetActive(false);
        }
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
