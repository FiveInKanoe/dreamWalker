using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomMerger : MonoBehaviour
{

    [SerializeField] private ClusterGenerator clustGen;
    [SerializeField] private List<GameObject> structures;
    [SerializeField] private float probability;

    private RoomCell[,] cellMatrix;
    private int roomWidth;
    private int roomHeight;

    private List<LayerInfo> layersInf;
    private List<List<BoundsInt>> placesForStructures;

    void Start()
    {
        placesForStructures = new List<List<BoundsInt>>();
        layersInf = new List<LayerInfo>();

        cellMatrix = clustGen.GetCuttedMapMatrix();

        placesForStructures.Add(new List<BoundsInt>());
        MakeMap();
        for (int i = 0; i < structures.Count; i++)
        {
            placesForStructures.Add(new List<BoundsInt>());
            CreateStructures(structures[i].GetComponent<RoomView>().Layers[0], i + 1);
        }   
    }

    private void MakeMap()
    {
        List<GameObject> layers = new List<GameObject>()
        {
            new GameObject("Grass"),
            new GameObject("Road"),
            new GameObject("Border"),
            new GameObject("Decoration")

        };

        int zPos = layers.Count;
        foreach (GameObject layer in layers)
        {
            layer.transform.position = new Vector3(0, 0, zPos--);

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

                    roomWidth = roomInfo.Layers[0].cellBounds.xMax;
                    roomHeight = roomInfo.Layers[0].cellBounds.yMax;

                    xRoomOffset = roomWidth * j;
                    yRoomOffset = roomHeight * i;

                    List<TileBase[]> roomLayers = new List<TileBase[]>();
                    foreach (Tilemap tmap in roomInfo.Layers)
                    {
                        BoundsInt squareToGetFrom = new BoundsInt
                            (
                            new Vector3Int(0, 0, 0),
                            new Vector3Int(roomWidth, roomHeight, 1)
                            );
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
                                    new Vector3Int(xRoomOffset + x, yRoomOffset + y, 0),
                                    roomLayers[k][tileCounter]
                                    );
                            }
                            tileCounter++;
                        }
                    }

                    if (cell == clustGen.StartCell || cell == clustGen.ExitCell)
                    {
                        placesForStructures[0].Add(new BoundsInt
                            (
                            new Vector3Int(xRoomOffset, yRoomOffset, 0),
                            new Vector3Int(roomWidth - 1, roomHeight - 1, 1)
                            ));
                    }
                }
            }
        }
    }

    private void CreateStructures(Tilemap structDecouration, int structureIdx)
    {
        //Список, хранящий области, подходящие под размещение структуры
        List<BoundsInt> placesList = new List<BoundsInt>();
        //Получаем слой травы уже сгенерированной карты
        Tilemap grassLayer = layersInf[0].Tilemap;

        //Получаем общаие размеры слоя травы (то есть, всей карты)
        int mapWidth = grassLayer.cellBounds.xMax;
        int mapHeight = grassLayer.cellBounds.yMax;
        Debug.Log($"width {mapWidth}  height{mapHeight}");
        //Получаем массив всех тайлов слоя травы
        TileBase[] tiles = grassLayer.GetTilesBlock(grassLayer.cellBounds);

        //Получаем размеры структуры
        int structWidth = structDecouration.cellBounds.xMax;
        int structHeight = structDecouration.cellBounds.yMax;
        Debug.Log($"structwidth {structWidth}  structheight{structHeight}");
        //Пееребираем все тайлы на карте
        for (int mapI = structHeight; mapI < mapHeight; mapI += structHeight + 1)
        {
            for (int mapJ = structWidth; mapJ < mapWidth; mapJ += structWidth + 1)
            {
                //Флаг, для выхода из цикла, если площадь уже не будет подходящей
                bool notMatchingSpace = false;
                //Выделим на рассмотрение конкретный блок тайлов с основной карты
                BoundsInt currentArea  = new BoundsInt
                    (
                    new Vector3Int(mapJ - structWidth, mapI - structHeight, 0),
                    new Vector3Int(structWidth, structHeight, 1)
                    );
                for (int i = mapI - structHeight; i < mapI; i++)
                {
                    for (int j = mapJ - structWidth; j < mapJ; j++)
                    {

                        notMatchingSpace = !grassLayer.HasTile(new Vector3Int(j, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j, i + 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i + 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i + 1, 0));
                        
                        foreach (List<BoundsInt> areasOfstructure in placesForStructures)
                        {
                            foreach (BoundsInt area in areasOfstructure)
                            {
                                if (j >= area.xMin && j <= area.xMax && i >= area.yMin && i <= area.yMax)
                                {
                                    notMatchingSpace = true;
                                    break;
                                }
                            }
                            if (notMatchingSpace) break;
                        }
                        if (notMatchingSpace) break;
                    }
                    if (notMatchingSpace) break;
                }
                //Если место оказалось подходящим - добавить в список подходящих мест
                if (!notMatchingSpace)
                {
                    placesList.Add(currentArea);
                }
            }
        }

        foreach (BoundsInt freeArea in placesList)
        {
            if (Random.Range(0f, 1f) <= probability)
            {
                bool res = placesForStructures[structureIdx].Exists(delegate (BoundsInt place)
                {
                    bool nPlace = freeArea.xMin - 1 == place.xMax
                    || freeArea.xMax + 1 == place.xMin
                    || freeArea.yMin - 1 == place.yMax
                    || freeArea.yMax + 1 == place.yMin;
                    return nPlace; 
                });
                if (!res)
                {
                    placesForStructures[structureIdx].Add(freeArea);
                }         
            }  
        }

        

        foreach (BoundsInt freeArea in placesForStructures[structureIdx])
        {
            layersInf[3].Tilemap.SetTilesBlock(freeArea, structDecouration.GetTilesBlock(structDecouration.cellBounds));
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
