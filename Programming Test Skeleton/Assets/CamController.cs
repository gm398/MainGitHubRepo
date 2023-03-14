using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.1f;
    Transform cam;

    Vector3 camShakePos = new Vector3(0, 0, 0);
    void Start()
    {
        cam = Camera.main.transform;
    }
    void FixedUpdate()
    {
        cam.localPosition = camShakePos;
    }
    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }
    
    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            //shakeOffset.y = 0;
            camShakePos = shakeOffset;

            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        camShakePos = new Vector3(0, 0, 0);
    }
}
