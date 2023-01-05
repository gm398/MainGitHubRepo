using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField]
    Transform
        barrels;
    [SerializeField]
    float
        numOfBarrels = 6;
    float
        timeOfNextShot = 0,
        shotsPerSec = 0;

    Gun gun;
    private void Start()
    {
        gun = GetComponent<Gun>();
    }
    // Update is called once per frame
    void Update()
    {
        if(timeOfNextShot > Time.time)
        {
            barrels.Rotate(0, 0, 360 / numOfBarrels * shotsPerSec * Time.deltaTime);
        }
        else
        {
            barrels.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void ShotFired()
    {
        this.shotsPerSec = gun.GetAttacksPerSecond();
        timeOfNextShot = Time.time + 1 / shotsPerSec;
        Debug.Log("fired");
    }
}
