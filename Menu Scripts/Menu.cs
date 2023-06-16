using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    private bool hasChosen = false;
    private string playerClan = "";
    private int playerPoints;
    private int foxPoints;
    private int catPoints;
    private int dragonPoints;
    private int falconPoints;
    // private string currentLeaders;
    // private int currentLeadersScore;
    // public TextMeshProUGUI tick;
    private Dictionary<string, int> clanPoints;

    Ticker ticker;
    BattleInfo battleInfo;
    SceneStuffs sceneStuffs;
    

    private void Awake() 
    {
        sceneStuffs = FindObjectOfType<SceneStuffs>();
        ticker = FindObjectOfType<Ticker>();
        CheckForClan();
    }
    void Start()
    {
        // ticker = FindObjectOfType<Ticker>();
        RunTicker();
    }


    void CheckForClan()
    {
        int checkClan = 0;
        bool exists = false;
        try 
        {
            exists = PlayerPrefs.HasKey("ClanChosen");
            if (exists) checkClan = PlayerPrefs.GetInt("ClanChosen");

        } catch (PlayerPrefsException ex)
        {
            Debug.Log("Player Prefs load error: " + ex.Message);
        }

        try
        {
            if (checkClan != 1 || !exists)
            {
                PlayerPrefs.SetInt("FoxScore", 0);
                PlayerPrefs.SetInt("CatScore", 0);
                PlayerPrefs.SetInt("DragonScore", 0);
                PlayerPrefs.SetInt("FalconScore", 0);
                PlayerPrefs.SetInt("ClanChosen", 0);
                PlayerPrefs.Save();
            } else
            {
                hasChosen = true;
                foxPoints = PlayerPrefs.GetInt("FoxScore");
                catPoints = PlayerPrefs.GetInt("CatScore");
                dragonPoints = PlayerPrefs.GetInt("DragonScore");
                falconPoints = PlayerPrefs.GetInt("FalconScore");
                playerClan = PlayerPrefs.GetString("Clan");
                clanPoints = new Dictionary<string, int>
                {
                    {"Fox", foxPoints},
                    {"Cat", catPoints},
                    {"Dragon", dragonPoints},
                    {"Falcon", falconPoints}
                };
                SetClan(playerClan);
                playerPoints = clanPoints[playerClan];
                string currentLeader =  CheckHighest();
                int leaderPoints = clanPoints[currentLeader];
                battleInfo = new BattleInfo(
                    foxPoints, 
                    catPoints, 
                    dragonPoints, 
                    falconPoints, 
                    playerPoints, 
                    leaderPoints, 
                    playerClan, 
                    currentLeader);
            }
        } catch (PlayerPrefsException ex)
        {
            Debug.Log("Player Prefs load error: " + ex.Message);
        }
    }

    void SetClan(string clanName)
    {
        ScoreDataTransfer.Instance.SetClan(clanName);
    }

    string CheckHighest()
    {
        string currentLeader = "";
        if (hasChosen)
        {
            var highestScore = clanPoints.Aggregate((x, y) => x.Value > y.Value ? x : y);
            currentLeader = highestScore.Key;
            // currentLeadersScore = highestScore.Value;
        }
        return currentLeader;
    }

    void RunTicker()
    {
        if (hasChosen) ticker.StartScrolling(battleInfo);
    }

    public void RunMainGame()
    {
        if (hasChosen)
        {
            sceneStuffs.LoadMainGame();
        } else
        {
            sceneStuffs.LoadClanSelection();
        }
    }

    

    public void ResetGame()
    {
        hasChosen = false;
        ScoreDataTransfer.Instance.ClearPlayerPrefs();
        sceneStuffs.LoadClanSelection();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
