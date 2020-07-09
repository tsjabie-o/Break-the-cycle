using System;
using System.Collections.Generic;
using System.Text;
namespace Opdracht_6
{
    class Graph
    {
        static int n, m;
        private static string[] edges;

        public Dictionary<int, List<int>> Nodes { get; set; }

        public Graph()
        {
            (n, m) = GetInfo();
            edges = GetEdges();
            this.Nodes = CreateGraph();
        }

        private (int, int) GetInfo()
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            return (n, m);
        }

        private string[] GetEdges()
        {
            string[] edges = new string[m];
            for (int i = 0; i < m; i++)
            {
                edges[i] = Console.ReadLine();
            }
            return edges;
        }

        private Dictionary<int, List<int>> CreateGraph()
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (string edge in edges)
            {
                int A, B;
                A = int.Parse(edge.Split(' ')[0]);
                B = int.Parse(edge.Split(' ')[1]);
                graph[A].Add(B);
            }
            return graph;
        }

    }

    class OrderedNodes
    {
        Graph graph;
        LinkedList<int> _orderedNodes;

        public OrderedNodes(Graph graph)
        {
            this.graph = graph;
            this._orderedNodes = new LinkedList<int>();

        }

        public StringBuilder GetOutput()
        {
            if (!DepthFirstSearch(this.graph.Nodes))
            {
                int node, edge;
                (node, edge) = FixCycle();
                if (node == -1)
                {
                    StringBuilder output = new StringBuilder();
                    output.Append(NoFix());
                    return output;
                }
                else
                {
                    StringBuilder output = new StringBuilder();
                    output.Append(Fix(node, edge));
                    output.AppendLine();
                    output.Append(DisplayOrderedNodes());
                    return output;
                }
            }
            else
            {
                StringBuilder output = new StringBuilder();
                output.Append("acyclic");
                output.AppendLine();
                output.Append(DisplayOrderedNodes());
                return output;
            }
        }

        private StringBuilder Fix(int node, int edge)
        {
            string output = string.Format("fix {0} {1}", node, edge);
            StringBuilder output1 = new StringBuilder(output);
            return output1;
        }

        private StringBuilder NoFix()
        {
            StringBuilder output = new StringBuilder("no fix");
            return output;
        }

        private (int, int) FixCycle()
        {
            Dictionary<int, List<int>> temp = new Dictionary<int, List<int>>();
            foreach (var node in this.graph.Nodes.Keys)
            {
                temp[node] = new List<int>();
                foreach (int edge in this.graph.Nodes[node])
                {
                    temp[node].Add(edge);
                }
            }
            foreach (var node in this.graph.Nodes.Keys)
            {
                foreach (int edge in graph.Nodes[node])
                {
                    this._orderedNodes.Clear();
                    temp[node].Remove(edge);
                    if (DepthFirstSearch(temp))
                    {
                        return (node, edge);
                    }
                    else
                    {
                        if (temp.ContainsKey(node))
                        {
                            temp[node].Add(edge);
                        }
                        else
                        {
                            temp[node] = new List<int>() { edge };
                        }
                    }
                }
            }
            return (-1, -1);
        }

        public LinkedList<int> orderedNodes
        {
            get => this._orderedNodes;
        }

        private bool DepthFirstSearch(Dictionary<int, List<int>> graph) // The entire depth first algorithm
        {
            foreach (var node in graph)
            {
                Stack<int> callstack = new Stack<int>();
                if (!Visit(node.Key, callstack, graph))
                {
                    return false;
                }
            }
            return true;
        }

        private bool Visit(int n, Stack<int> callstack, Dictionary<int, List<int>> graph) // The recursive visiting algorithm
        {
            // If the node is already in orderedNodes, then no need to check
            if (this._orderedNodes.Contains(n))
            {
                return true;
            }

            // If callstack contains the node, there is a cycle
            if (callstack.Contains(n))
            {
                return false;
            }

            // Recursion
            callstack.Push(n);
            foreach (var node in graph[n])
            {
                if (!Visit(node, callstack, graph))
                {
                    return false;
                }
            }
            this._orderedNodes.AddFirst(n);
            callstack.Pop();
            return true;
        }

        public StringBuilder DisplayOrderedNodes() // Uses console to output ordered nodes
        {
            StringBuilder output = new StringBuilder();
            foreach (var node in this._orderedNodes)
            {
                output.Append(node + " ");
            }
            return output;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder output = new StringBuilder();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Graph graph = new Graph();
                OrderedNodes orderedNodes = new OrderedNodes(graph);
                output.Append(orderedNodes.GetOutput());
                if (i + 1 != n)
                {
                    output.AppendLine();
                }
            }
            System.Console.WriteLine(output);
        }
    }
}