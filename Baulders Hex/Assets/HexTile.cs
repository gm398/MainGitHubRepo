using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexTile : MonoBehaviour
{
    #region Properties

    public GameObject TileGameObjectVisuals;
    public Transform TileTopPosition;
    [SerializeField]
    public Hexagon Hexagon;


    public Vector3 WorldCoords
    {
        get
        {
           // if (_worldCoords != default) return _worldCoords;
            _worldCoords.z = Hexagon.QCoordinate * -.866f;
            _worldCoords.x = (2 * Hexagon.RCoordinate + Hexagon.QCoordinate) * .5f;
            _worldCoords.y = 0;
            return _worldCoords;
        }
    }

    private Vector3 _worldCoords;

    #endregion


    public void SetWorldPosision()
    {
        this.transform.position = WorldCoords;
    }
}


