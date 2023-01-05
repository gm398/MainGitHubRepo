using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public List<GameObject> planets;
    public double G = .001;
    Rigidbody rb;
    public float startVelocity = 0;
    public Transform startingDirec;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Planet"))
        {
            planets.Add(g);
        }
        rb.velocity = (startingDirec.position - transform.position).normalized * startVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject g in planets)
        {
            if (!g.Equals(this.gameObject))
            {
                Grav(g);
                
            }
        }
    }

    void Grav(GameObject planet)
    {
        Rigidbody r = planet.GetComponent<Rigidbody>();
        double
            distance = Vector3.Distance(transform.position, planet.transform.position),
            m1 = rb.mass,
            m2 = r.mass;
            //G = 6.67 * Mathf.Pow(10, -11);
        Debug.Log("G: " + G);
        Vector3 direction = (planet.transform.position - transform.position).normalized;

        double force = G * ((m1 * m2) / (distance * distance));
        Debug.Log("force: " + (float)force);
        rb.AddForce(direction * (float)force);
    }
}
