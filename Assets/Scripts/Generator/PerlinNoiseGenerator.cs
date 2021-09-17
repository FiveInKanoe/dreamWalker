using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerlinNoiseGen", menuName = "Generators/Perlin Noise Generator")]
public class PerlinNoiseGenerator : Generator
{

    [SerializeField] private float modifier = 0.15f;

    [Tooltip("Create global map borders")]
    [SerializeField] private bool makeWalls = true;
    [Tooltip("Thickness of the global borders")]
    [SerializeField] private int bordersThickness = 1;


    public override void InitMap(char[,] charMap, bool isPreGen)
    {
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
    }


    //Перезаписывает карту полностью. Нуждается в доработке
    public override void Generate()
    {
        if (CharMap != null)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {

                    bool isBorder = x <= bordersThickness - 1 || y <= bordersThickness - 1 ||
                                    x >= Width - 1 - bordersThickness || y >= Height - 1 - bordersThickness;
                    if (makeWalls && isBorder)
                    {
                        CharMap[y, x] = (char)CellType.WALL;
                    }
                    else
                    {
                        CharMap[y, x] = (char)(Mathf.RoundToInt(Mathf.PerlinNoise(x * modifier, y * modifier)) == 1 ? CellType.WALL : CellType.GROUND);
                    }
                }
            }
        }
    }

}
