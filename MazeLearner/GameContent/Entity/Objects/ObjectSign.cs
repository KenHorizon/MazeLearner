using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectSign : ObjectEntity
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            foreach (var kv in ObjectSign.EncodeMessage(this.Message))
            {
                this.Dialogs[kv.Key] = kv.Value;
            }
        }
        
        public static Dictionary<int, string> EncodeMessage(string input)
        {
            var result = new Dictionary<int, string>();

            var matches = Regex.Matches(input, @"\[(\d+)\]\s*([^\[]*)");

            foreach (Match match in matches)
            {
                int index = int.Parse(match.Groups[1].Value);
                string value = match.Groups[2].Value.Trim();

                result[index] = value;
            }

            return result;
        }
    }
}
