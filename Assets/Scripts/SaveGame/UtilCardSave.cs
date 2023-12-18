using UnityEngine;

namespace SaveGame
{
    public static class UtilCardSave
    {
        public static int GetStarAmount(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetStartAmount(string key, int value)
        {
            if (GetStarAmount(key) > value) return;
            
            PlayerPrefs.SetInt(key, value);
        }
    }
}