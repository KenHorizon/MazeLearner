using Assimp;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MazeLearner.GameContent.Entity.AI
{
    public class Pathfind
    {
        private Main game;
        private PathNode[][] _nodes;
        private PathNode _goalNode;
        private PathNode _startNode;
        private PathNode _currentNode;
        private List<PathNode> _openList = new List<PathNode>();
        public PathNode[] PathList = new PathNode[1];
        private int step;
        public bool GoalReached = false;
        
        public PathNode[][] Nodes
        {
            get { return _nodes; } 
            set { _nodes = value; }
        }
        public Pathfind(Main game)
        {
            this.game = game;
            this.Initialize();
        }

        private void Initialize()
        {
            this.Nodes = new PathNode[Main.MaxScreenCol][];
            for (int i = 0; i < this.Nodes.Length; i++)
            {
                this.Nodes[i] = new PathNode[Main.MaxScreenRow];
            }
            int x = 0;
            int y = 0;
            while (x < Main.MaxScreenCol && y < Main.MaxScreenRow)
            {
                this.Nodes[x][y] = new PathNode(x, y);
                x++;
                if (y == Main.MaxScreenRow)
                {
                    x = 0;
                    y = 0;
                }
            }
        }
        public void Reset()
        {
            int x = 0;
            int y = 0;
            while (x < Main.MaxScreenCol && y < Main.MaxScreenRow)
            {
                this.Nodes[x][y].Opened = false;
                this.Nodes[x][y].Checked = false;
                this.Nodes[x][y].Walkable = false;
                x++;
                if (y == Main.MaxScreenRow)
                {
                    x = 0;
                    y = 0;
                }
            }
            this._openList.Clear();
            Array.Clear(this.PathList, 0, this.PathList.Length);
            this.step = 0;
            this.GoalReached = false;
        }
        public void SetNodes(Vector2 targetPosition, NPC npc)
        {
            this.Reset();
            Vector2 position = npc.Position;
            int TPX = targetPosition.ToPoint().X;
            int TPY = targetPosition.ToPoint().Y;
            int SPX = position.ToPoint().X;
            int SPY = position.ToPoint().Y;
            this._startNode = this.Nodes[SPX][SPY];
            this._currentNode = this._startNode;
            this._goalNode = this.Nodes[TPX][TPY];

            int x = 0;
            int y = 0;
            while (x < Main.MaxScreenCol && y < Main.MaxScreenRow)
            {
                this.GetCost(this.Nodes[x][y]);
                x++;
                this.Nodes[x][y].Walkable = Main.Tiled.IsWalkable(new Vector2(x, y));
                if (y == Main.MaxScreenRow)
                {
                    x = 0;
                    y = 0;
                }
            }
        }

        private void GetCost(PathNode pathNode)
        {
            int distanceX = Math.Abs(pathNode.X - this._startNode.X);
            int distanceY = Math.Abs(pathNode.Y - this._startNode.Y);
            pathNode.G = distanceX + distanceY;
            distanceX = Math.Abs(pathNode.X - this._goalNode.X);
            distanceY = Math.Abs(pathNode.Y - this._goalNode.Y);
            pathNode.H = distanceX + distanceY;
            pathNode.F = pathNode.G + pathNode.H;
        }

        public bool Search()
        {
            while (true)
            {
                if (this.GoalReached == false && this.step < 500)
                {
                    int x = this._currentNode.X;
                    int y = this._currentNode.Y;
                    this._currentNode.Checked = true;
                    this._openList.Remove(this._currentNode);
                    if (y - 1 >= 0)
                    {
                        this.OpenNode(this.Nodes[x][y - 1]);
                    }
                    if (y - 1 >= 0)
                    {
                        this.OpenNode(this.Nodes[x - 1][y]);
                    }
                    if (x + 1 < Main.MaxScreenRow)
                    {
                        this.OpenNode(this.Nodes[x][y + 1]);
                    }
                    if (y + 1 < Main.MaxScreenCol)
                    {
                        this.OpenNode(this.Nodes[x + 1][y]);
                    }
                    int bestNodeIndex = 0;
                    int bestNodeFCost = 999;
                    for (int i = 0; i < this._openList.Count; i++)
                    {
                        if (((PathNode)this._openList[i]).F < bestNodeFCost)
                        {
                            bestNodeIndex = i;
                            bestNodeFCost = ((PathNode)this._openList[i]).F;
                        } else if (((PathNode)this._openList[i]).F == bestNodeFCost && ((PathNode)this._openList[i]).G < ((PathNode)this._openList[bestNodeIndex]).G)
                        {
                            bestNodeIndex = i;
                        }
                    }
                    if (this._openList.Count != 0)
                    {
                        this._currentNode = (PathNode)this._openList[bestNodeIndex];
                        if (this._currentNode == this._goalNode)
                        {
                            this.GoalReached = true;
                            this.TrackThePath();
                        }

                        ++this.step;
                        continue;
                    }
                }
                return this.GoalReached;
            }
        }

        private void TrackThePath()
        {
            for (var node = this._goalNode; node != this._startNode; node = node.Parent)
            {
                this.PathList[0] = node;
            }
        }

        public void OpenNode(PathNode pathNode)
        {
            if (pathNode.Opened == false && pathNode.Checked == false && pathNode.Walkable == false)
            {
                pathNode.Opened = true;
                pathNode.Parent = this._currentNode;
                this._openList.Add(pathNode);
            }
        }
    }
}
