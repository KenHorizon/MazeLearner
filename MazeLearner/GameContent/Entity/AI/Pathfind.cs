using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MazeLearner.GameContent.Entity.AI
{
    public class Pathfind
    {
        private Main game;
        private Node[,] _nodes;
        private int _w;
        private int _h;
        public int W
        {
            get { return _w; }
            set { _w = value; }
        }
        public int H
        {
            get { return _h; }
            set { _h = value; }
        }
        public Node[,] Nodes
        {
            get { return _nodes; } 
            set { _nodes = value; }
        }
        private static readonly Vector2[] Directions =
        {
            new Vector2(0, -1),
            new Vector2(0, 1),
            new Vector2(-1, 0),
            new Vector2(1, 0)
        };

        public Pathfind(Main game, int w, int h)
        {
            this.game = game;
            this.W = w;
            this.H = h;
            this.Nodes = new Node[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    var initNode = new Node(x, y);
                    initNode.Passable = true;
                    this.Nodes[x, y] = initNode;
                }
        }
        public Node Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= this.W || y >= this.H)
                return null;

            return this.Nodes[x, y];
        }
        public void Reset()
        {
            for (int x = 0; x < this.W; x++)
                for (int y = 0; y < this.H; y++)
                    this.Nodes[x, y].Reset();

        }
        public List<Vector2> FindPath(Pathfind grid, Vector2 start, Vector2 end, Func<int, int, bool> isWalkable)
        {
            grid.Reset();

            var openList = new List<Node>(64);

            Node startNode = grid.Get((int)start.X, (int)start.Y);
            Node endNode = grid.Get((int)end.X, (int)end.Y);

            openList.Add(startNode);
            startNode.Opened = true;

            while (openList.Count > 0)
            {
                Node current = openList[0];
                for (int i = 1; i < openList.Count; i++)
                    if (openList[i].F < current.F)
                        current = openList[i];

                openList.Remove(current);
                current.Opened = false;

                if (current == endNode)
                    return Retrace(endNode);

                foreach (var dir in Directions)
                {
                    int nx = (int)(current.X + dir.X);
                    int ny = (int)(current.Y + dir.Y);

                    if (!isWalkable(nx, ny))
                        continue;

                    Node neighbor = grid.Get(nx, ny);
                    if (neighbor == null || neighbor.Opened == false)
                        continue;

                    int newG = current.G + 10;

                    if (!neighbor.Opened || newG < neighbor.G)
                    {
                        neighbor.G = newG;
                        neighbor.H = Manhattan(neighbor, endNode);
                        neighbor.F = neighbor.G + neighbor.H;
                        neighbor.Parent = current;

                        if (!neighbor.Opened)
                        {
                            openList.Add(neighbor);
                            neighbor.Opened = true;
                        }
                    }
                }
            }

            return null;
        }

        private int Manhattan(Node a, Node b)
        {
            return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y)) * 10;
        }

        private List<Vector2> Retrace(Node end)
        {
            var path = new List<Vector2>();
            Node current = end;

            while (current != null)
            {
                path.Add(new Vector2(current.X, current.Y));
                current = current.Parent;
            }

            path.Reverse();
            return path;
        }
    }
}
