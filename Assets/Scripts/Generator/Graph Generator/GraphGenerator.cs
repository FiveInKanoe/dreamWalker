using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GraphGenerator", menuName = "Generators/Graph Generator")]
public partial class GraphGenerator : ScriptableObject
{
    [SerializeField] private int roomCount;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject exitRoom;
    [SerializeField] private List<GameObject> roomPool = new List<GameObject>();

    private List<Vertex> vertices;
    private List<Edge> edges;

    private void OnEnable()
    {
        vertices = new List<Vertex>();
        edges = new List<Edge>();
        Generate();
    }

    private void Generate()
    {
        Shuffle();
        roomPool.Insert(0, startRoom);
        roomPool.Add(exitRoom);
        for (int i = 0; i < roomPool.Count; i++) // Исправить на то, чтоб комнаты не дублировались
        {
            vertices.Add(new Vertex(roomPool[0]));
            roomPool.RemoveAt(0);
        }
        for (int i = 1; i < vertices.Count; i++)
        {
            AddEdge(vertices[i - 1], vertices[i]);
        }
        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                if (vertices[i].CurrentPower == vertices[i].MaxPower)
                {
                    break;
                }
                if (vertices[j].CurrentPower == vertices[j].MaxPower)
                {
                    continue;
                }
                if (!vertices[i].IsConnected(vertices[j]))
                {
                    AddEdge(vertices[i], vertices[j]);
                }
            }
        }
    }

    private void AddEdge(Vertex first, Vertex second)
    {
        GameObject exitFirst = first.GetExit;
        GameObject exitSecond = second.GetExit;
        Edge edge = new Edge(first, second, exitFirst, exitSecond);
        first.AddEdge(edge);
        second.AddEdge(edge);
        edges.Add(edge);
    }

    private void Shuffle()
    {
        int j;
        for (int i = roomPool.Count - 1; i > 0; i--)
        {
            j = (int)Mathf.Floor(Random.Range(0, i + 1));
            GameObject room = roomPool[j];
            roomPool[j] = roomPool[i];
            roomPool[i] = room;
        } 
    }

    public List<GameObject> GetRooms()
    {
        List<GameObject> rooms = null;
        if (vertices.Count != 0)
        {
            rooms = new List<GameObject>();
            foreach (Vertex vertex in vertices)
            {
                rooms.Add(vertex.Room);
            }
        }
        return rooms;
    } 
}
