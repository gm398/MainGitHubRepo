using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    [SerializeField] List<Tile> tiles;

    Dictionary<Vector2, Tile> map;
    // Start is called before the first frame update
    void Start()
    {
        map = new Dictionary<Vector2, Tile>();
        map.Add(new Vector2(0, 0),tiles[Random.Range(0, tiles.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void CreateMap()
    {

    }
}
