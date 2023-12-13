using Reuse.Utils;
using UnityEngine;

namespace Layout
{
    [CreateAssetMenu(fileName = "LevelDefinition", menuName = "CardGameDesign/Level Definition")]
    public class LevelDefinition : ScriptableObject
    {
        [SerializeField] private Texture2D level;
        [SerializeField] private Color notCardColor;
        [SerializeField] private bool includeEmptyColumns = false;
    
        [Space] [Header("CARD ART STYLE")] [Space]

        [SerializeField] private Sprite backCardPattern;
        [SerializeField] private Sprite[] frontCardPattern;

        [Space] [Header("CARD SPACING")] [Space] 
    
        [SerializeField] private float xSpacing = 1.5f;
        [SerializeField] private float ySpacing = 2.2f;
    
        [Space] [Header("CARD STARTING POINT")] [Space] 
    
        [SerializeField] private float xStartingPoint = -0.5f;
        [SerializeField] private float yStartingPoint = 1f;

        [Space] [Header("SAVE KEY")] [Space] 
        
        [SerializeField] private string saveName;
        public (float x, float y) GetSpacings()
        {
            return (xSpacing, ySpacing);
        }
    
        public (float x, float y) GetStartingPoints()
        {
            return (xStartingPoint, yStartingPoint);
        }
    
        public float GetStartingPointX()
        {
            return xStartingPoint;
        }
    
        public float GetStartingPointY()
        {
            return yStartingPoint;
        }

        public UtilImages.ImageResult GetLevelLayout()
        {
            return UtilImages.GetDifferentPixelsThenColor(level, notCardColor, includeEmptyColumns);
        }

        public Sprite GetBackImage()
        {
            return backCardPattern;
        }

        public Sprite GetFaceImage(int index)
        {
            return frontCardPattern[index];
        }

        public int GetFaceImagesAmount()
        {
            return frontCardPattern.Length;
        }

        public string GetSaveName()
        {
            return saveName;
        }
    }
}

