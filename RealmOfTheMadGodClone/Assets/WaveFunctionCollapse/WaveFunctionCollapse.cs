using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{


    [SerializeField]
    float width = 5, length = 5;
    [SerializeField]
    List<GameObject> tileSet;
    

    Dictionary<Vector2, GameObject> map = new Dictionary<Vector2, GameObject>();
    
    Dictionary<Vector2, List<TileType>> entropy = new Dictionary<Vector2, List<TileType>>();

    [SerializeField]
    List<TileType> entList = new List<TileType>();

    bool spawned = false;

    public enum TileType
    {
        DEEPWATER,
        WATER,
        BEACH,
        SAND,
        GRASS,
        FORREST,
        MOUNTAIN,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !spawned)
        {
            Debug.Log("spawning");
            SpawnMap();
            spawned = true;
        }
    }

    void PrepMap()
    {
        List<TileType> tiles = new List<TileType>();
        tiles.Add(TileType.DEEPWATER);
        tiles.Add(TileType.WATER);
        tiles.Add(TileType.BEACH);
        tiles.Add(TileType.GRASS);
        tiles.Add(TileType.FORREST);
        tiles.Add(TileType.MOUNTAIN);

        for (int x = 0; x < width; x++)
        {
            for(int z = 0; z < length; z++)
            {
                entropy.Add(new Vector2(x, z), tiles);
            }
        }
    }
    void BuildMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                Vector2 pos = ChoosePosition();
                if(pos.x == -1)
                {
                    return;
                }

                TileType tile = entropy[pos][Random.Range(0, entropy[pos].Count)];
                entropy[pos] = new List<TileType>();
                entropy[pos].Add(tile);
                UpdateEntropy(pos);
            }
        }
    }
    Vector2 ChoosePosition()
    {
        Vector2 newPos = new Vector2(-1, -1);
        List<Vector2> possibleNew = new List<Vector2>();
        foreach (Vector2 pos in entropy.Keys)
        {
            if (entropy[pos].Count <= 1)
            {
                continue;
            }
            if (newPos.x == -1)
            {
                newPos = pos;
                continue;
            }
            if (entropy[pos].Count < entropy[newPos].Count)
            {
                newPos = pos;
                possibleNew.Clear();
                possibleNew.Add(pos);
                continue;
            }
            if (entropy[pos].Count == entropy[newPos].Count)
            {
                possibleNew.Add(pos);
            }
        }
        if (possibleNew.Count < 0)
        {
            return possibleNew[Random.Range(0, possibleNew.Count)];
        }
        else
        {
            return newPos;
        }
    }

    void UpdateEntropy(Vector2 pos)
    {
        UpdateEntropyPosition(pos + new Vector2(1, 0), entropy[pos][0]);
        UpdateEntropyPosition(pos + new Vector2(-1, 0), entropy[pos][0]);
        UpdateEntropyPosition(pos + new Vector2(0, 1), entropy[pos][0]);
        UpdateEntropyPosition(pos + new Vector2(0, -1), entropy[pos][0]);
    }
    void UpdateEntropyPosition(Vector2 pos, TileType tile)
    {
        if (!entropy.ContainsKey(pos))
        {
            return;
        }
        if (entropy[pos].Count <= 1)
            return;
        List<TileType> newTileList = new List<TileType>();
        foreach(TileType t in entropy[pos])
        {
            bool contains = false;
            foreach(TileType tt in GetPossibleTouching(tile))
            {
                if(tt == t)
                {
                    contains = true;
                    break;
                }
            }
            if (contains)
            {
                newTileList.Add(t);
            }
        }
        entropy[pos] = newTileList;
    }
    void SpawnMap()
    {
        PrepMap();
        BuildMap();

        foreach(Vector2 pos in entropy.Keys)
        {
            GameObject tile = Instantiate(tileSet[0], new Vector3(pos.x, 0, pos.y), Quaternion.identity);
            tile.GetComponent<WFCTile>().SetTileType(entropy[pos][0]);
        }
    }

    
    public static List<TileType> GetPossibleTouching(TileType tileType)
    {
        List<TileType> tiles = new List<TileType>();
        switch (tileType)
        {
            case TileType.DEEPWATER:
                tiles.Add(TileType.DEEPWATER);
                tiles.Add(TileType.WATER);
                break;
            case TileType.WATER:
                tiles.Add(TileType.DEEPWATER);
                tiles.Add(TileType.WATER);
                tiles.Add(TileType.BEACH);
                break;
            case TileType.BEACH:
                tiles.Add(TileType.WATER);
                tiles.Add(TileType.GRASS);
                break;
            case TileType.GRASS:
                tiles.Add(TileType.BEACH);
                tiles.Add(TileType.GRASS);
                tiles.Add(TileType.FORREST);
                break;
            case TileType.FORREST:
                tiles.Add(TileType.GRASS);
                tiles.Add(TileType.FORREST);
                tiles.Add(TileType.MOUNTAIN);
                break;
            case TileType.MOUNTAIN:
                tiles.Add(TileType.FORREST);
                tiles.Add(TileType.MOUNTAIN);
                break;
            default:
                break;
        }
        return tiles;
    }

    public static Color GetColorFromTileType(TileType t)
    {
        Color c = new Color(1, 1, 1);

        switch (t)
        {
            case TileType.DEEPWATER:
                c = new Color(0, 0, .3f);
                break;
            case TileType.WATER:
                c = new Color(0, 1, 1);
                break;
            case TileType.BEACH:
                c = new Color(1, .5f, 0);
                break;
            case TileType.GRASS:
                c = new Color(0, 1, 0);
                break;
            case TileType.FORREST:
                c = new Color(0, .3f, 0);
                break;
            case TileType.MOUNTAIN:
                c = new Color(.5f, .5f, .5f);
                break;
            default:
                break;

        }


        return c;
    }
    
}
