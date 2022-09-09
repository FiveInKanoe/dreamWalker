using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomMerger : MonoBehaviour
{

    [SerializeField] private ClusterGenerator clustGen;
    [SerializeField] private List<Room> structures = new List<Room>();
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
            CreateStructures(structures[i].GetComponent<Room>().Layers, i + 1);
        }   
    }

    private void CreateLayers()
    {
        const int MAX_LAYER_OREDER = 40;
        int backgroundID = SortingLayer.NameToID("Background");

        List<Tilemap> layerObjects = clustGen.StartCell.Room.GetComponent<Room>().Layers;

        int index = 0;   
        foreach (Tilemap layerObject in layerObjects)
        {
            GameObject globalLayerObject = new GameObject();

            Tilemap globallayersTilemap = globalLayerObject.AddComponent<Tilemap>();
            TilemapRenderer globalLayersRenderer = globalLayerObject.AddComponent<TilemapRenderer>();

            globalLayersRenderer.sortingLayerID = backgroundID;
            globalLayerObject.name = layerObject.name;

            switch (globalLayerObject.name)
            {
                case "Border":
                    globalLayersRenderer.sortingOrder = MAX_LAYER_OREDER;
                    break;
                case "Water":
                    globalLayersRenderer.sortingOrder = MAX_LAYER_OREDER - 1;
                    break;
                default:
                    globalLayersRenderer.sortingOrder = index++;
                    break;
            }

            globalLayerObject.transform.SetParent(transform);
            layersInf.Add(new LayerInfo(globalLayerObject, globallayersTilemap));
            
        }

        //const int BACKGROUND_IDX = 1, FOREGROUND_IDX = 3;

        //int layersCount = clustGen.StartCell.Room.GetComponent<Room>().Layers.Count;
        //int countOfDecoLayers = (layersCount - 1) / 2;

        //for (int i = -1; i < layersCount - 1; i++)
        //{
        //    GameObject layer = new GameObject();
        //    Tilemap tmap = layer.AddComponent(typeof(Tilemap)) as Tilemap;
        //    TilemapRenderer tmapRenderer = layer.AddComponent(typeof(TilemapRenderer)) as TilemapRenderer;

        //    if (i == -1)
        //    {
        //        layer.name = "Border";
        //        tmapRenderer.sortingLayerID = SortingLayer.layers[BACKGROUND_IDX].id;
        //        tmapRenderer.sortingOrder = 40;
        //    }
        //    else if (i < countOfDecoLayers)
        //    {
        //        layer.name = "Background-" + i.ToString();
        //        tmapRenderer.sortingLayerID = SortingLayer.layers[BACKGROUND_IDX].id;
        //        tmapRenderer.sortingOrder = i;
        //    }
        //    else
        //    {
        //        int foregroundLayerOrder = i - countOfDecoLayers;
        //        layer.name = "Foreground-" + foregroundLayerOrder.ToString();
        //        tmapRenderer.sortingLayerID = SortingLayer.layers[FOREGROUND_IDX].id;
        //        tmapRenderer.sortingOrder = foregroundLayerOrder;
        //    }

        //    layer.transform.SetParent(transform);
        //    layersInf.Add(new LayerInfo(layer, tmap));
        //}
    }

    private void MakeMap()
    {
        //going through masked map matrix
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                //pick a cell from this matrix
                RoomCell cell = cellMatrix[i, j];
                if (cell != null)
                {
                    //get all info about a room from the cell
                    Room roomInfo = cell.Room.GetComponent<Room>();

                    Tilemap currentLayer = roomInfo.Layers[0];

                    currentLayer.CompressBounds();
                    //get size of the room
                    roomWidth = currentLayer.cellBounds.xMax;
                    roomHeight = currentLayer.cellBounds.yMax;

                    //calculate global offset for whole map
                    int xRoomOffset = roomWidth * j;
                    int yRoomOffset = roomHeight * i;

                    //get all the room layers and write them to a list
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
                    //go through each tile of the room
                    for (int y = 0; y < roomHeight; y++)
                    {
                        for (int x = 0; x < roomWidth; x++)
                        {
                            //go through each layer of the room
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

                    foreach (GameObject gameObject in roomInfo.Objects)
                    {
                        GameObject newObjectToPlace = Instantiate(gameObject);
                        Vector2 objectPosition = new Vector2()
                        {
                            x = gameObject.transform.position.x + xRoomOffset,
                            y = gameObject.transform.position.y + yRoomOffset
                        };
                        newObjectToPlace.transform.localPosition = objectPosition;
                        newObjectToPlace.transform.SetParent(transform);
                    }

                    //???
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
        List<BoundsInt> placesList = new List<BoundsInt>();

        Tilemap grassLayer = layersInf[1].Tilemap;

        int mapWidth = grassLayer.cellBounds.xMax;
        int mapHeight = grassLayer.cellBounds.yMax;
        Debug.Log($"width {mapWidth}  height{mapHeight}");

        TileBase[] tiles = grassLayer.GetTilesBlock(grassLayer.cellBounds);


        int structWidth = structsTilemaps[0].cellBounds.xMax;
        int structHeight = structsTilemaps[0].cellBounds.yMax;
        Debug.Log($"structwidth {structWidth}  structheight{structHeight}");

        for (int mapI = structHeight; mapI < mapHeight; mapI += structHeight + 1)
        {
            for (int mapJ = structWidth; mapJ < mapWidth; mapJ += structWidth + 1)
            {
                bool notMatchingSpace = false;
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
