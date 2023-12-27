using UnityEngine;

namespace SaveGame
{
    public static class UtilCardSave
    {
        private static string sfxCardSave = "sfx_card_save_key";
        private static string musicCardSave = "music_card_save_key";
        private static string languageCardSave = "language_card_save_key";
        private static string sensibilityCardSave = "sensibility_card_save_key";
        public static int GetStarAmount(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetStartAmount(string key, int value)
        {
            if (GetStarAmount(key) > value) return;
            
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        public static float LoadSfx(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(sfxCardSave, defaultValue);
        }
        public static float LoadMusic(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(musicCardSave, defaultValue);
        }
        public static int LoadLanguage()
        {
            return PlayerPrefs.GetInt(languageCardSave, 0);
        }
        public static float LoadSensibility(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(sensibilityCardSave, defaultValue);
        }
        
        public static void SaveSfx(float value)
        {
            PlayerPrefs.SetFloat(sfxCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveMusic(float value)
        {
            PlayerPrefs.SetFloat(musicCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveLanguage(int value)
        {
            PlayerPrefs.SetInt(languageCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveSensibility(float value)
        {
            PlayerPrefs.SetFloat(sensibilityCardSave, value);
            PlayerPrefs.Save();
        }
    }
}