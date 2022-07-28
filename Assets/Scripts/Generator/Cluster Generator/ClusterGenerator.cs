using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClusterGenerator", menuName = "Generators/Cluster Generator")]
public class ClusterGenerator : ScriptableObject
{
    [SerializeField] private int roomsCount;

    [SerializeField] private Room startRoom;
    [SerializeField] private Room exitRoom;
    [SerializeField] private List<Room> roomsPool = new List<Room>();

    private RoomCell[,] roomCellGrid;
    private List<RoomCell> existedCells;

    private int tempRoomsCount;
    private Dictionary<Direction, Vector2Int> directionVectors;

    public RoomCell StartCell { get; private set; }
    public RoomCell ExitCell { get; private set; }

    private void OnEnable()
    {
        directionVectors = new Dictionary<Direction, Vector2Int>()
        {
            { Direction.NORTH, new Vector2Int(-1, 0) },
            { Direction.EAST, new Vector2Int(0, 1) },
            { Direction.SOUTH, new Vector2Int(1, 0) },
            { Direction.WEST, new Vector2Int(0, -1) }
        };

        roomCellGrid = new RoomCell[roomsCount * 2 + 3, roomsCount * 2 + 3];
        StartCell = new RoomCell(roomsCount + 1, roomsCount + 1);
        roomCellGrid[StartCell.Y, StartCell.X] = StartCell;

        tempRoomsCount = roomsCount - 1;

        existedCells = new List<RoomCell>();
        existedCells.Add(StartCell);

        Generate();
        Shuffle();
        PlaceRoomsInCells();

    }

    private void Generate()
    {
        const int MAX_VON_NEUMANN_AREA = 4;
        Direction[] allDirections = (Direction[])System.Enum.GetValues(typeof(Direction));

        RoomCell currentRoom = StartCell;
        while (tempRoomsCount != 0)
        {
            int roomGenCounter = Random.Range(1, MAX_VON_NEUMANN_AREA + 1);

            if (roomGenCounter > tempRoomsCount)
            {
                roomGenCounter -= roomGenCounter - tempRoomsCount;
            }
            if (roomGenCounter + currentRoom.AreaRooms.Count > MAX_VON_NEUMANN_AREA)
            {
                roomGenCounter -= (roomGenCounter + currentRoom.AreaRooms.Count) - MAX_VON_NEUMANN_AREA;
            }
            while (roomGenCounter != 0)
            {
                Direction direction = allDirections[Random.Range(0, allDirections.Length)];
                bool result = PlaceNextRoom(currentRoom, directionVectors[direction]);

                if (result)
                {
                    roomGenCounter--;
                }
            }

            CalculateRoomsInRoomsArea(currentRoom);

            int nextRoomsIndex = Random.Range(0, currentRoom.AreaRooms.Count);
            currentRoom = currentRoom.AreaRooms[nextRoomsIndex];

            CalculateRoomsInRoomsArea(currentRoom);
        }

        PlaceExit();

        
    }

    private bool PlaceNextRoom(RoomCell roomCell, Vector2Int position)
    {
        int x = position.x, y = position.y;
        bool result = false;
        if (roomCellGrid[roomCell.Y + y, roomCell.X + x] == null)
        {
            roomCellGrid[roomCell.Y + y, roomCell.X + x] = new RoomCell(roomCell.Position + position);
            existedCells.Add(roomCellGrid[roomCell.Y + y, roomCell.X + x]);
            tempRoomsCount--;
            result = true;
        }
        return result;
    }

    private void CalculateRoomsInRoomsArea(RoomCell roomCell)
    {
        RoomCell potentialRoom;
        foreach (Vector2Int directionVector in directionVectors.Values)
        {
            Vector2Int tempRoomPos = roomCell.Position + directionVector;
            potentialRoom = roomCellGrid[tempRoomPos.y, tempRoomPos.x];
            if (potentialRoom != null)
            {
                if (!roomCell.AreaRooms.Contains(potentialRoom))
                {
                    roomCell.AreaRooms.Add(potentialRoom);
                }
            }
        }
    }

    private void PlaceExit()
    {
        ExitCell = existedCells[existedCells.Count - 1];

        float maxLength = Vector2Int.Distance(StartCell.Position, ExitCell.Position);
        for (int i = 1; i < existedCells.Count; i++)
        {
            float tempLength = Vector2Int.Distance(StartCell.Position, existedCells[i].Position);
            if (tempLength > maxLength)
            {
                ExitCell = existedCells[i];
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
            Room room = roomsPool[j];
            roomsPool[j] = roomsPool[i];
            roomsPool[i] = room;
        }
    }

    private void PlaceRoomsInCells()
    {
        int poolIdx = 0;
        foreach (RoomCell cell in existedCells)
        {
            
            if (poolIdx == roomsPool.Count)
            {
                poolIdx = 0;
                Shuffle();
            }

            if (cell == StartCell)
            {
                cell.Room = startRoom;
            }
            else if (cell == ExitCell)
            {
                cell.Room = exitRoom;
            }
            else
            {
                cell.Room = roomsPool[poolIdx];
            }

            poolIdx++;
        }
    }

    public RoomCell[,] GetCuttedMapMatrix()
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int maxX = int.MinValue;
        int maxY = int.MinValue;

        foreach (RoomCell cell in existedCells)
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

        foreach (RoomCell cell in existedCells)
        {
            cuttedMap[cell.Y - minY, cell.X - minX] = cell;       
        }

        return cuttedMap;
    }
}




