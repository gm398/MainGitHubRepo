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
        maxAimOffset = 3f,
        scrollSpeed = 1;


    Camera cam;
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if(follow == null)
        {
            follow = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        


        Vector3 target = 
            follow.position + 
            Vector3.ClampMagnitude(MouseAim.aimingAt - follow.position, maxAimOffset);
        transform.position =
            Vector3.Lerp(
                transform.position,
                target,
                followSpeed * Time.fixedDeltaTime);

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
    private void Update()
    {
        if(cam.orthographicSize - InputController.mouseScroll * scrollSpeed > 0)
        {
            cam.orthographicSize -= InputController.mouseScroll * scrollSpeed;
        }
        
    }

   
    

   
}
