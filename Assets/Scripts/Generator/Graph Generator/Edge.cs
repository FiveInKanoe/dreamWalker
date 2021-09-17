using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GraphGenerator
{
    private class Edge
    {
        public Vertex First { get; private set; }
        public GameObject ExitFirst { get; private set; }

        public Vertex Second { get; private set; }
        public GameObject ExitSecond { get; private set; }

        public Edge(Vertex first, Vertex second, GameObject exitFirst, GameObject exitSecond)
        {
            First = first;
            Second = second;
            ExitFirst = exitFirst;
            ExitSecond = exitSecond;
        }
    }
}
