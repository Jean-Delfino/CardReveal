using System.Collections;

namespace Reuse.Utils
{
    public class UtilIEnumerator
    {
        public static IEnumerator WaitForFrames(int frameCount){
            while (frameCount > 0){
                frameCount--;
                yield return null; //returning 0 or null will make it wait 1 frame
            }
        }
    }
}