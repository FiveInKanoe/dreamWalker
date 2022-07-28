using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AutomataGenerator", menuName = "Generators/Automata Generator")]
class CellularAutomata : Generator
{
    [Tooltip("Count of WALL cells near th GROUND cell to make it WALL")]
    [SerializeField] private int wallCellsRequired = 4;
    [Tooltip("Chance for GROUND cell to spawn if the map isn't pregenerated")]
    [SerializeField] private float chanceOfGroundSpawn = 0.8f;

    [Tooltip("Make WALL cell GROUND, if there are less WALL cells than var")]
    [SerializeField] private int cellToCleanCountOfNeighbors = 2;

    [Tooltip("Create empty spaces in the room walls (If there was a BSP map before Automata)")]
    [SerializeField] private bool createEmptyBlocksInWalls = true;
    [Tooltip("Chanse for GROUND cell to spawn in the wall of the room")]
    [SerializeField] private float chanceOfEmptyBlockInRoomWall = 0.8f;
    [Tooltip("Chanse for GROUND cell to spawn in the wall of the hall")]
    [SerializeField] private float chanceOfEmptyBlockInHallWall = 0.9f;

    [Tooltip("Create global map borders")]
    [SerializeField] private bool createBorders;
    [Tooltip("Thickness of the global borders")]
    [SerializeField] private int borderWallThickness = 1;
    [Tooltip("How deep should empty spaces be created in the room walls")]
    [SerializeField] private int BSPWallOffset = 1;

    public BSP BSP { get; set; }

    private Cell[] cells;


    public override void InitMap(char[,] charMap, bool isPreGen)
    {
        BSP = null;
        CharMap = charMap;
        Height = charMap.GetLength(0);
        Width = charMap.GetLength(1);
        if (!isPreGen)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    CharMap[y, x] = Random.Range(0f, 1f) <= chanceOfGroundSpawn ? (char)CellType.GROUND : (char)CellType.WALL;
                }
            }
        }
        cells = new Cell[CharMap.Length - 2 * (Width + Height - 2)];
    }

    public void InitBSPMap(BSP bspMap)
    {
        BSP = bspMap;
        CharMap = BSP.CharMap;
        Height = CharMap.GetLength(0);
        Width = CharMap.GetLength(1);
        cells = new Cell[CharMap.Length - 2 * (Width + Height - 2)];
    }

    public override void Generate()
    {
        if (CharMap != null)
        {
            if (BSP != null && createEmptyBlocksInWalls)
            {
                MakeWallSpaces(BSPWallOffset);
            }


            int cellsIdx = 0;
            for (int i = 1; i < Height - 1; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    cells[cellsIdx] = new Cell(CharMap[i, j], j, i);
                    cellsIdx++;
                }
            }

            AutomatonRules();
            CleanUp();      

            if (createBorders)
            {
                CreateBorders();
            }           
        }      
    }

    private void MakeWallSpaces(int offset) //Костыльный метод создания пустых пространств в стенах
    {
        foreach (RoomBSP room in BSP.Rooms)
        {
            for (int y = room.Y; y <= room.Yend; y++)
            {
                for (int x = room.X; x <= room.Xend; x++)
                {
                    for (int i = 0; i < offset; i++)
                    {
                        char cellValue = Random.Range(0f, 1f) <= chanceOfEmptyBlockInRoomWall ? (char)CellType.GROUND : (char)CellType.WALL;
                        CharMap[y + i, x + i] = cellValue;
                        CharMap[y - i, x - i] = cellValue;
                        CharMap[y + i, x - i] = cellValue;
                        CharMap[y - i, x + i] = cellValue;
                    }
                }
            }
        }

        foreach (Leaf leaf in BSP.Leaves)
        {
            if (leaf.Halls != null)
            {
                foreach (RoomBSP hall in leaf.Halls)
                {
                    for (int y = hall.Y; y <= hall.Yend; y++)
                    {
                        for (int x = hall.X; x <= hall.Xend; x++)
                        {
                            for (int i = 0; i < offset; i++)
                            {
                                char cellValue = Random.Range(0f, 1f) <= chanceOfEmptyBlockInHallWall ? (char)CellType.GROUND : (char)CellType.WALL;
                                CharMap[y + i, x + i] = cellValue;
                                CharMap[y - i, x - i] = cellValue;
                                CharMap[y + i, x - i] = cellValue;
                                CharMap[y - i, x + i] = cellValue;
                            }
                        }
                    }
                }
            }
        }

    }

    private void AutomatonRules()
    {
        foreach (Cell cell in cells)
        {
            CountMooreArea(cell, 1);
            if (cell.Value == (char)CellType.GROUND && cell.CountOfWall > wallCellsRequired)
            {
                cell.Value = (char)CellType.WALL;
                CharMap[cell.Y, cell.X] = cell.Value;
            }
        }
    }

    private void CleanUp()
    {
        foreach (Cell cell in cells)
        {
            CountMooreArea(cell, 1);
            if (cell.Value == (char)CellType.WALL && cell.CountOfWall <= cellToCleanCountOfNeighbors)
            {
                cell.Value = (char)CellType.GROUND;
                CharMap[cell.Y, cell.X] = cell.Value;
            }
        }
    }

    private void CreateBorders()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (i <= borderWallThickness - 1 || i >= Height - 1 - borderWallThickness ||
                    j <= borderWallThickness - 1 || j >= Width - 1 - borderWallThickness)
                {
                    CharMap[i, j] = (char)CellType.WALL;
                }
            }
        }
    }

    private void CountMooreArea(Cell cell, int rank)
    {
        /*
         X  X  X
         X  O  X
         X  X  X
        */
        cell.CountOfGround = 0;
        cell.CountOfWall = 0;
        int initRowIdx = cell.Y - 1;
        int initColIdx = cell.X - 1;
        for (int i = initRowIdx; i <= initRowIdx + rank + 1; i++)
        {
            for (int j = initColIdx; j <= initColIdx + rank + 1; j++)
            {
                if (i == cell.Y && j == cell.X)
                {
                    continue;
                }
                switch (CharMap[i, j])
                {
                    case (char)CellType.WALL:
                        cell.CountOfWall++;
                        break;
                    case (char)CellType.GROUND:
                        cell.CountOfGround++;
                        break;
                }
            }
        }
    }
}

class Cell
{
    public int CountOfWall { get; set; }
    public int CountOfGround { get; set; }
    public char Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Cell(char value, int x, int y)
    {
        Value = value;
        X = x;
        Y = y;
    }
}