using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class CollectableItemBuilder
    {
        private static int ID;

        public static void Register()
        {
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Creator Signature", "2 R and 1 V, Thank you for all the support!"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Kenny", "A letter from one of the dev"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Cookie Monster", "Cookie monster? probably you know the reference"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Cat Plushie", "A cute cat plushie"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Puppy Plushie", "A cute puppy plushie"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Math Medal", "Congratulation for beating the math"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "English Medal", "Congratulation for beating the math"));
            CollectableItems.CollectableItem.Add(CollectableItems.Create(CreateId(), "Science Medal", "Congratulation for beating the science"));
        }

        private static int CreateId()
        {
            return ID++;
        }
    }
}
