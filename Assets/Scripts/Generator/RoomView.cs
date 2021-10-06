using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomView : MonoBehaviour
{
    [SerializeField] private List<GameObject> exits;
    [SerializeField] private List<Tilemap> layers;

    public List<GameObject> Exits => exits;
    public List<Tilemap> Layers => layers;
}
