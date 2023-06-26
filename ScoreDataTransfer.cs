using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private string winner;
    private int winningScore;
    private List<KeyValuePair<string, int>> winList;
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

    public string Winner
    {
        get { return winner; }
        set { winner = value; }
    }

    public int WinningScore
    {
        get { return winningScore; }
    }

    public List<KeyValuePair<string, int>> WinList
    {
        get {return winList;}
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
            PlayerPrefs.SetInt("TimerSet", 0);
            PlayerPrefs.SetString("Winner", "No Winners");
            PlayerPrefs.Save();
        } catch (PlayerPrefsException ex)
        {
            Debug.LogError("Error PlayerPrefs: " + ex.Message);
        }
    }

    public void ClearWeek()
    {
        try 
        {
            PlayerPrefs.SetInt("FoxScore", 0);
            PlayerPrefs.SetInt("CatScore", 0);
            PlayerPrefs.SetInt("DragonScore", 0);
            PlayerPrefs.SetInt("FalconScore", 0);
            PlayerPrefs.SetInt("TimerSet", 0);
            PlayerPrefs.SetString("Winner", "No Winners");
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

    public void GetWeeksWinner()
    {
        // string winner = "";
        string place = "";

        Dictionary<string, int> clanPoints = new Dictionary<string, int>
                {
                    {"Fox", foxScore},
                    {"Cat", catScore},
                    {"Dragon", dragonScore},
                    {"Falcon", falconScore}
                };

        List<KeyValuePair<string, int>> sortedPointsList = clanPoints.OrderByDescending(pair => pair.Value).ToList();

        winList = sortedPointsList;

        int[] pointsValues = new int[]
        {
            sortedPointsList[0].Value,
            sortedPointsList[1].Value,
            sortedPointsList[2].Value,
            sortedPointsList[3].Value
        };

        string[] clanNames = new string[]
        {
            sortedPointsList[0].Key,
            sortedPointsList[1].Key,
            sortedPointsList[2].Key,
            sortedPointsList[3].Key
        };

        if (clanNames[0].Equals(clan)) place = "First";
        if (clanNames[1].Equals(clan)) place = "Second";
        if (clanNames[2].Equals(clan)) place = "Third";
        if (clanNames[3].Equals(clan)) place = "Fourth";

        winningScore =  pointsValues[0];
        bool noScore = pointsValues.All(x => x == 0);
        bool allDraw = pointsValues.All(x => x == pointsValues[0]);

        if (noScore)
        {
            winner = "No Winners";
        }

        if (!noScore && allDraw)
        {
            winner = clan;
        }

        if (pointsValues[0] == pointsValues[1])
        {
            if (pointsValues[1] == pointsValues[2])
            {
                if (place.Equals("First") || place.Equals("Second") || place.Equals("Third"))
                {
                    winner = clan;
                }
            } else if (place.Equals("First") || place.Equals("Second"))
            {
                winner = clan;
            }
        } else 
        {
            winner = clanNames[0];
        }

        try 
        {
            PlayerPrefs.SetInt("TimerSet", 0);
            PlayerPrefs.SetString("Winner", winner);
        } catch (PlayerPrefsException ex)
        {
            Debug.LogError("Error PlayerPrefs: " + ex.Message);
        }
    }

}
