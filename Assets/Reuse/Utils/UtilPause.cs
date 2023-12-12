using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilPause
    {
        public static void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1f;
        }

    }
}