using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    List<GameObject> bulletPool;
    [SerializeField] Transform muzzle;
    [SerializeField] float attacksPerSec = 5f;
    [SerializeField] float shotForce = 30f;

    [SerializeField] AudioSource shotSound;
    bool canShoot = true;
    bool isShooting = false;
    bool isAiming = false;
    Animator anim;


    [SerializeField] float lineRange = 10f;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            b.SetActive(false);
            bulletPool.Add(b);
        }
        anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        isShooting = Input.GetKey(KeyCode.Mouse0);
        isAiming = Input.GetKey(KeyCode.Mouse1);
    }
    private void FixedUpdate()
    {
        if (isShooting && canShoot)
        {
            //Camera shake
            Camera.main.transform.parent.BroadcastMessage("ShakeCamera", SendMessageOptions.DontRequireReceiver);

            //Sounds
            shotSound.pitch = 1 + Random.Range(-.1f, .1f);
            shotSound.Play();
            
            Shoot();

            canShoot = false;
            Invoke("ResetShot", 1 / attacksPerSec);
        }

        //Visuals
        if (isAiming || isShooting)
        {
            anim.SetBool("isShooting", true);
            line.enabled = true;
            DrawLine();
        }
        else
        {
            anim.SetBool("isShooting", false);
            line.enabled = false;
        }
        
    }


    void Shoot()
    {
        //Gets the bullet from the front of the queue
        GameObject bulletToUse = null;
        foreach(GameObject b in bulletPool)
        {
            if (!b.activeInHierarchy)
            {
                bulletToUse = b;
            }
        }
        
        //Resets the bullet
        bulletToUse.SetActive(true);
        bulletToUse.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        bulletToUse.transform.position = muzzle.position;
        bulletToUse.transform.rotation = muzzle.rotation;
        Physics.SyncTransforms();

        //Fires the bullet
        bulletToUse.GetComponent<Rigidbody>().AddForce(muzzle.forward * shotForce, ForceMode.Impulse);
    }
    void DrawLine()
    {
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, lineRange, 1, QueryTriggerInteraction.Ignore))
        {
            line.SetPosition(0, muzzle.position);
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(0, muzzle.position);
            line.SetPosition(1, muzzle.position + muzzle.forward * lineRange);
        }
    }


    void ResetShot()
    {
        canShoot = true;
        if (!isShooting)
        {
            
            anim.SetBool("isShooting", false);
        }
    }
}
