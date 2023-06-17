using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    Transform follow;

    [SerializeField]
    float 
        rotationSpeed = 90f,
        followSpeed = 5f,
        maxAimOffset = 3f;

    private void Start()
    {
        if(follow == null)
        {
            follow = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookAt = 
            follow.position + 
            Vector3.ClampMagnitude(
                MouseAim.aimingAt - follow.position, 
                maxAimOffset) +
            follow.GetComponent<Rigidbody>().velocity;
        transform.position = 
            Vector3.Lerp(
                transform.position, 
                follow.position, 
                followSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        float direc = 0;
        if (Input.GetKey(KeyCode.E))
        {
            direc++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            direc--;
        }

        transform.Rotate(
            0,
            rotationSpeed * direc * Time.deltaTime,
            0);
    }
}
