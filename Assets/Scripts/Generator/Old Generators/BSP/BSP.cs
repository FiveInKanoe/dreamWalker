using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BSPGenerator", menuName = "Generators/BSP Generator")]
class BSP : Generator
{
    [SerializeField] private int maxLeafSize = 15;
    [SerializeField] private int minLeafSize = 17;

    [Tooltip("Offset for room from the first border of the leaf")]
    [SerializeField] private int fromWallOffset = 1;
    [Tooltip("Offset for room from the second border of the leaf")]
    [SerializeField] private int toWallOffset = 2;

    [Tooltip("Minimal size of the room")]
    [SerializeField] private int minRoomSize = 13;

    [Tooltip("Minimal width of the halls between rooms")]
    [SerializeField] private int minHallSize = 4;

    [Tooltip("Thickness of the global borders")]
    [SerializeField] private int borderWallThickness = 3;


    private Leaf root;

    private List<Leaf> leaves;
    private List<RoomBSP> rooms;

    public List<Leaf> Leaves { get => leaves; }
    public List<RoomBSP> Rooms { get => rooms; }

    private bool didSplit;

    public override void InitMap(char[,] charMap, bool isPreGen)
    {
        rooms = new List<RoomBSP>();
        didSplit = true;
        leaves = new List<Leaf>();

        CharMap = charMap;
        Height = charMap.GetLength(0);
        Width = charMap.GetLength(1);
        if (!isPreGen)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    CharMap[y, x] = (char)CellType.WALL;
                }
            }
        }
        root = new Leaf // Тут регулируется толщина стен
            (
            borderWallThickness - 1,
            borderWallThickness - 1,
            Width - borderWallThickness,
            Height - borderWallThickness
            );
        leaves.Add(root);
    }

    public override void Generate()
    {
        if (CharMap != null)
        {
            BSPSplit();
            root.CreateRooms(minRoomSize, minHallSize, fromWallOffset, toWallOffset);
            CollectRooms();
            //SetLeafBorders();
            SetRooms();
            SetHalls();
        }
      
    }

    private void BSPSplit()
    {
        while (didSplit)
        {
            didSplit = false;
            for (int i = 0; i < leaves.Count; i++)
            {
                if (leaves[i].LeftChild == null && leaves[i].RightChild == null)
                {
                    if (leaves[i].Width > maxLeafSize || leaves[i].Height > maxLeafSize || Random.Range(0f, 1f) > 0.25)
                    {
                        if (leaves[i].Split(minLeafSize))
                        {
                            leaves.Add(leaves[i].LeftChild);
                            leaves.Add(leaves[i].RightChild);
                            didSplit = true;
                        }
                    }
                }
            }
        }
    }

    private void CollectRooms()
    {
        foreach (Leaf leaf in leaves)
        {
            if (leaf.Room != null)
            {
                rooms.Add(leaf.Room);
            }
        }
    }

    private void SetLeafBorders()
    {
        foreach (Leaf leaf in leaves)
        {
            for (int y = leaf.Y; y <= leaf.Yend; y++)
            {
                for (int x = leaf.X; x <= leaf.Xend; x++)
                {
                    if (y == leaf.Y || y == leaf.Yend || x == leaf.X || x == leaf.Xend)
                    {
                        CharMap[y, x] = (char)CellType.LEAF_MARKER;
                    }
                }
            }
        }
    }

    private void SetRooms()
    {
        foreach (RoomBSP room in rooms)
        {
            for (int y = room.Y; y <= room.Yend; y++)
            {
                for (int x = room.X; x <= room.Xend; x++)
                {

                    CharMap[y, x] = (char)CellType.GROUND;

                }
            }
        }
    }

    private void SetHalls()
    {
        foreach (Leaf leaf in leaves)
        {
            if (leaf.Halls == null)
            {
                continue;
            }
            foreach (RoomBSP hall in leaf.Halls)
            {
                for (int y = hall.Y; y <= hall.Yend; y++)
                {
                    for (int x = hall.X; x <= hall.Xend; x++)
                    {
                        CharMap[y, x] = (char)CellType.GROUND;
                    }
                }
            }
        }
    }
}




