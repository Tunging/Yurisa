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

        Node[,] Nodes;

        int width = 100;
        int height = 100;

        public void InitMapInfo()
        {
            Nodes = new Node[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Node node = new Node(i, j, NodeType.Normal);
                    Nodes[i, j] = node;
                }
            }
        }

        public List<Node> FindPath(UnityEngine.Vector2 startPos, Vector2 endPos)
        {
            if (startPos.x < 0 || startPos.x > width ||
                startPos.y < 0 || startPos.y > height ||
                endPos.x < 0 || endPos.x > width ||
                endPos.y < 0 || endPos.y > height)
            {
                return null;
            }

            Node startNode = Nodes[(int)startPos.x, (int)startPos.y];
            Node endNode = Nodes[(int)endPos.x, (int)endPos.y];
            if (startNode == null || startNode.NodeType == NodeType.Wall
                || endNode == null || endNode.NodeType == NodeType.Wall)
            {
                return null;
            }

            startNode.parent = null;
            startNode.G = 0;
            startNode.H = 0;

            int x = startNode.x;
            int y = startNode.y;
            AddNode(x,y+1,10, startNode, endNode);
            AddNode(x+1,y+1,14, startNode, endNode);
            AddNode(x-1,y+1,14, startNode, endNode);
            AddNode(x,y-1,10, startNode, endNode);
            AddNode(x+1,y-1,14, startNode, endNode);
            AddNode(x-1,y-1,14, startNode, endNode);
            AddNode(x+1,y,10, startNode, endNode);
            AddNode(x-1,y,10, startNode, endNode);

            openList.Sort(CompareNode);

        }

        private int CompareNode(Node x, Node y)
        {
            return x.F.CompareTo(y.F);
        }

        public void AddNode(int x ,int y , int G,Node parentNode,Node endNode)
        {
            Node node = Nodes[x, y];
            if (node == null || node == endNode)
            {
                return;
            }

            if (openList .Contains(node) || closeList.Contains(node))
            {
                return;
            }
            node.G = node.parent.G + G;
            node.H = Mathf.Abs(x - endNode.x)+Mathf.Abs( y - endNode.y);
            openList.Add(node);

            node.parent = parentNode;
        }
    }
}
