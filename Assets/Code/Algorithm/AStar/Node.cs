using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        internal GameObject go;

        public Node(int x, int y ,NodeType nodeType)
        {
            this.x = x;
            this.y = y;
            NodeType = nodeType;
        }

        public override string ToString()
        {
            return $"x = {this.x}, y = {y}, F = {F}, NodeType = {NodeType} , parent = {parent}";
        }
    }

    public enum NodeType
    {
        Normal,
        Wall,
        Water
    }
}
