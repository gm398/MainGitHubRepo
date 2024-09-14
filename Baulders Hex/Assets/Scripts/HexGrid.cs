using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid
{
    List<HexTile> hexs;
    List<HexTile> visibleHexs;
    public Dictionary<Vector3, HexTile> HexDictionary = new Dictionary<Vector3, HexTile>();

    private static HexGrid _hexGridInstance;
    public static HexGrid HexGridInstance
    {
        get
        {
            if (_hexGridInstance == null)
            {
                _hexGridInstance = new HexGrid();
            }

            return _hexGridInstance;
        }
    }

    public void InitializeGrid(List<GameObject> mapPieces)
    {
        HexDictionary = new Dictionary<Vector3, HexTile>();
        GetHexesFromGameobject(mapPieces);
        AddHexsToDictionary();
    }
   
    [ContextMenu("Test")]
    public void TestPrint()
    {
        Debug.Log("hello world");
    }

    public List<HexTile> GetNeighbours(HexTile startHex)
    {
        List<HexTile> neighbours = new List<HexTile>();
        foreach (Hexagon hexagon in startHex.Hexagon.NeighborCoordiates())
        {
            if(HexDictionary.TryGetValue(hexagon.QRSCoordinates, out var hexTile)) neighbours.Add(hexTile);
        }
        return neighbours; 
    }
    public List<HexTile> GetNeighbours(Hexagon startHex)
    {
        return GetNeighbours(HexDictionary[startHex.QRSCoordinates]); 
    }

    public bool GetHex(Vector3 RQS, out HexTile hex)
    {
        return HexDictionary.TryGetValue(RQS, out hex);
    }

    public List<HexTile> GetHexesInRange(int range, HexTile center)
    {
        List<HexTile> hexList = new List<HexTile>();
        Vector3 centerCoords = center.Hexagon.QRSCoordinates;
        for(int q = -range; q <= range; q++)
        {
            for (int r = -range; r <= range; r++)
            {
                for (int s = -range; s <= range; s++)
                {
                    if (q + r + s == 0)
                    {
                        
                        if (GetHex(new Vector3(q, r, s) + centerCoords, out HexTile addition))
                        {
                            hexList.Add(addition);
                            //Debug.Log("hex added");
                        }
                    }
                }
                
            }
        }
        return hexList;
    }
    
    
    //adds each hex to the dictionary and moves them into the correct positions
    void AddHexsToDictionary()
    {
        foreach (HexTile hex in hexs)
        {
            if (hex != null)
            {
                hex.Hexagon = new Hexagon(hex.transform.position, true);
                //hex.GetHexCoordinates().MoveToGridCords();
                if (HexDictionary.TryAdd(hex.Hexagon.QRSCoordinates, hex))
                {
                    hex.SetWorldPosision();
                }
            }
        }
    }


    //takes hexs out of a parent, usually a map prefab and adds them to the hex list
    public void GetHexesFromGameobject(List<GameObject> mapPieces)
    {
        foreach (GameObject piece in mapPieces)
        {
            if (piece != null)
            {
                HexTile[] parts = piece.GetComponentsInChildren<HexTile>();
                foreach (HexTile part in parts)
                {
                    hexs.Add(part);
                }
            }
        }
    }

    public Dictionary<Vector3, HexTile> GetHexDictionary()
    {
        return HexDictionary;
    }

}
   
