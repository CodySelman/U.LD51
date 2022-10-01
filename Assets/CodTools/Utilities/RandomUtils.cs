using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodTools.Utilities
{
    public static class RandomUtils
    {
       public static List<T> Shuffle<T>(List<T> t) {
            // Loops through array
            for (int i = t.Count - 1; i > 0; i--)
            {
                // Randomize a number between 0 and i (so that the range decreases each time)
                int rnd = UnityEngine.Random.Range(0, i);
                // Save the value of the current i, otherwise it'll overwrite when we swap the values
                T temp = t[i];
                // Swap the new and old values
                t[i] = t[rnd];
                t[rnd] = temp;
            }
            return t;
        }

        public static T GetRandomItem<T>(List<T> list) {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}