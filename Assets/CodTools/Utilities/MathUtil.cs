using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodTools.Utilities
{
    public static class MathUtil
    {
        public static Vector3 Vec3Average(List<Vector3> vecs) {
            Vector3 averageVec = Vector3.zero;
            if (vecs.Count == 1) {
                averageVec = vecs[0];
            }
            else {
                foreach (Vector3 v in vecs) {
                    averageVec += v;
                }
                averageVec /= vecs.Count;
            }

            return averageVec;
        }
    }
}
