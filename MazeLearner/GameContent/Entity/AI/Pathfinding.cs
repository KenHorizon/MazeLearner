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
            public int Col;
            public int Row;

            public int G;
            public int H;
            public int F;

            public bool Open;
            public bool Checked;
            public bool Walkable;

            public PathNode Parent;

            public PathNode(int col, int row)
            {
                Col = col;
                Row = row;
            }
        }

        private PathNode[,] nodes;
        private List<PathNode> openList = new List<PathNode>();
        public List<PathNode> pathList = new List<PathNode>();

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
            this.nodes = new PathNode[Main.Tiled.Width, Main.Tiled.Height];
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    this.nodes[col, row] = new PathNode(col, row);
                }
            }
        }

        private void Reset()
        {
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    PathNode node = nodes[col, row];
                    node.Open = false;
                    node.Checked = false;
                    node.Walkable = false;
                    node.Parent = null;
                }
            }

            openList.Clear();
            pathList.Clear();
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
            this.startNode = nodes[startCol, startRow];
            this.goalNode = nodes[goalCol, goalRow];
            this.currentNode = startNode;
            this.openList.Add(currentNode);
            for (int col = 0; col < Main.Tiled.Width; col++)
            {
                for (int row = 0; row < Main.Tiled.Height; row++)
                {
                    if (Main.Tiled.IsWalkable(new Vector2(col, row)) == false)
                    {
                        this.nodes[col, row].Walkable = true;
                    }
                    this.ComputeCost(this.nodes[col, row]);
                }
            }
        }

        private void ComputeCost(PathNode node) 
        {
            int xDistance = Math.Abs(node.Col - startNode.Col);
            int yDistance = Math.Abs(node.Row - startNode.Row);
            node.G = xDistance + yDistance;
            xDistance = Math.Abs(node.Col - goalNode.Col);
            yDistance = Math.Abs(node.Row - goalNode.Row);
            node.H = xDistance + yDistance;
            node.F = node.G + node.H;
        }

        public bool Search()
        {
            while (this.goalReached == false && this.step < 500)
            {
                int col = this.currentNode.Col;
                int row = this.currentNode.Row;
                this.currentNode.Checked = true;
                this.openList.Remove(currentNode);
                if (row - 1 >= 0)
                {
                    this.OpenNode(this.nodes[col, row - 1]);
                }
                if (col - 1 >= 0)
                {
                    this.OpenNode(this.nodes[col - 1, row]);
                }
                if (row + 1 < Main.Tiled.Height)
                {
                    this.OpenNode(this.nodes[col, row + 1]);
                }
                if (col + 1 < Main.Tiled.Width)
                {
                    this.OpenNode(this.nodes[col + 1, row]);
                }
                if (this.openList.Count == 0) return false;
                int bestIndex = 0;
                int bestFCost = int.MaxValue;
                for (int i = 0; i < openList.Count; i++)
                {
                    if (this.openList[i].F < bestFCost)
                    {
                        bestFCost = openList[i].F;
                        bestIndex = i;
                    }
                    else if (this.openList[i].F == bestFCost && this.openList[i].G < this.openList[bestIndex].G)
                    {
                        bestIndex = i;
                    }
                }
                this.currentNode = this.openList[bestIndex];
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
                this.openList.Add(node);
            }
        }

        private void TrackThePath()
        {
            PathNode current = this.goalNode;
            while (current != this.startNode)
            {
                this.pathList.Insert(0, current);
                current = current.Parent;
            }
        }
    }
}
