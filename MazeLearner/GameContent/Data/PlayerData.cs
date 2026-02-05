using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Data
{
    [Serializable]
    public class PlayerData
    {
        int health;
        int damage;
        int armor;
        float posX;
        float posY;
        Gender gender;
        PlayerState playerState;
        string name;

        string path;
        PlayerEntity playerEntity;
        public PlayerData(string path)
        {
            this.path = path;
        }
    }
}
