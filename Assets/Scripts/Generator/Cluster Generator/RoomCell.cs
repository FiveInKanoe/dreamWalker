using System.Collections.Generic;
using UnityEngine;

public class RoomCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public List<RoomCell> VonNeumannAreaRooms { get; private set; }
    public GameObject Room { get; set; }

    public RoomCell(int y, int x)
    {
        X = x;
        Y = y;
        VonNeumannAreaRooms = new List<RoomCell>();
    }
}
