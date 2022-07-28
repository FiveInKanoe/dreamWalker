using System.Collections.Generic;
using UnityEngine;

public class RoomCell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public List<RoomCell> AreaRooms { get; private set; }
    public Room Room { get; set; }
    public Vector2Int Position { get; private set; }

    public RoomCell(int x, int y)
    {
        X = x;
        Y = y;
        AreaRooms = new List<RoomCell>();
        Position = new Vector2Int(x, y);
    }

    public RoomCell(Vector2Int position)
    {
        X = position.x;
        Y = position.y;
        AreaRooms = new List<RoomCell>();
        Position = position;
    }
}
