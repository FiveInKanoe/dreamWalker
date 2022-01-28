using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClusterGenerator", menuName = "Generators/Cluster Generator")]
public class ClusterGenerator : ScriptableObject
{
    [SerializeField] private int roomsCount;

    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject exitRoom;
    [SerializeField] private List<GameObject> roomsPool = new List<GameObject>();


    private RoomCell[,] roomCellGrid;
    private List<RoomCell> existedRooms;

    private int tempRoomsCount;

    public RoomCell StartCell { get; private set; }
    public RoomCell ExitCell { get; private set; }

    private void OnEnable()
    {
        roomCellGrid = new RoomCell[roomsCount * 2 + 3, roomsCount * 2 + 3];
        StartCell = new RoomCell(roomsCount + 1, roomsCount + 1);
        roomCellGrid[StartCell.Y, StartCell.X] = StartCell;

        tempRoomsCount = roomsCount - 1;

        existedRooms = new List<RoomCell>();
        existedRooms.Add(StartCell);


        Generate();
        Shuffle();
        PlaceRoomsInCells();

    }

    private void Generate()
    {
        int maxVonNeumannArea = 4;

        RoomCell currentRoom = StartCell;
        while (tempRoomsCount != 0)
        {
            int roomGenCounter = Random.Range(1, maxVonNeumannArea + 1);

            if (roomGenCounter > tempRoomsCount)
            {
                roomGenCounter -= roomGenCounter - tempRoomsCount;
            }
            if (roomGenCounter + currentRoom.VonNeumannAreaRooms.Count > maxVonNeumannArea)
            {
                roomGenCounter -= (roomGenCounter + currentRoom.VonNeumannAreaRooms.Count) - maxVonNeumannArea;
            }
            while (roomGenCounter != 0)
            {
                bool result = false;
                Direction direction = GetRandomDirection();
                switch (direction)
                {
                    case Direction.NORTH:
                        result = PlaceRoom(currentRoom, -1, 0);
                        break;
                    case Direction.EAST:
                        result = PlaceRoom(currentRoom, 0, 1);
                        break;
                    case Direction.SOUTH:
                        result = PlaceRoom(currentRoom, 1, 0);
                        break;
                    case Direction.WEST:
                        result = PlaceRoom(currentRoom, 0, -1);
                        break;
                }
                if (result)
                {
                    roomGenCounter--;
                }
            }

            CalculateVonNeumannArea(currentRoom);
            int idx = Random.Range(0, currentRoom.VonNeumannAreaRooms.Count);
            currentRoom = currentRoom.VonNeumannAreaRooms[idx];
            CalculateVonNeumannArea(currentRoom);
        }

        PlaceExit();

        
    }

    private bool PlaceRoom(RoomCell roomCell, int y, int x)
    {
        bool result = false;
        if (roomCellGrid[roomCell.Y + y, roomCell.X + x] == null)
        {
            roomCellGrid[roomCell.Y + y, roomCell.X + x] = new RoomCell(roomCell.Y + y, roomCell.X + x);
            existedRooms.Add(roomCellGrid[roomCell.Y + y, roomCell.X + x]);
            tempRoomsCount--;
            result = true;
        }
        return result;
    }

    private void CalculateVonNeumannArea(RoomCell roomCell)
    {
        RoomCell tempRoomCell;
        tempRoomCell = roomCellGrid[roomCell.Y + 1, roomCell.X];
        if (tempRoomCell != null)
        {
            if (!roomCell.VonNeumannAreaRooms.Contains(tempRoomCell))
            {
                roomCell.VonNeumannAreaRooms.Add(tempRoomCell);
            }
        }
        tempRoomCell = roomCellGrid[roomCell.Y - 1, roomCell.X];
        if (tempRoomCell != null)
        {
            if (!roomCell.VonNeumannAreaRooms.Contains(tempRoomCell))
            {
                roomCell.VonNeumannAreaRooms.Add(tempRoomCell);
            }
        }
        tempRoomCell = roomCellGrid[roomCell.Y, roomCell.X + 1];
        if (tempRoomCell != null)
        {
            if (!roomCell.VonNeumannAreaRooms.Contains(tempRoomCell))
            {
                roomCell.VonNeumannAreaRooms.Add(tempRoomCell);
            }
        }
        tempRoomCell = roomCellGrid[roomCell.Y, roomCell.X - 1];
        if (tempRoomCell != null)
        {
            if (!roomCell.VonNeumannAreaRooms.Contains(tempRoomCell))
            {
                roomCell.VonNeumannAreaRooms.Add(tempRoomCell);
            }
        }
    }

    private Direction GetRandomDirection()
    {
        Direction direction;
        double chance = Random.Range(0f, 1f);
        if (chance >= 0.5)
        {
            if (chance >= 0.75)
            {
                direction = Direction.NORTH;
            }
            else
            {
                direction = Direction.EAST;
            }
        }
        else
        {
            if (chance < 0.25)
            {
                direction = Direction.SOUTH;
            }
            else
            {
                direction = Direction.WEST;
            }
        }
        return direction;
    }

    private void PlaceExit()
    {
        // Лаба Шолоха хд (ну почти)
        /* TODO:
         * Можно потом написать алгоритм с лабы
         * для создания максимально удаленных точек старта и конца,
         * т. е. инициализировать вход, как и выход, в самом конце
         */
        ExitCell = existedRooms[existedRooms.Count - 1];
        double maxLength = Mathf.Sqrt(Mathf.Pow(ExitCell.X - StartCell.X, 2) + Mathf.Pow(ExitCell.Y - StartCell.Y, 2));
        for (int i = 1; i < existedRooms.Count; i++)
        {
            double tempLength = Mathf.Sqrt(Mathf.Pow(existedRooms[i].X - StartCell.X, 2) + Mathf.Pow(existedRooms[i].Y - StartCell.Y, 2));
            if (tempLength > maxLength)
            {
                ExitCell = existedRooms[i];
                maxLength = tempLength;
            }
        }
    }

    private void Shuffle()
    {
        int j;
        for (int i = roomsPool.Count - 1; i > 0; i--)
        {
            j = (int)Mathf.Floor(Random.Range(0, i + 1));
            GameObject room = roomsPool[j];
            roomsPool[j] = roomsPool[i];
            roomsPool[i] = room;
        }
    }

    private void PlaceRoomsInCells()
    {
        int poolIdx = 0;
        foreach (RoomCell cell in existedRooms)
        {
            
            if (poolIdx == roomsPool.Count)
            {
                poolIdx = 0;
                Shuffle();
            }
            if (cell == StartCell && cell.Room == null)
            {
                cell.Room = startRoom;
            }
            else if (cell == ExitCell && cell.Room == null)
            {
                cell.Room = exitRoom;
            }
            else
            {
                cell.Room = roomsPool[poolIdx];
            }
            if (cell.Room != null)
            {
                poolIdx++;
            }
        }
    }

    public RoomCell[,] GetCuttedMapMatrix()
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = int.MinValue;
        int maxY = int.MinValue;
        foreach (RoomCell cell in existedRooms)
        {
            if (minX > cell.X)
            {
                minX = cell.X;
            }
            if (minY > cell.Y)
            {
                minY = cell.Y;
            }
            if (maxX < cell.X)
            {
                maxX = cell.X;
            }
            if (maxY < cell.Y)
            {
                maxY = cell.Y;
            }
        }
        RoomCell[,] cuttedMap = new RoomCell[Mathf.Abs(maxY - minY) + 1, Mathf.Abs(maxX - minX) + 1];
        foreach (RoomCell cell in existedRooms)
        {
            cuttedMap[cell.Y - minY, cell.X - minX] = cell;
        }
        return cuttedMap;
    }
}




