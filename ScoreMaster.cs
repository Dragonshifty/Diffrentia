using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMaster : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerTotalText;
    [SerializeField] TextMeshProUGUI compyTotalText;
    [SerializeField] TextMeshProUGUI playerLastScoreText;
    [SerializeField] TextMeshProUGUI compyLastScoreText;
    [SerializeField] TextMeshProUGUI foxScoreText;
    [SerializeField] TextMeshProUGUI owlScoreText;
    [SerializeField] TextMeshProUGUI dragonScoreText;
    [SerializeField] TextMeshProUGUI catScoreText;
    [SerializeField] Image quadrinityRemaining;
    
    int playerScore;
    int compyScore;
    int lastPlayerScore;
    int lastCompyScore;
    int houseFoxScore;
    int houseCatScore;
    int houseDragonScore;
    int houseFalconScore;

    private static ScoreMaster instance;

    public static ScoreMaster Instance
    {
        get { return instance;}
    }

    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }
    private void Update() 
    {
        playerTotalText.text = playerScore.ToString();
        compyTotalText.text = compyScore.ToString();
        playerLastScoreText.text = lastPlayerScore.ToString();
        compyLastScoreText.text = lastCompyScore.ToString();
        foxScoreText.text = houseFoxScore.ToString();
        owlScoreText.text = houseFalconScore.ToString();
        dragonScoreText.text = houseDragonScore.ToString();
        catScoreText.text = houseCatScore.ToString();
        
    }

    public int PlayerScore
    {
        get {return playerScore; }
        set {playerScore += value; }
    }

    public int CompyScore
    {
        get { return compyScore; }
        set { compyScore += value; }
    }

    public int LastPlayerScore
    {
        get { return lastPlayerScore; }
        set { lastPlayerScore = value; }
    }

    public int LastCompyScore
    {
        get { return lastCompyScore; }
        set { lastCompyScore = value; }
    }

    public int HouseFoxScore
    {
        get { return houseFoxScore; }
        set { houseFoxScore += value; }
    }

    public int HouseCatScore
    {
        get { return houseCatScore; }
        set { houseCatScore += value; }
    }
    public int HouseDragonScore
    {
        get { return houseDragonScore; }
        set { houseDragonScore += value; }
    }
    public int HouseFalconScore
    {
        get { return houseFalconScore; }
        set { houseFalconScore += value; }
    }
    
    public void HouseScore(string house, int amount)
    {
        switch(house)
        {
            case "Fox":
                houseFoxScore += amount;
                break;
            case "Cat":
                houseCatScore += amount;
                break;
            case "Dragon":
                houseDragonScore += amount;
                break;
            case "Falcon":
                houseFalconScore += amount;
                break;
        }
    }


    public void ResetScores()
    {
        playerScore = 0;
        compyScore = 0;
        lastPlayerScore = 0;
        lastCompyScore = 0;
    }
}
