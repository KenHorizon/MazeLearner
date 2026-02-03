using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Data
{
    public class PlayerFileData : FileData
    {
        private PlayerEntity _player;
        public PlayerEntity Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                if (value != null)
                {
                    Name = _player.langName;
                }
            }
        }
        public FileMetadata MetaData;
        public PlayerFileData() : base("Player")
        {
        }
        public PlayerFileData(string path)
            : base("Player", path)
        {
        }
        public static PlayerFileData CreateAndSave(PlayerEntity player)
        {
            PlayerFileData playerFileData = new PlayerFileData();
            playerFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.Player);
            playerFileData.Player = player;
            playerFileData._path = Main.GetPlayerPathFromName(player.langName);
            PlayerEntity.SavePlayer(playerFileData);
            return playerFileData;
        }
        public override void MoveToCloud()
        {

        }

        public override void MoveToLocal()
        {

        }

        public override void SetAsActive()
        {
            Main.ActivePlayerFileData = this;
            Main.Players[Main.MyPlayer] = Player;
        }
    }
}
