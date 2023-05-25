using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDataTransfer : MonoBehaviour
{
    public static ScoreDataTransfer Instance { get; set; }

    private int playerScore;
    private int compyScore;
    private int foxScore;
    private int catScore;
    private int dragonScore;
    private int falconScore;
    private int winAmount = 10;
    private string clan= "";

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
    }

    public int PlayerScore 
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    public int CompyScore
    {
        get { return compyScore; }
        set { compyScore = value; }
    }

    public int FoxScore
    {
        get { return foxScore; }
        set { foxScore = value; }
    }

    public int CatScore
    {
        get { return catScore; }
        set { catScore = value; }
    }

    public int DragonScore
    {
        get { return dragonScore; }
        set { dragonScore = value; }
    }

    public int FalconScore
    {
        get { return falconScore; }
        set { falconScore = value; }
    }

    public void UpdatePlayerPrefs()
    {
        bool exists = PlayerPrefs.HasKey("FoxScore");
        bool clanExists = PlayerPrefs.HasKey("Clan");

        if (!clanExists) PlayerPrefs.SetString("Clan", "Fox");
        if (clan != "") clan = PlayerPrefs.GetString("Clan");

        if (!exists)
        {
            PlayerPrefs.SetInt("FoxScore", FoxScore);
            PlayerPrefs.SetInt("CatScore", CatScore);
            PlayerPrefs.SetInt("DragonScore", DragonScore);
            PlayerPrefs.SetInt("FalconScore", FalconScore);
        } else 
        {
            int tempFox = PlayerPrefs.GetInt("FoxScore");
            int tempCat = PlayerPrefs.GetInt("CatScore");
            int tempDragon = PlayerPrefs.GetInt("DragonScore");
            int tempFalcon = PlayerPrefs.GetInt("FalconScore");

            foxScore += tempFox;
            catScore += tempCat;
            dragonScore += tempDragon;
            falconScore += tempFalcon;

            if (playerScore > compyScore)
            {
                switch (clan)
                {
                    case "Fox":
                        foxScore += winAmount;
                        break;
                    case "Dragon":
                        dragonScore += winAmount;
                        break;
                    case "Falcon":
                        falconScore += winAmount;
                        break;
                    case "Cat":
                        catScore += winAmount;
                        break;
                    default:
                        break;
                }
            }

            PlayerPrefs.SetInt("FoxScore", FoxScore);
            PlayerPrefs.SetInt("CatScore", CatScore);
            PlayerPrefs.SetInt("DragonScore", DragonScore);
            PlayerPrefs.SetInt("FalconScore", FalconScore);
        }
        PlayerPrefs.Save();
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.SetInt("FoxScore", 0);
        PlayerPrefs.SetInt("CatScore", 0);
        PlayerPrefs.SetInt("DragonScore", 0);
        PlayerPrefs.SetInt("FalconScore", 0);
        PlayerPrefs.Save();
    }

    public void SetClan(string clanChoice)
    {
        PlayerPrefs.SetString("Clan", clanChoice);
        clan = clanChoice;
        PlayerPrefs.Save();
    }

}
