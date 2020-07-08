using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Algorithm.AStar
{
    public class AStarEngine
    {
        int maxRow, maxCol = 100;
        Node[][] Nodes;
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

        static AStarEngine instance;
        public static AStarEngine Instance { get {
                if (instance == null)
                {
                    instance = new AStarEngine();
                }
                return instance;
            } }


    }
}
