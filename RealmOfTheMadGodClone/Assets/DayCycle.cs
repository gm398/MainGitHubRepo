using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{

    Material sky;
    float offset = 0;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        sky = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        sky.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
