using MazeLearner.GameContent.BattleSystems;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystem
{
    public enum BattleResult
    {
        None,
        Draw,
        Win,
        Defeat
    }

    public class BattleSystem
    {
        private PlayerEntity _player;
        private SubjectEntity _enemy;
        private NpcCategory _npcCategory;
        
        public PlayerEntity Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public SubjectEntity Enemy
        {
            get { return _enemy; }
            set { _enemy = value; }
        }
        public NpcCategory NpcCategory
        {
            get{ return _npcCategory; }
            set { _npcCategory = value; }
        }

        public void Start()
        {

        }
    }
}
