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
    [SerializeField]
    bool rotateClockwise = true;
    Gun gun;
    private void Start()
    {
        gun = GetComponent<Gun>();
        gun.onShoot += ShotFired;
    }
    // Update is called once per frame
    void Update()
    {
        if(timeOfNextShot > Time.time)
        {
            int direc = (rotateClockwise) ? -1 : 1;
            barrels.Rotate(0, 0, direc * 360 / numOfBarrels * shotsPerSec * Time.deltaTime);
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
        //Debug.Log("fired");
    }
}
