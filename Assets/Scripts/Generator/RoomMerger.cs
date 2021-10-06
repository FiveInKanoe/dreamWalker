using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomMerger : MonoBehaviour
{

    [SerializeField] private ClusterGenerator clustGen;

    private RoomCell[,] cellMatrix;


    void Start()
    {
        cellMatrix = clustGen.GetCuttedMapMatrix();
        MakeMap();
    }


    void FixedUpdate()
    {
        
    }

    private void MakeMap()
    {
        List<GameObject> layers = new List<GameObject>()
        {
            { new GameObject("Grass") },
            { new GameObject("Road") },
            { new GameObject("Border") }
        };

        List<LayerInfo> layersInf = new List<LayerInfo>();

        foreach (GameObject layer in layers)
        {
            layer.transform.SetParent(transform);
            Tilemap tmap = layer.AddComponent(typeof(Tilemap)) as Tilemap;
            layer.AddComponent(typeof(TilemapRenderer));

            layersInf.Add(new LayerInfo(layer, tmap));
        }

        layers.Clear();

        int xRoomOffset = 0;
        int yRoomOffset = 0;
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                RoomCell cell = cellMatrix[i, j];
                if (cell != null)
                {
                    RoomView roomInfo = cell.Room.GetComponent<RoomView>();

                    int roomWidth = roomInfo.Layers[0].cellBounds.xMax;
                    int roomHeight = roomInfo.Layers[0].cellBounds.yMax;

                    xRoomOffset = roomWidth * j;
                    yRoomOffset = roomHeight * i;

                    List<TileBase[]> roomLayers = new List<TileBase[]>();
                    foreach (Tilemap tmap in roomInfo.Layers)
                    {
                        BoundsInt squareToGetFrom = new BoundsInt(new Vector3Int(0, 0, 0), new Vector3Int(roomWidth, roomHeight, 1)); //баг где-то тут      
                        roomLayers.Add(tmap.GetTilesBlock(squareToGetFrom));
                    }

                    int tileCounter = 0;
                    for (int y = 0; y < roomHeight; y++)
                    {
                        for (int x = 0; x < roomWidth; x++)
                        {
                            for (int k = 0; k < layersInf.Count; k++)
                            {
                                layersInf[k].Tilemap.SetTile
                                    (
                                    new Vector3Int(xRoomOffset + x, yRoomOffset + y, layersInf.Count - k),
                                    roomLayers[k][tileCounter]
                                    );
                            }
                            tileCounter++;
                        }
                    }
                    
                }
            }
        }
    }



    private class LayerInfo
    {
        public GameObject Layer { get; private set; }
        public Tilemap Tilemap { get; private set; }

        public LayerInfo(GameObject layer, Tilemap tilemap)
        {
            Layer = layer;
            Tilemap = tilemap;
        }
    }
}
