using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    float
        maxThrust;


    Rigidbody rb;
    bool
        forwardInput,
        backInput,
        rollLeftInput,
        rollRightInput,
        turnLeftInput,
        turnRghtInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetKey(KeyCode.W);
        backInput = Input.GetKey(KeyCode.S);
        rollLeftInput = Input.GetKey(KeyCode.A);
        rollRightInput = Input.GetKey(KeyCode.D);
        turnLeftInput = Input.GetKey(KeyCode.Q);
        turnRghtInput = Input.GetKey(KeyCode.E);
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
    }

    void AddThrust()
    {
        if (forwardInput)
        { }
    }
}
