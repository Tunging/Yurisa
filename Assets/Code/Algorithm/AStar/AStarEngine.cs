using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Algorithm.AStar
{
    public class AStarEngine:BaseManager<AStarEngine>
    {
        int maxRow = 100, maxCol = 100;
        Node[][] Nodes;

        List<Node> closeList = new List<Node>();
        List<Node> openList = new List<Node>();

        public AStarEngine()
        {
            Nodes = new Node[maxRow][];
            for (int i = 0; i < maxRow; i++)
            {
                Nodes[i] = new Node[maxCol];
                for (int col = 0; col < maxCol; col++)
                {
                    Node node = new Node(i, col);
                    node.NodeType = NodeType.Normal;
                    Nodes[i][col] = node;
                }
            }
        }

        public void FindPath(Vector2Int start, Vector2Int end)
        {
            Node endNode = Nodes[end.x][end.y];
            if (FindPath(Nodes[start.x][start.y], endNode))
            {
                Node node = endNode;

                while (node.parent!=null)
                {
                    UnityEngine.Debug.Log(node);
                    node = node.parent;
                }
            }
        }

        public bool FindPath(Node start,Node end)
        {
            closeList.Add(start);

            Node[] neighbours = FindNeighbour(start);
            openList.AddRange(neighbours);

            Node minFNode = null;
            for (int i = 0; i < neighbours.Length; i++)
            {
                Node node = neighbours[i];
                if (node!=null)
                {
                    node.H = (Math.Abs(end.x - node.x) + Math.Abs(end.y - node.y))*10;
                    node.G = CalcNodeG(node, start);

                    if (minFNode == null)
                    {
                        minFNode = node;
                    }
                    else if(minFNode.F>node.F)
                    {
                        minFNode = node;
                    }
                }
            }

            if (minFNode == null || minFNode == end)
            {
                UnityEngine.Debug.Log("ddddd");
                //end
                return true;
            }
            else
            {
                minFNode.parent = start;
                FindPath(minFNode, end);
            }

            return false;
        }

        private int CalcNodeG(Node node,Node start)
        {
            if (node == null || start == null)
            {
                return 0;
            }
            int offsetx = node.x - start.x;
            int offsety = node.y - start.y;
            if (offsetx ==1 && offsety == 1)
            {
                return 14;
            }
            else if(offsetx == 1 || offsety == 1)
            {
                return 10;
            }
            else
            {
                return CalcNodeG(node,node.parent)+CalcNodeG(node.parent, start);
            }
        }

        private Node[] FindNeighbour(Node start)
        {
            List<Node> nodes = new List<Node>();
            SelectNode(start.x, start.y + 1,ref nodes);//top
            SelectNode(start.x-1, start.y + 1, ref nodes);//top left
            SelectNode(start.x + 1, start.y + 1, ref nodes);//top right

            SelectNode(start.x - 1, start.y , ref nodes);//left
            SelectNode(start.x + 1, start.y, ref nodes);//right

            SelectNode(start.x - 1, start.y -1, ref nodes);//bottom left
            SelectNode(start.x , start.y - 1, ref nodes);//bottom 
            SelectNode(start.x + 1, start.y - 1, ref nodes);//bottom right
            return nodes.ToArray();
        }

        private void SelectNode(int x, int y , ref List<Node> nodes)
        {
            if (ValidCoord(x, y)  && !openList.Contains(Nodes[x][y]) && !closeList.Contains(Nodes[x][y]))
            {
                nodes.Add(Nodes[x][y]);
            }
        }

        private bool ValidCoord(int topx, int topy)
        {
            return topx >= 0 && topx < 100 && topy >= 0 && topx < 100;
        }
    }
}
