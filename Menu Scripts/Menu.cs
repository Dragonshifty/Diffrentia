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
    private string currentLeaders;
    private int currentLeadersScore;
    public TextMeshProUGUI tick;
    private Dictionary<string, int> clanPoints;

    Ticker ticker;
    

    private void Awake() 
    {
       CheckForClan();
    }
    void Start()
    {
        // tick = GetComponent<Ticker>();
        CheckHighest();
        RunTicker();
    }

    
    void Update()
    {
        
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
                // SetClan(playerClan);
                playerPoints = clanPoints[playerClan];
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

    void CheckHighest()
    {
        if (hasChosen)
        {
            var highestScore = clanPoints.Aggregate((x, y) => x.Value > y.Value ? x : y);
            currentLeaders = highestScore.Key;
            currentLeadersScore = highestScore.Value;
        }
    }

    void RunTicker()
    {
        ticker.StartScrolling(currentLeaders, currentLeadersScore, playerClan, playerPoints, true);
        Debug.Log(currentLeaders + currentLeadersScore.ToString() + playerClan + playerPoints.ToString());
    }

    
}
