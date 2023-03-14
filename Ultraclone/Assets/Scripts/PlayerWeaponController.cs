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
        isShooting = false;
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
        if (isShooting)
        {
            weapons[currentWeaponIndex].Shoot();
        }
    }
    // Update is called once per frame
    void Update()
    {
        isShooting = false;
        if (Input.GetKey(KeyCode.Mouse0) && currentWeaponIndex >= 0 && canShoot)
        {
            isShooting = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
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
