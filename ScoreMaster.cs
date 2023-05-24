using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    int playerScore;
    int compyScore;
    int lastPlayerScore;
    int lastCompyScore;
    int houseFoxScore;
    int houseCatScore;
    int houseDragonScore;
    int houseOwlScore;

    private static ScoreMaster instance;

    public static ScoreMaster Instance
    {
        get { return instance;}
    }

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            // instance = FindObjectOfType<ScoreMaster>();
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update() 
    {
        playerTotalText.text = playerScore.ToString();
        compyTotalText.text = compyScore.ToString();
        playerLastScoreText.text = lastPlayerScore.ToString();
        compyLastScoreText.text = lastCompyScore.ToString();
        foxScoreText.text = $"F: {houseFoxScore.ToString()}";
        owlScoreText.text = $"O: {houseOwlScore.ToString()}";
        dragonScoreText.text = $"{houseDragonScore.ToString()}: D";
        catScoreText.text = $"{houseCatScore.ToString()}: C";
        
    }

    private void Start() 
    {
        UpdatePlayerPrefs(); 
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
    public int HouseOwlScore
    {
        get { return houseOwlScore; }
        set { houseOwlScore += value; }
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
            case "Owl":
                houseOwlScore += amount;
                break;
        }
    }

    public void UpdatePlayerPrefs()
    {
        bool exists = PlayerPrefs.HasKey("FoxScore");

        if (!exists)
        {
            PlayerPrefs.SetInt("FoxScore", houseFoxScore);
            PlayerPrefs.SetInt("CatScore", houseCatScore);
            PlayerPrefs.SetInt("DragonScore", houseDragonScore);
            PlayerPrefs.SetInt("OwlScore", houseOwlScore);
        } else 
        {
            int tempFox = PlayerPrefs.GetInt("FoxScore");
            int tempCat = PlayerPrefs.GetInt("CatScore");
            int tempDragon = PlayerPrefs.GetInt("DragonScore");
            int tempOwl = PlayerPrefs.GetInt("OwlScore");

            tempFox += houseFoxScore;
            tempCat += houseCatScore;
            tempDragon += houseDragonScore;
            tempOwl += houseOwlScore;

            PlayerPrefs.SetInt("FoxScore", tempFox);
            PlayerPrefs.SetInt("CatScore", tempCat);
            PlayerPrefs.SetInt("DragonScore", tempDragon);
            PlayerPrefs.SetInt("OwlScore", tempOwl);

            houseFoxScore = tempFox;
            houseCatScore = tempCat;
            houseDragonScore = tempDragon;
            houseOwlScore = tempOwl;

            PlayerPrefs.Save();
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
