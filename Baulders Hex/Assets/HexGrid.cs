using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] List<HexTile> hexs;
    List<HexTile> visibleHexs;
    Dictionary<Vector3, HexTile> hexDictionary = new Dictionary<Vector3, HexTile>();
    [SerializeField] GameObject[] mapPieces;
    

    
    // Start is called before the first frame update
    private void Awake()
    {
        hexDictionary = new Dictionary<Vector3, HexTile>();
        GetHexesFromGameobject();
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
            if(hexDictionary.TryGetValue(hexagon.QRSCoordinates, out var hexTile)) neighbours.Add(hexTile);
        }
        return neighbours; 
    }
    public List<HexTile> GetNeighbours(Hexagon startHex)
    {
        return GetNeighbours(hexDictionary[startHex.QRSCoordinates]); 
    }

    public bool GetHex(Vector3 RQS, out HexTile hex)
    {
        return hexDictionary.TryGetValue(RQS, out hex);
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
                if (hexDictionary.TryAdd(hex.Hexagon.QRSCoordinates, hex))
                {
                    hex.SetWorldPosision();
                }
            }
        }
    }


    //takes hexs out of a parent, usually a map prefab and adds them to the hex list
    void GetHexesFromGameobject()
    {
        foreach (GameObject piece in mapPieces)
        {
            if (piece != null)
            {
                HexTile[] parts = piece.GetComponentsInChildren<HexTile>();
                foreach (HexTile part in parts)
                {
                    hexs.Add(part);
                    part.transform.SetParent(this.transform);
                    
                }
            }
        }
    }

    public Dictionary<Vector3, HexTile> GetHexDictionary()
    {
        return hexDictionary;
    }

}
   
