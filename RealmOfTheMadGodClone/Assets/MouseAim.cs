using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    static public Vector3 aimingAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindAimingAt();
    }

    void FindAimingAt()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance = 100;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            aimingAt = ray.GetPoint(distance);
        }
    }
}
