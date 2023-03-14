using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    public bool
        canGoNE,
        canGoE,
        canGoSE,
        canGoSW,
        canGoW,
        canGoNW;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //can this tile connects to the given tile
    bool CanConnect(int q, int r)
    {
        bool canConnect = false;

        if(q == 1 && r == -1)
        { canConnect = canGoNE; }
        if (q == 1 && r == 0)
        { canConnect = canGoE; }
        if (q == 0 && r == 1)
        { canConnect = canGoSE; }
        if (q == -1 && r == 1)
        { canConnect = canGoSW; }
        if (q == -1 && r == 0)
        { canConnect = canGoW; }
        if (q == 0 && r == -1)
        { canConnect = canGoNW; }
        
        return canConnect;
    }
}
