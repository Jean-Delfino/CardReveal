using System;
using System.Collections.Generic;
using System.Linq;
using SaveGame;
using UI;
using UnityEngine;

public enum ScoreType
{
    None, //Safety first
    LuckyReveal,
    MemoryReveal,
    MemoryMiss,
    Miss,
}

public enum WinScoreType
{
    None = 0,
    Bronze = 1,
    Silver = 2,
    Gold = 3,
}
public class CardScoreController : MonoBehaviour
{
    [Serializable]
    public class ScoreByType
    {
        public ScoreType type;
        public float score;
    }

    [Serializable]
    public class WinScoreTypeLimited
    {
        public WinScoreType type;
        [Range(0,1)] public float min;
        [Range(0,1)] public float max;
    }
    
    [Space] [Header("SCORE CONFIGURATION")] [Space]
    
    [SerializeField] private List<ScoreByType> typePoints;
    [SerializeField] private List<WinScoreTypeLimited> winsType;
    [SerializeField] private float minimumUntilAutoLose = -800;
    
    private float _score;
    private float _maxScoreType;
    private float _maxScore;
    
    [Space] [Header("UI CONFIGURATION")] [Space] 
    [SerializeField] private CardScoreControllerUI scoreUI;

    //Save the reveals
    private Dictionary<ScoreType, int> _reveals = new();
    private void Awake()
    {
        foreach (var scoreType in (ScoreType[])Enum.GetValues(typeof(ScoreType)))
        {
            _reveals.Add(scoreType, 0);
        }
        
        _maxScoreType = typePoints.Max(item => item.score);
    }

    public void ResetScore()
    {
        foreach (var key in _reveals.Keys.ToList())
        {
            _reveals[key] = 0;
        }

        _score = 0;
        
        scoreUI.gameObject.SetActive(true);
        scoreUI.ResetScore(0);
    }

    public void SetMaxScore(int cardCombinationAmount)
    {
        _maxScore = _maxScoreType * cardCombinationAmount;
    }

    public bool AddScore(ScoreType type)
    {
        var typeScore = typePoints.Find(e => e.type == type);
        
        if(typeScore == null) return false;
        
        _score += typeScore.score;

        _reveals[type]++;
        scoreUI.ShowTextScore(_score);
        scoreUI.ShowScoreType(type);
        
        return _score < minimumUntilAutoLose;
    }

    public void EndGame(CardScoreEndGameUI ui)
    {
        scoreUI.gameObject.SetActive(false);

        if (_score < 0) return;
        var star = FindStars();
        
        CardManager.Instance.SetStars(star);
        ui.Setup(star, _score, _reveals);
    }

    private int FindStars()
    {
        var percentage = _score / _maxScore;

        foreach (var score in winsType)
        {
            if (percentage >= score.min && percentage < score.max) return ((int) score.type);
        }

        return -1;
    }
}
