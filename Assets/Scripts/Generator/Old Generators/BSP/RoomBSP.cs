using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RoomBSP
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Xend { get; private set; }
    public int Yend { get; private set; }

    public RoomBSP(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Xend = x + width - 1;
        Yend = y + height - 1;
    }
}