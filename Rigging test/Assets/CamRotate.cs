using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] float sensitivity = 500f;
    [SerializeField] Transform holder;

    float xRot;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");

        xRot -= yInput * Time.deltaTime * sensitivity;
        xRot = Mathf.Clamp(xRot, -90, 90);

        transform.Rotate(0, xInput * Time.deltaTime * sensitivity, 0);
        holder.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
