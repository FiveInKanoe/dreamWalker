using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesPlacer : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase groundTile; 

    [SerializeField] private List<Generator> generators = new List<Generator>();

    private char[,] map;


	void Start()
    {
        map = new char[height, width];
        StartGeneratorsQueue();
        PlaceTiles(); 
    }

    private void StartGeneratorsQueue()
    {
        bool bspBeforeAutomata = CheckBSPBeforeAutomata();
        for (int i = 0; i < generators.Count; i++)
        {
            bool preGen = i != 0; // Первая генерация будет сгенерирована с нуля
            if (generators[i] is CellularAutomata && bspBeforeAutomata)
            {
                int bspIndex = generators.FindIndex(delegate (Generator gen) { return gen is BSP; }); 
                ((CellularAutomata)generators[i]).InitBSPMap((BSP)generators[bspIndex]);
                bspBeforeAutomata = false;
            }
            else
            {
                generators[i].InitMap(map, preGen);
            }          
            generators[i].Generate();
        }
    }

    private bool CheckBSPBeforeAutomata()
    {
        int bspIndex = generators.FindIndex(delegate (Generator gen) { return gen is BSP; });
        int automataIndex = generators.FindIndex(delegate (Generator gen) { return gen is CellularAutomata; });
        return bspIndex < automataIndex && bspIndex != -1;
    }

	private void PlaceTiles()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[y, x] == (char)CellType.WALL)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }

                if (map[y, x] == (char)CellType.GROUND)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 2), groundTile);
                    
                }

            }
        }
    }

}
