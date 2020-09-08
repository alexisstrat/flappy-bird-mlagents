using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highScore;

    [SerializeField] private TMP_Text currentScore;

    private int _currentScore = 0;
    private int _highScore = 0;

    
    private static GameManager _instance;

    #region Singleton
    public static GameManager Get
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }
    #endregion

    public void UpdateScore()
    {
        _currentScore++;
        currentScore.text = _currentScore.ToString();
    }

    public void ResetScore()
    {
        CheckForHighscore();
        _currentScore = 0;
        currentScore.text = _currentScore.ToString();
    }

    private void CheckForHighscore()
    {
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            highScore.text = _highScore.ToString();
        }
    }
}
