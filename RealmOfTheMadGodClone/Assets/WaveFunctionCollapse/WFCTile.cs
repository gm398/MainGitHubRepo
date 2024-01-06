using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WFCTile : MonoBehaviour
{
     
    public WaveFunctionCollapse.TileType tileType;

    public List<WaveFunctionCollapse.TileType> GetPossibleTouching()
    {
        return WaveFunctionCollapse.GetPossibleTouching(tileType);
    }
    
    public void SetTileType(WaveFunctionCollapse.TileType t)
    {
        tileType = t;
        GetComponent<MeshRenderer>().material.color = WaveFunctionCollapse.GetColorFromTileType(t);
    }
}
