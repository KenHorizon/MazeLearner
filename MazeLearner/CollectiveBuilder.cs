using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class CollectiveBuilder
    {
        private static int ID;

        public static void Register()
        {
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Creator Signature", "Thank you for all the support!"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Kenny", "A letter from one of the dev"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Cookie Monster", "Cookie monster? probably you know the reference"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Cat Plushie", "A cute cat plushie"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Puppy Plushie", "A cute puppy plushie"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "67", "Six Seveeennn!!!!"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Fighter Medal", "Congratulation for beating the maze"));
            CollectiveItems.CollectableItem.Add(CollectiveItems.Create(CreateId(), "Honor Reward", "Congratulation for beating the game"));
        }

        private static int CreateId()
        {
            return ID++;
        }
    }
}
