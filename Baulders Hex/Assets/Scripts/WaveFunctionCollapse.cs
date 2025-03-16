using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse
{
    private Dictionary<Vector3, WFCHex> WHCHexs = new();

    public WaveFunctionCollapse(int noOfHexs)
    {
        
    }

    List<HexType> CheckPossibleNeighbours(List<HexType> hexToCheck)
    {
        List<HexType> eligibleNeighbours = new();
        List<HexType> nonEligibleList = new();
        foreach (var hex in hexToCheck)
        {
            switch (hex)
            {
                case HexType.Deepwater:
                    nonEligibleList.Add(HexType.Grass);
                    nonEligibleList.Add(HexType.Hill);
                    nonEligibleList.Add(HexType.Mountain);
                    break;
                case HexType.Water:
                    nonEligibleList.Add(HexType.Hill);
                    nonEligibleList.Add(HexType.Mountain);
                    break;
                case HexType.Grass:
                    nonEligibleList.Add(HexType.Deepwater);
                    nonEligibleList.Add(HexType.Mountain);
                    break;
                case HexType.Hill:
                    nonEligibleList.Add(HexType.Deepwater);
                    nonEligibleList.Add(HexType.Water);
                    break;
                case HexType.Mountain:
                    nonEligibleList.Add(HexType.Deepwater);
                    nonEligibleList.Add(HexType.Water);
                    nonEligibleList.Add(HexType.Grass);
                    break;
            }
        }

        foreach (HexType type in Enum.GetValues(typeof(HexType)))
        {
            if(!nonEligibleList.Contains(type)) eligibleNeighbours.Add(type);
        }
        return eligibleNeighbours;
    }
}


public class WFCHex
{
    public Hexagon Hex;
    public HexType Type;
    public int Entropy;
}

public enum HexType
{
    Default,
    Deepwater,
    Water,
    Grass,
    Hill,
    Mountain
}
