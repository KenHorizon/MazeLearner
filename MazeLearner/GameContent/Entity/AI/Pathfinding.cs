using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MazeLearner.GameContent.Entity.AI
{
    public class Pathfinding
    {
        private Main game;

        public class PathNode
        {
            public int X;
            public int Y;

            public int G;
            public int H;
            public int F;

            public bool Open;
            public bool Checked;
            public bool Walkable;

            public PathNode Parent;

            public PathNode(int col, int row)
            {
                X = col;
                Y = row;
            }
        }

        private PathNode[,] _nodes;
        private List<PathNode> _openList = new List<PathNode>();
        public List<PathNode> PathList = new List<PathNode>();

        private PathNode startNode;
        private PathNode goalNode;
        private PathNode currentNode;

        private bool goalReached = false;
        private int step = 0;

        public Pathfinding(Main game)
        {
            this.game = game;
            InstantiateNodes();
        }

        private void InstantiateNodes()
        {
            this._nodes = new PathNode[Main.Tiled.Width, Main.Tiled.Height];
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    this._nodes[col, row] = new PathNode(col, row);
                }
            }
        }

        private void Reset()
        {
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    PathNode node = _nodes[col, row];
                    node.Open = false;
                    node.Checked = false;
                    node.Walkable = false;
                    node.Parent = null;
                }
            }

            _openList.Clear();
            PathList.Clear();
            goalReached = false;
            step = 0;
        }

        public void SetNodes(Vector2 startWorldPos, Vector2 goalWorldPos)
        {
            this.Reset();
            int startCol = (int)(startWorldPos.ToPoint().X / Main.TileSize);
            int startRow = (int)(startWorldPos.ToPoint().Y / Main.TileSize);
            int goalCol = (int)(goalWorldPos.ToPoint().X / Main.TileSize);
            int goalRow = (int)(goalWorldPos.ToPoint().Y / Main.TileSize);
            this.startNode = _nodes[startCol, startRow];
            this.goalNode = _nodes[goalCol, goalRow];
            this.currentNode = startNode;
            this._openList.Add(currentNode);
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    if (Main.Tiled.IsWalkable(new Vector2(col, row)) == true)
                    {
                        this._nodes[col, row].Walkable = true;
                    }
                    this.ComputeCost(this._nodes[col, row]);
                }
            }
        }

        private void ComputeCost(PathNode node) 
        {
            int xDistance = Math.Abs(node.X - startNode.X);
            int yDistance = Math.Abs(node.Y - startNode.Y);
            node.G = xDistance + yDistance;
            xDistance = Math.Abs(node.X - goalNode.X);
            yDistance = Math.Abs(node.Y - goalNode.Y);
            node.H = xDistance + yDistance;
            node.F = node.G + node.H;
        }

        public bool Search()
        {
            while (this.goalReached == false && this.step < 500)
            {
                int col = this.currentNode.X;
                int row = this.currentNode.Y;
                this.currentNode.Checked = true;
                this._openList.Remove(currentNode);
                if (row - 1 >= 0)
                {
                    this.OpenNode(this._nodes[col, row - 1]);
                }
                if (col - 1 >= 0)
                {
                    this.OpenNode(this._nodes[col - 1, row]);
                }
                if (row + 1 < Main.Tiled.Height)
                {
                    this.OpenNode(this._nodes[col, row + 1]);
                }
                if (col + 1 < Main.Tiled.Width)
                {
                    this.OpenNode(this._nodes[col + 1, row]);
                }
                if (this._openList.Count == 0) return false;
                int bestIndex = 0;
                int bestFCost = int.MaxValue;
                for (int i = 0; i < _openList.Count; i++)
                {
                    if (this._openList[i].F < bestFCost)
                    {
                        bestFCost = _openList[i].F;
                        bestIndex = i;
                    }
                    else if (this._openList[i].F == bestFCost && this._openList[i].G < this._openList[bestIndex].G)
                    {
                        bestIndex = i;
                    }
                }
                this.currentNode = this._openList[bestIndex];
                if (this.currentNode == this.goalNode)
                {
                    this.goalReached = true;
                    this.TrackThePath();
                }
                this.step++;
            }

            return this.goalReached;
        }

        private void OpenNode(PathNode node)
        {
            if (node.Open == false && node.Checked == false && node.Walkable == true)
            {
                //Loggers.Debug($"Node opened at {node.Col} {node.Row}");
                node.Open = true;
                node.Parent = this.currentNode;
                this._openList.Add(node);
            }
        }

        private void TrackThePath()
        {
            PathNode current = this.goalNode;
            while (current != this.startNode)
            {
                this.PathList.Insert(0, current);
                current = current.Parent;
            }
        }
    }
}
