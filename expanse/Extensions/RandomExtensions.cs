using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expanse.Extensions
{
    public static class RandomExtensions
    {

        /// <summary>
        /// Chooses a random item from the sequence of items passed in. 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="rand">The random number generator to use.</param>
        /// <param name="seq">The list to select an item from.</param>
        /// <returns>The random item that was selected.</returns>
        public static T Choose<T>(this Random rand, IList<T> items)
        {
            return items[rand.Next(0, items.Count - 1)];
        }

        /// <summary>
        /// Uses a weighted random algorithm to choose an item from the sequence of items passed in. 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="rand">The random number generator to use.</param>
        /// <param name="seq">The sequence to select an item from.</param>
        /// <param name="weightSelector">The expression to use to select a weight for each item.</param>
        /// <returns>The item selected via the weighted random algorithm.</returns>
        public static TItem Choose<TItem>(this Random rand, IEnumerable<TItem> seq, Func<TItem, double> weightSelector)
        {
            return Choose(rand, seq, weightSelector, seq.Sum(it=>weightSelector(it)));
        }

        /// <summary>
        /// Uses a weighted random algorithm to choose an item from the sequence of items passed in.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="rand">The random number generator to use.</param>
        /// <param name="seq">The sequence to select an item from.</param>
        /// <param name="weightSelector">The expression to use to select a weight for each item.</param>
        /// <param name="totalWeight">The total weight of all the items in the sequence.</param>
        /// <returns>The item selected via the weighted random algorithm.</returns>
        public static TItem Choose<TItem>(this Random rand, IEnumerable<TItem> seq, Func<TItem, double> weightSelector, double totalWeight)
        {
            //adapted from an answer here: http://stackoverflow.com/questions/56692/random-weighted-choice
            double itemWeightIndex = rand.NextDouble() * totalWeight;
            double currentWeightIndex = 0;

            foreach (var item in seq)
            {
                var weight = weightSelector(item);

                currentWeightIndex += weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return item;
            }

            return default(TItem);
        }
    }
}
