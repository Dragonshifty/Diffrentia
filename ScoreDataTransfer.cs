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
    public string clan = "";

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
        // bool clanExists = PlayerPrefs.HasKey("Clan");

        clan = PlayerPrefs.GetString("Clan");

        if (!exists)
        {
            PlayerPrefs.SetInt("FoxScore", 0);
            PlayerPrefs.SetInt("CatScore", 0);
            PlayerPrefs.SetInt("DragonScore", 0);
            PlayerPrefs.SetInt("FalconScore", 0);
        }

        int tempFox = PlayerPrefs.GetInt("FoxScore");
        int tempCat = PlayerPrefs.GetInt("CatScore");
        int tempDragon = PlayerPrefs.GetInt("DragonScore");
        int tempFalcon = PlayerPrefs.GetInt("FalconScore");

        // foxScore += tempFox;
        // catScore += tempCat;
        // dragonScore += tempDragon;
        // falconScore += tempFalcon;

        if (playerScore > compyScore)
        {
            switch (clan)
            {
                case "Fox":
                    foxScore += (winAmount + tempFox);
                    catScore = tempCat;
                    dragonScore = tempDragon;
                    falconScore = tempFalcon;
                    PlayerPrefs.SetInt("FoxScore", FoxScore);
                    break;
                case "Dragon":
                    dragonScore += (winAmount + tempDragon);
                    foxScore = tempFox;
                    catScore = tempCat;
                    falconScore = tempFalcon;
                    PlayerPrefs.SetInt("DragonScore", DragonScore);
                    break;
                case "Falcon":
                    falconScore += (winAmount + tempFalcon);
                    foxScore = tempFox;
                    catScore = tempCat;
                    dragonScore = tempDragon;
                    PlayerPrefs.SetInt("FalconScore", FalconScore);
                    break;
                case "Cat":
                    catScore += (winAmount + tempCat);
                    foxScore = tempFox;
                    dragonScore = tempDragon;
                    falconScore = tempFalcon;
                    PlayerPrefs.SetInt("CatScore", CatScore);
                    break;
                default:
                    break;
            }
        }

        if (compyScore > playerScore)
        {
            if (!clan.Equals("Fox"))
            {
                foxScore += tempFox;
                PlayerPrefs.SetInt("FoxScore", FoxScore);
            }
            if (!clan.Equals("Cat"))
            {
                catScore += tempCat;
                PlayerPrefs.SetInt("CatScore", CatScore);
            }
            if (!clan.Equals("Dragon"))
            {
                dragonScore += tempDragon;
                PlayerPrefs.SetInt("DragonScore", DragonScore);
            }
            if (!clan.Equals("Falcon"))
            {
                falconScore += tempFalcon;
                PlayerPrefs.SetInt("FalconScore", FalconScore);
            }

            switch (clan)
            {
                case "Fox":
                    foxScore = tempFox;
                    break;
                case "Dragon":
                    dragonScore = tempDragon;
                    break;
                case "Cat":
                    catScore = tempCat;
                    break;
                case "Falcon":
                    falconScore = tempFalcon;
                    break;
            }
        }

        if (compyScore == playerScore)
        {
            foxScore = tempFox;
            catScore = tempCat;
            dragonScore = tempDragon;
            falconScore = tempFalcon;
        }
        
        PlayerPrefs.Save();
    }

    

    public void ClearPlayerPrefs()
    {
        try 
        {
            PlayerPrefs.SetInt("FoxScore", 0);
            PlayerPrefs.SetInt("CatScore", 0);
            PlayerPrefs.SetInt("DragonScore", 0);
            PlayerPrefs.SetInt("FalconScore", 0);
            PlayerPrefs.SetInt("ClanChosen", 0);
            PlayerPrefs.Save();
        } catch (PlayerPrefsException ex)
        {
            Debug.LogError("Error PlayerPrefs: " + ex.Message);
        }
    }

    public void SetClan(string clanChoice)
    {
        PlayerPrefs.SetString("Clan", clanChoice);
        PlayerPrefs.SetInt("ClanChosen", 1);
        clan = clanChoice;
        PlayerPrefs.Save();
    }

}
