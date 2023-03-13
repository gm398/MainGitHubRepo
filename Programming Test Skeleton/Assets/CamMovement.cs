using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    [SerializeField] float sensitivity = 500;
    [SerializeField] Transform body;

    float xRotation;

    float xRot = 0, yRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        yRot = Input.GetAxis("Mouse X");
        xRot = Input.GetAxis("Mouse Y");
    }
    private void FixedUpdate()
    {
        xRotation -= xRot * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        this.transform.localEulerAngles = new Vector3(xRotation, 0, 0);

        body.Rotate(0, yRot * sensitivity * Time.deltaTime, 0);
    }
}
