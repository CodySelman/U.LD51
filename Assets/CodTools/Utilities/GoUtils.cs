using UnityEngine;

namespace CodTools.Utilities
{
    public static class GoUtils 
    {
        /// <summary>
        /// Return true if GameObject is null. This method is necessary because Serialized GameObjects and Components
        /// that inherit from MonoBehaviour will return true for <code>go is not null</code> if checking against inspector 
        /// variables that are unassigned.
        /// </summary>
        /// <param name="go"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Bool value indicating if game object is null</returns>
        public static bool IsGameObjectNull<T>(T go) {
            if (go is not Object obj) return go is null;
            return !obj;
        }
    }
}
