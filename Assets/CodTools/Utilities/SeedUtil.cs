using System;
using System.Collections.Generic;

namespace CodTools.Utilities
{
    public static class SeedUtil
    {
        public static int HexToInt(string hex) {
            return Convert.ToInt32(hex, 16);
        }

        public static string IntToHex(int num) {
            return num.ToString("X");
        }

        public static Guid StringToGuid(string str) {
            bool success = System.Guid.TryParse(str, out Guid guid);
            if (success) {
                return guid;
            }
            throw new Exception();
        }

        public static string GuidToString(Guid guid) {
            return guid.ToString();
        }

        public static Guid GenerateSeed() {
            return System.Guid.NewGuid();
        }

        public static int GuidToInt(Guid guid) {
            return guid.GetHashCode();
        }

        public static void InitRandomWithSeed(Guid guid) {
            UnityEngine.Random.InitState(GuidToInt(guid));
        }

        public static void InitRandomWithSeed(int seed) {
            UnityEngine.Random.InitState(seed);
        }
    }
}