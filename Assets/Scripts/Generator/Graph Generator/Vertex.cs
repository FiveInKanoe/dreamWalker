using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GraphGenerator
{
    private class Vertex
    {
        private List<GameObject> exits;
        public GameObject Room { get; private set; }
        public List<Edge> Edges { get; private set; }
        public int MaxPower { get; private set; }
        public GameObject GetExit => exits[CurrentPower];
        public int CurrentPower => Edges.Count;

        public Vertex(GameObject room)
        {
            Room = room;
            Edges = new List<Edge>();
            exits = room.GetComponent<RoomView>()?.Exits;
            MaxPower = exits.Count;
        }

        public bool IsConnected(Vertex vertex)
        {
            bool result = false;
            foreach (Edge edge in Edges)
            {
                if (edge.First == this)
                {
                    result = edge.Second == vertex;
                }
                else
                {
                    result = edge.First == vertex;
                }
                if (result)
                {
                    break;
                }
            }
            return result;
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        
    }
    
}
