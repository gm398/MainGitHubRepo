using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : Hexagon
{
    #region Properties
    
    #endregion


}

public class game : MonoBehaviour
{
    public game()
    {
       
    }
    private void Start()
    {
        HexGrid = new List<HexTile>();

    }
    private List<HexTile> HexGrid;
    private void Update()
    {
        
    }
}
