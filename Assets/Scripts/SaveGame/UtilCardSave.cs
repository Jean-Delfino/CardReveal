using UnityEngine;

namespace SaveGame
{
    public static class UtilCardSave
    {
        private const string SfxCardSave = "sfx_card_save_key";
        private const string MusicCardSave = "music_card_save_key";
        private const string LanguageCardSave = "language_card_save_key";
        private const string SensibilityCardSave = "sensibility_card_save_key";
        
        public static int GetStarAmount(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetStartAmount(string key, int value)
        {
            if (GetStarAmount(key) >= value) return;
            
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        public static float LoadSfx(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(SfxCardSave, defaultValue);
        }
        public static float LoadMusic(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(MusicCardSave, defaultValue);
        }
        public static int LoadLanguage()
        {
            return PlayerPrefs.GetInt(LanguageCardSave, -1); //No saved language
        }
        public static float LoadSensibility(float defaultValue = 0.5f)
        {
            return PlayerPrefs.GetFloat(SensibilityCardSave, defaultValue);
        }
        
        public static void SaveSfx(float value)
        {
            PlayerPrefs.SetFloat(SfxCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveMusic(float value)
        {
            PlayerPrefs.SetFloat(MusicCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveLanguage(int value)
        {
            PlayerPrefs.SetInt(LanguageCardSave, value);
            PlayerPrefs.Save();
        }
        public static void SaveSensibility(float value)
        {
            PlayerPrefs.SetFloat(SensibilityCardSave, value);
            PlayerPrefs.Save();
        }
    }
}