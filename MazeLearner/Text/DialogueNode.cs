using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Text
{
    public class DialogueNode
    {
        public (int Set, int Index) Id;
        public string Text;
        public bool IsChoice;
        public List<DialogueChoice> Choices = new List<DialogueChoice>();

        public bool Empty => Text.IsEmpty() == true;
    }

    public class DialogueChoice
    {
        public string Text;
        public (int, int) Target;
    }
}
