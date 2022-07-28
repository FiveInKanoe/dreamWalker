using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Leaf
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Xend { get; private set; }
    public int Yend { get; private set; }

    public Leaf RightChild { get; private set; }
    public Leaf LeftChild { get; private set; }

    public List<RoomBSP> Halls { get; private set; }
    public RoomBSP Room { get; private set; }


    public Leaf(int x, int y, int width, int height)
    {
        X = x;
        Y = y;

        Width = width;
        Height = height;

        Xend = X + Width - 1;
        Yend = Y + Height - 1;

        Halls = null;
        Room = null;
    }

    public bool Split(int minLeafSize)
    {
        if (RightChild != null || LeftChild != null)
        {
            return false;
        }

        bool splitVertical = Random.Range(0f, 1f) > 0.5;
        if (Width > Height && Width / Height >= 1.25)
        {
            splitVertical = false;
        }
        else if (Height > Width && Height / Width >= 1.25)
        {
            splitVertical = true;
        }

        int maxSize = (splitVertical ? Height : Width) - minLeafSize;
        if (maxSize <= minLeafSize)
        {
            return false;
        }

        int splitPlace = Random.Range(minLeafSize, maxSize);
        if (splitVertical)
        {
            LeftChild = new Leaf(X, Y, Width, splitPlace);
            RightChild = new Leaf(X, Y + splitPlace, Width, Height - splitPlace);
        }
        else
        {
            LeftChild = new Leaf(X, Y, splitPlace, Height);
            RightChild = new Leaf(X + splitPlace, Y, Width - splitPlace, Height);
        }
        return true;
    }

    public void CreateRooms(int minRoomSize,int minHallSize, int fromWallOffset, int toWallOffset)
    {
        if (LeftChild != null || RightChild != null)
        {
            if (LeftChild != null)
            {
                LeftChild.CreateRooms(minRoomSize, minHallSize, fromWallOffset, toWallOffset);
            }
            if (RightChild != null)
            {
                RightChild.CreateRooms(minRoomSize, minHallSize, fromWallOffset, toWallOffset);
            }

            if (LeftChild != null && RightChild != null)
            {
                CreateHall(LeftChild.GetRoom(), RightChild.GetRoom(), minHallSize);
            }
        }
        else
        {
            int roomWidth = Random.Range(minRoomSize, Width - toWallOffset);
            int roomHeight = Random.Range(minRoomSize, Height - toWallOffset);

            int roomX = Random.Range(fromWallOffset, Width - roomWidth - fromWallOffset);
            int roomY = Random.Range(fromWallOffset, Height - roomHeight - fromWallOffset);

            Room = new RoomBSP(X + roomX, Y + roomY, roomWidth, roomHeight);
        }
    }


    private RoomBSP GetRoom()
    {
        if (Room != null)
        {
            return Room;
        }
        else
        {
            RoomBSP leftRoom = null;
            RoomBSP rightRoom = null;

            if (LeftChild != null) { leftRoom = LeftChild.GetRoom(); }

            if (RightChild != null) { rightRoom = RightChild.GetRoom(); }

            if (leftRoom == null && rightRoom == null) { return null; }

            else if (rightRoom == null) { return leftRoom; }

            else if (leftRoom == null) { return rightRoom; }

            else if (Random.Range(0f, 1f) >= 0.5) { return leftRoom; }

            else { return rightRoom; }
        }
    }

    private void CreateHall(RoomBSP leftRoom, RoomBSP rightRoom, int minHallSize)
    {
        Halls = new List<RoomBSP>();
        int point1x = Random.Range(leftRoom.X + 1, leftRoom.Xend - 4);
        int point1y = Random.Range(leftRoom.Y + 1, leftRoom.Yend - 4);

        int point2x = Random.Range(rightRoom.X + 1, rightRoom.Xend - 4);
        int point2y = Random.Range(rightRoom.Y + 1, rightRoom.Yend - 4);

        int width = point2x - point1x;
        int height = point2y - point1y;

        if (width < 0)
        {
            if (height < 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new RoomBSP(point2x, point1y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point2x, point2y, minHallSize, Mathf.Abs(height)));
                }
                else
                {
                    Halls.Add(new RoomBSP(point2x, point2y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point1x, point2y, minHallSize, Mathf.Abs(height)));
                }
            }
            else if (height > 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new RoomBSP(point2x, point1y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point2x, point1y, minHallSize, Mathf.Abs(height)));
                }
                else
                {
                    Halls.Add(new RoomBSP(point2x, point2y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point1x, point1y, minHallSize, Mathf.Abs(height)));
                }
            }
            else
            {
                Halls.Add(new RoomBSP(point2x, point2y, Mathf.Abs(width), minHallSize));
            }
        }
        else if (width > 0)
        {
            if (height < 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new RoomBSP(point1x, point2y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point1x, point2y, minHallSize, Mathf.Abs(height)));
                }
                else
                {
                    Halls.Add(new RoomBSP(point1x, point1y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point2x, point2y, minHallSize, Mathf.Abs(height)));
                }
            }
            else if (height > 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new RoomBSP(point1x, point1y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point2x, point1y, minHallSize, Mathf.Abs(height)));
                }
                else
                {
                    Halls.Add(new RoomBSP(point1x, point2y, Mathf.Abs(width), minHallSize));
                    Halls.Add(new RoomBSP(point1x, point1y, minHallSize, Mathf.Abs(height)));
                }
            }
            else
            {
                Halls.Add(new RoomBSP(point1x, point1y, Mathf.Abs(width), minHallSize));
            }
        }
        else
        {
            if (height < 0)
            {
                Halls.Add(new RoomBSP(point2x, point2y, minHallSize, Mathf.Abs(height)));
            }
            else if (height > 0)
            {
                Halls.Add(new RoomBSP(point1x, point1y, minHallSize, Mathf.Abs(height)));
            }
        }
    }
}
