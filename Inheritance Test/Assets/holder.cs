using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder : MonoBehaviour
{
    public MonoBehaviour[] guns;
    
    // Start is called before the first frame update
    void Start()
    {
        guns = GetComponents<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
