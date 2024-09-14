using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private HexGrid HexGrid => HexGrid.HexGridInstance;
    [SerializeField] private List<GameObject> mapPieces;
    
    private void Start()
    {
        HexGrid.InitializeGrid(mapPieces);
    }
}
