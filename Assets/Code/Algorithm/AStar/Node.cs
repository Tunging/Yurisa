using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Algorithm.AStar
{
    public class Node
    {
        public int x;
        public int y;

        public NodeType NodeType;

        public int F => G + H;
        public int G;
        public int H;

        //父节点
        public Node parent;

        public Node(int x, int y )
        {
            this.x = x;
            this.y = y;
        }
    }

    public enum NodeType
    {
        Normal,
        Wall,
        Water
    }
}
