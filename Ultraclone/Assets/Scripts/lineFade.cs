using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineFade : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float speed = 10f;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * speed);
        lr.startColor = color;
        lr.endColor = color;
       if(color.a < 0.01)
        {
            Destroy(this.gameObject);
        }
    }
}
