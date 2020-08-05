using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Algorithm.AStar
{
    public class AStarManager : BaseManager<AStarManager>
    {
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();

        public Node[,] Nodes;

        int width = 5;
        int height = 5;

        public void InitMapInfo(int width , int height)
        {
            this.width = width;
            this.height = height;

            Nodes = new Node[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Node node = new Node(i, j, UnityEngine.Random.Range(0,100)<20?NodeType.Wall:NodeType.Normal);
                    Nodes[i, j] = node;
                }
            }
        }
        Node startNode;

        public List<Node> FindPath(UnityEngine.Vector2 startPos, Vector2 endPos)
        {
            if (startPos.x < 0 || startPos.x > width ||
                startPos.y < 0 || startPos.y > height ||
                endPos.x < 0 || endPos.x > width ||
                endPos.y < 0 || endPos.y > height)
            {
                return null;
            }

            startNode = Nodes[(int)startPos.x, (int)startPos.y];
            Node endNode = Nodes[(int)endPos.x, (int)endPos.y];
            if (startNode == null || startNode.NodeType == NodeType.Wall
                || endNode == null || endNode.NodeType == NodeType.Wall)
            {
                return null;
            }

            closeList.Clear();
            openList.Clear();

            startNode.parent = null;
            startNode.G = 0;
            startNode.H = 0;
            closeList.Add(startNode);

            while (true)
            {
                int x = startNode.x;
                int y = startNode.y;
                AddNode(x, y + 1, 10, startNode, endNode);
                AddNode(x + 1, y + 1, 14, startNode, endNode);
                AddNode(x - 1, y + 1, 14, startNode, endNode);
                AddNode(x, y - 1, 10, startNode, endNode);
                AddNode(x + 1, y - 1, 14, startNode, endNode);
                AddNode(x - 1, y - 1, 14, startNode, endNode);
                AddNode(x + 1, y, 10, startNode, endNode);

                AddNode(x - 1, y, 10, startNode, endNode);

                openList.Sort(CompareNode);

                Node next = openList[0];
                closeList.Add(next);
                startNode = next;
                openList.Remove(next);
                if (startNode == endNode)
                {
                    List<Node> result = new List<Node>();
                    result.Add(startNode);
                    while (startNode.parent != null)
                    {
                        result.Add(startNode.parent);
                        startNode = startNode.parent;
                    }
                    result.Reverse();
                    return result;
                }

                if (openList.Count==0)
                {
                    return null;
                }
            }
        }

        private int CompareNode(Node x, Node y)
        {
            if (x.parent == startNode)
            {
                if (y.parent != startNode)
                {
                    return -1;
                }
            }
            else
            {
                if (y.parent == startNode)
                {
                    return 1;
                }
            }

            return x.F.CompareTo(y.F);
        }

        public void AddNode(int x, int y, int G, Node parentNode, Node endNode)
        {
            try
            {
                if (x<0 || x >= width || y <0 || y >= height)
                {
                    return;
                }

                Node node = Nodes[x, y];
                if (node == null)
                {
                    return;
                }

                if (node.NodeType != NodeType.Normal || openList.Contains(node) || closeList.Contains(node))
                {
                    return;
                }
                node.parent = parentNode;

                node.G = node.parent == null ? 0:node.parent.G + G;
                node.H = Mathf.Abs(x - endNode.x) + Mathf.Abs(y - endNode.y);
                openList.Add(node);
            }
            catch (Exception ex)
            {
                Debug.Log($"{ex.Message}    {x}   {y}");
            }

        }
    }
}
