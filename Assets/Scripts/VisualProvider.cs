using UnityEngine;
using UnityEngine.UI;

public class VisualProvider : ScriptableObject
{
    private static VisualProvider _instance;

    public static VisualProvider Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load("VisualProvider") as VisualProvider;

            return _instance;
        }
        

    }
    
    [SerializeField] private Sprite[] starSprites;

    public Sprite GetStarSprite(int starIndex)
    {
        if (starIndex < 0 || starIndex > starSprites.Length) return null;
        
        return starSprites[starIndex];
    }
}