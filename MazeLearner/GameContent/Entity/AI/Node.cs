using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.AI
{
    public sealed class Node
    {
        private Node _parent;
        private int _x;
        private int _y;
        private bool _passable;
        private bool _checked;
        private bool _opened;
        private int _g;
        private int _h;
        private int _f;
        public Node Parent
        {
            get { return _parent; } 
            set { _parent = value; }
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public bool Opened
        {
            get { return _opened; }
            set { _opened = value; }
        }
        public int G
        {
            get { return _g; }
            set { _g = value; }
        }
        public int H
        {
            get { return _h; }
            set { _h = value; }
        }
        public int F
        {
            get { return _f; }
            set { _f = value; }
        }
        public Node(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public void Reset()
        {
            this.G = 0;
            this.H = 0;
            this.F = 0;
            this.Parent = null;
            this.Opened = false;
        }
    }
}
