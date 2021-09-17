using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomMerger : MonoBehaviour
{

    [SerializeField] private GraphGenerator graphGen;
    private List<GameObject> rooms;


    private Grid mainGrid;


    void Start()
    {
        rooms = graphGen.GetRooms();
        PlaceRooms();
        MergeRooms();
    }


    void FixedUpdate()
    {
        
    }

    private void PlaceRooms()
    {
        GameObject grass = new GameObject("Grass");
        grass.transform.SetParent(transform);
        Tilemap grassTmap = grass.AddComponent(typeof(Tilemap)) as Tilemap;
        grass.AddComponent(typeof(TilemapRenderer));
        

        GameObject road = new GameObject("Road");
        road.transform.SetParent(transform);
        Tilemap roadTmap = road.AddComponent(typeof(Tilemap)) as Tilemap;
        road.AddComponent(typeof(TilemapRenderer));
        

        GameObject border = new GameObject("Border");
        border.transform.SetParent(transform);
        Tilemap borderTmap = border.AddComponent(typeof(Tilemap)) as Tilemap;
        border.AddComponent(typeof(TilemapRenderer));
        
        //—ÀŒ»  ŒÃÕ¿“ Õ≈ ◊»“¿ﬁ“—ﬂ! Õ¿ƒŒ »—œ–¿¬»“‹

        int xRoomOffset = 0;
        int yRoomOffset = 0;
        foreach (GameObject room in rooms)
        {
            RoomView roomInfo = room.GetComponent<RoomView>();
            
            int roomWidth = roomInfo.Layers[0].cellBounds.xMax;
            int roomHeight = roomInfo.Layers[0].cellBounds.yMax;

            TileBase[] roomGrass = roomInfo.Layers[0].GetTilesBlock(roomInfo.Layers[0].cellBounds);
            TileBase[] roomRoad = roomInfo.Layers[1].GetTilesBlock(roomInfo.Layers[1].cellBounds);
            TileBase[] roomBorder = roomInfo.Layers[2].GetTilesBlock(roomInfo.Layers[2].cellBounds);

            int mult = 0;
            for (int y = 0; y < roomHeight; y++)
            {
                mult++;
                for (int x = 0; x < roomWidth; x++)
                {
                    grassTmap.SetTile(new Vector3Int(xRoomOffset + x, yRoomOffset + y, 2), roomGrass[y * mult + x]);
                    roadTmap.SetTile(new Vector3Int(xRoomOffset + x, yRoomOffset + y, 1), roomRoad[y * mult + x]);
                    borderTmap.SetTile(new Vector3Int(xRoomOffset + x, yRoomOffset + y, 0), roomBorder[y * mult + x]);
                    Debug.Log($"[{xRoomOffset + x}, { yRoomOffset + y}]");
                }
            }

            //¬Ò∏ ‚ Ó‰ÌÛ ÎËÌË˛
            xRoomOffset += roomWidth - 1;
            //yRoomOffset += roomHeight - 1;
            Debug.Log(xRoomOffset);
        }
    }

    private void MergeRooms()
    {
        
    }
}
