using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<Tilemap> layers;

    public List<GameObject> Objects => objects;
    public List<Tilemap> Layers => layers;
}
