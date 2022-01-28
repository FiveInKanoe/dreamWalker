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
        CreateLayers();
        MakeMap();
        for (int i = 0; i < structures.Count; i++)
        {
            placesForStructures.Add(new List<BoundsInt>());
            CreateStructures(structures[i].GetComponent<RoomView>().Layers, i + 1);
        }   
    }

    private void CreateLayers()
    {
        const int BACKGROUND_IDX = 1, FOREGROUND_IDX = 3;

        int layersCount = clustGen.StartCell.Room.GetComponent<RoomView>().Layers.Count;
        int countOfDecoLayers = (layersCount - 1) / 2;

        for (int i = -1; i < layersCount - 1; i++)
        {
            GameObject layer = new GameObject();
            Tilemap tmap = layer.AddComponent(typeof(Tilemap)) as Tilemap;
            TilemapRenderer tmapRenderer = layer.AddComponent(typeof(TilemapRenderer)) as TilemapRenderer;

            if (i == -1)
            {
                layer.name = "Border";
                tmapRenderer.sortingLayerID = SortingLayer.layers[BACKGROUND_IDX].id;
                tmapRenderer.sortingOrder = 40;
            }
            else if (i < countOfDecoLayers)
            {
                layer.name = "Background-" + i.ToString();
                tmapRenderer.sortingLayerID = SortingLayer.layers[BACKGROUND_IDX].id;
                tmapRenderer.sortingOrder = i;
            }
            else
            {
                int foregroundLayerOrder = i - countOfDecoLayers;
                layer.name = "Foreground-" + foregroundLayerOrder.ToString();
                tmapRenderer.sortingLayerID = SortingLayer.layers[FOREGROUND_IDX].id;
                tmapRenderer.sortingOrder = foregroundLayerOrder;
            }

            layer.transform.SetParent(transform);
            layersInf.Add(new LayerInfo(layer, tmap));
        }
    }

    private void MakeMap()
    {
        //List<GameObject> layers = new List<GameObject>()
        //{
        //    new GameObject("Grass"),
        //    new GameObject("Road"),
        //    new GameObject("Border"),
        //    new GameObject("Decoration")

        //};

        //int zPos = layers.Count;
        //foreach (GameObject layer in layers)
        //{
        //    layer.transform.position = new Vector3(0, 0, zPos--);

        //    layer.transform.SetParent(transform);
        //    Tilemap tmap = layer.AddComponent(typeof(Tilemap)) as Tilemap;
        //    layer.AddComponent(typeof(TilemapRenderer));

        //    layersInf.Add(new LayerInfo(layer, tmap));
        //}

        //layers.Clear();

        

        int xRoomOffset = 0;
        int yRoomOffset = 0;
        //Обход матричной маски, с размещениями чанков
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                //Конкретный чанк
                RoomCell cell = cellMatrix[i, j];
                if (cell != null)
                {
                    //Информация о чанке (слои и т д)
                    RoomView roomInfo = cell.Room.GetComponent<RoomView>();
                    //Получаем текущий слой
                    Tilemap currentLayer = roomInfo.Layers[0];
                    //Спрессовываем границы слоя для корректного "склеивания" чанков
                    currentLayer.CompressBounds();
                    //Сохраняем размер чанка
                    roomWidth = currentLayer.cellBounds.xMax;
                    roomHeight = currentLayer.cellBounds.yMax;
                    //Просчитываем "сдвиг" чанков друг от друга, по их позиции на матрице-маске
                    xRoomOffset = roomWidth * j;
                    yRoomOffset = roomHeight * i;

                    //Сохраняем все слои текущего чанка
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
                    //Обходим весь чанк по тайлам
                    for (int y = 0; y < roomHeight; y++)
                    {
                        for (int x = 0; x < roomWidth; x++)
                        {
                            //Обходим все слои, которые ранее сохранили
                            for (int k = 0; k < layersInf.Count; k++)
                            {
                                //Устанавливаем тайлы на определенную позицию 
                                layersInf[k].Tilemap.SetTile
                                    (
                                    new Vector3Int(xRoomOffset + x, yRoomOffset + y, 0),
                                    roomLayers[k][tileCounter]
                                    );
                            }
                            tileCounter++;
                        }
                    }
                    //Добавляем старотовый и конечный чанк в список структур
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

    private void CreateStructures(List<Tilemap> structsTilemaps, int structureIdx)
    {
        //Список, хранящий области, подходящие под размещение структуры
        List<BoundsInt> placesList = new List<BoundsInt>();
        //Получаем слой травы уже сгенерированной карты
        Tilemap grassLayer = layersInf[1].Tilemap;

        //Получаем общаие размеры слоя травы (то есть, всей карты)
        int mapWidth = grassLayer.cellBounds.xMax;
        int mapHeight = grassLayer.cellBounds.yMax;
        Debug.Log($"width {mapWidth}  height{mapHeight}");
        //Получаем массив всех тайлов слоя травы
        TileBase[] tiles = grassLayer.GetTilesBlock(grassLayer.cellBounds);

        //Получаем размеры структуры
        int structWidth = structsTilemaps[0].cellBounds.xMax;
        int structHeight = structsTilemaps[0].cellBounds.yMax;
        Debug.Log($"structwidth {structWidth}  structheight{structHeight}");
        //Пееребираем все потенциальные места на карте c шагом в чанк
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
                //перебираем все тайлы в потенциальном чанке
                for (int i = mapI - structHeight; i < mapI; i++)
                {
                    for (int j = mapJ - structWidth; j < mapJ; j++)
                    {
                        //Не выходит ли текущее место за пределы карты?
                        notMatchingSpace = !grassLayer.HasTile(new Vector3Int(j, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i, 0))
                            || !grassLayer.HasTile(new Vector3Int(j, i + 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i + 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j + 1, i - 1, 0))
                            || !grassLayer.HasTile(new Vector3Int(j - 1, i + 1, 0));
                        
                        //Перебираем списки размещения для разных структур (у каждой структуры - свой список) (можно создать монобех для структуры и хранить список там)
                        foreach (List<BoundsInt> areasOfstructure in placesForStructures)
                        {
                            //Перебираем конкреетно каждую область в списке областей
                            foreach (BoundsInt area in areasOfstructure)
                            {
                                //Если 
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

        

        //редачить тут
        foreach (BoundsInt freeArea in placesForStructures[structureIdx])
        {
            TileBase[] emptyTiles = new TileBase[freeArea.size.x * freeArea.size.y];
            for (int i = 0; i < emptyTiles.Length; i++)
            {
                emptyTiles[i] = null;
            }
            for (int i = 2; i < layersInf.Count; i++)
            {
                if (structsTilemaps.Count > (i - 2))
                {
                    layersInf[i].Tilemap.SetTilesBlock(freeArea, structsTilemaps[i - 2].GetTilesBlock(structsTilemaps[i - 2].cellBounds));               
                }
                else
                {
                    layersInf[i].Tilemap.SetTilesBlock(freeArea, emptyTiles);
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
