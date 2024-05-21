using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon
{

    #region Properties
    public int QCoordinate { get; private set; }
    public int RCoordinate { get; private set; }
    public int SCoordinate => -QCoordinate - RCoordinate;

    public Vector3 QRSCoordinates => new Vector3(QCoordinate, RCoordinate, SCoordinate);
    #endregion

    #region Constructors

    public Hexagon() { }
    public Hexagon(int q, int r)
    {
        QCoordinate = q;
        RCoordinate = r;
    }

    public Hexagon(Vector3 QRSCoordinates)
    {
        QCoordinate = (int)QRSCoordinates.x;
        RCoordinate = (int)QRSCoordinates.y;
    }
    #endregion


    #region public Methods

    public void SetHex(Hexagon hex)
    {
        QCoordinate = hex.QCoordinate;
        RCoordinate = hex.RCoordinate;
    }
    public void AddHexCoordinates(Vector3 coords)
    {
        QCoordinate += (int)coords.x;
        RCoordinate += (int)coords.y;
    }
    public List<Hexagon> NeighborCoordiates()
    {
        List<Hexagon> neighbors = Hexagon.GetSurroundingCoords();
        foreach(Hexagon h in neighbors)
        {
            h.AddHexCoordinates(QRSCoordinates);
        }

        return neighbors;
    }

    public int DistanceFromHex(Hexagon hex)
    {
        if (hex == null)
            return 0;
        return Hexagon.DistanceBetweenHexs(this, hex);
    }
    #endregion

    #region Static Methods

    static List<Hexagon> GetSurroundingCoords()
    {
        List<Hexagon> neighbors = new List<Hexagon>();
        neighbors.Add(new Hexagon(new Vector3(1, 0, -1)));
        neighbors.Add(new Hexagon(new Vector3(1, -1, 0)));
        neighbors.Add(new Hexagon(new Vector3(0, -1, 1)));
        neighbors.Add(new Hexagon(new Vector3(-1, 0, 1)));
        neighbors.Add(new Hexagon(new Vector3(-1, 1, 0)));
        neighbors.Add(new Hexagon(new Vector3(0, 1, -1)));

        return neighbors;
    }

    static int DistanceBetweenHexs(Hexagon hex1, Hexagon hex2)
    {
        if (hex1 == null || hex2 == null)
            return 0;
        Vector3 hexCoords = new Vector3(
            hex1.QCoordinate - hex2.QCoordinate,
            hex1.RCoordinate - hex2.RCoordinate,
            hex1.SCoordinate - hex2.SCoordinate);
        return (int)Mathf.Max(
            Mathf.Abs(hexCoords.x),
            Mathf.Abs(hexCoords.y),
            Mathf.Abs(hexCoords.z));
    }
    #endregion
}
