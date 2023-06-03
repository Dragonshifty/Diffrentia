using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinLoseConditions : MonoBehaviour
{
    System.Random rand = new System.Random();
    List<string> drawMessages = new List<string>();
    List<string> veryCloseWin = new List<string>();
    List<string> closeWin = new List<string>();
    List<string> generalWin = new List<string>();
    List<string> bigWin = new List<string>();
    List<string> veryCloseLoss = new List<string>();
    List<string> closeLoss = new List<string>();
    List<string> generalLoss = new List<string>();
    List<string> bigLoss = new List<string>();
    string clan = "";

    [SerializeField] TextMeshProUGUI falconPoints;
    [SerializeField] TextMeshProUGUI foxPoints;
    [SerializeField] TextMeshProUGUI catPoints;
    [SerializeField] TextMeshProUGUI dragonPoints;
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI compyScore;
    [SerializeField] TextMeshProUGUI winLoseOrDrawText;
    [SerializeField] TextMeshProUGUI conditionsMessage;
    // List<TextMeshProUGUI> pointsTextList;

    // ScoreDataTransfer scoreDataTransfer;

    public enum WinLossType
    {
        VeryCloseWin,
        CloseWin,
        GeneralWin,
        BigWin,
        VeryCloseLoss,
        CloseLoss,
        GeneralLoss,
        BigLoss,
        Draw
    } 

    void Awake() 
    {
        
    }

    private void Start() 
    {
        // pointsTextList.Add(falconPoints);
        // pointsTextList.Add(foxPoints);
        // pointsTextList.Add(catPoints);
        // pointsTextList.Add(dragonPoints);

        drawMessages.Add("Alright, let's call it a draw");
        drawMessages.Add("A draw! Yay. Well done, you.");

        veryCloseWin.Add("Really down to the wire, there");
        veryCloseWin.Add("Just clinched it.");
        veryCloseWin.Add("A win! Only just, but still, a win!");
        veryCloseWin.Add("I probably shouldn't have bet against you there.");

        closeWin.Add("Nicely done.");
        closeWin.Add("Convincing win.");
        closeWin.Add("Actual skill or just lucky? We may never know.");
        closeWin.Add("This is promising.");

        generalWin.Add("Very convinving win.");
        generalWin.Add("That'll do it.");
        generalWin.Add("Things are looking up.");

        bigWin.Add("Outstanding!");
        bigWin.Add("Go, you!");
        bigWin.Add("Now do it again!");

        veryCloseLoss.Add("That's a bad miss.");
        veryCloseLoss.Add("So, so close!");
        veryCloseLoss.Add("Noooo!");
        veryCloseLoss.Add("Harsh.");

        closeLoss.Add("That was almost close.");
        closeLoss.Add("Could have gone either way. Kinda.");

        bigLoss.Add("That could have gone better.");
        bigLoss.Add("Hacks, obviously.");
        bigLoss.Add("Moving on...");
        bigLoss.Add("Ouch.");

        generalLoss.Add("At least you tried (did you?).");
        generalLoss.Add("Participation trophy on the way to you.");
        generalLoss.Add("You showed up. That's good, isn't it?");
        generalLoss.Add("Minor blip.");

        RunEndGame();
    }

    public string[] CheckWinLoseConditions(int player, int compy)
    {
        string message = CheckScores(player, compy);
        string[] returnInfo = new string[2];

        if (message != null)
        {
            switch(message)
            {
                case "VeryCloseWin":
                    returnInfo[0] = RunVeryCloseWin();
                    returnInfo[1] = "Win";
                    break;
                case "CloseWin":
                    returnInfo[0] = RunCloseWin();
                    returnInfo[1] = "Win";
                    break;
                case "GeneralWin":
                    returnInfo[0] = RunGeneralWin();
                    returnInfo[1] = "Win";
                    break;
                case "BigWin":
                    returnInfo[0] = RunBigWin();
                    returnInfo[1] = "Win";
                    break;
                case "VeryCloseLoss":
                    returnInfo[0] = RunVeryCloseLoss();
                    returnInfo[1] = "Lose";
                    break;
                case "CloseLoss":
                    returnInfo[0] = RunCloseLoss();
                    returnInfo[1] = "Lose";
                    break;
                case "GeneralLoss":
                    returnInfo[0] = RunGeneralLoss();
                    returnInfo[1] = "Lose";
                    break;
                case "BigLoss":
                    returnInfo[0] = RunBigLoss();
                    returnInfo[1] = "Lose";
                    break;  
                default:
                    returnInfo[0] = "?";
                    returnInfo[1] = "?";
                    break; 
            }
        }
        return returnInfo;
    }

    private string CheckScores(int player, int compy)
    {
        if (player == compy)
        {
            return "Draw";
        } else if (player < 0 )
        { 
            return "BigLoss";
        } else if (compy < 0)
        {   
            return "BigWin";
        } else if (player > compy)
        {
            int difference = player - compy;
            if (difference > 0  && difference < 6)
            {
                return "VeryCloseWin";
            } else if (difference > 5 && difference < 11)
            {
                return "CloseWin";
            } else if (difference > 10 && difference < 26)
            {
                return "GeneralWin";
            } else if (difference > 25)
            {
                return "BigWin";
            }
        } else if (compy > player)
        {
            int difference = compy - player;
            if (difference > 0  && difference < 6)
            {
                return "VeryCloseLoss";
            } else if (difference > 5 && difference < 11)
            {
                return "CloseLoss";
            } else if (difference > 10 && difference < 26)
            {
                return "GeneralLoss";
            } else if (difference > 25)
            {
                return "BigLoss";
            }
        } else
        {
            return null;
        }
        return null;
    }

    public void RunEndGame()
    {

        int player = ScoreDataTransfer.Instance.PlayerScore;
        int compy = ScoreDataTransfer.Instance.CompyScore;

        string[] recieveInfo = CheckWinLoseConditions(player, compy);

        clan = ScoreDataTransfer.Instance.clan;
        falconPoints.text = ScoreDataTransfer.Instance.FalconScore.ToString();
        catPoints.text = ScoreDataTransfer.Instance.CatScore.ToString();
        foxPoints.text = ScoreDataTransfer.Instance.FoxScore.ToString();
        dragonPoints.text = ScoreDataTransfer.Instance.DragonScore.ToString();

        playerScore.text = player.ToString();
        compyScore.text = compy.ToString();

        PointsColour();

        conditionsMessage.text = recieveInfo[0];
        winLoseOrDrawText.text = recieveInfo[1];
    }

    void PointsColour()
    {
        Debug.Log(clan);
        switch (clan)
        {
            case "Cat":
                catPoints.color = new Color32(255, 4, 229, 255);
                break;
            case "Dragon":
                dragonPoints.color = new Color32(255, 4, 229, 255);
                break;
            case "Fox":
                foxPoints.color = new Color32(255, 4, 229, 255);
                break;
            case "Falcon":
                falconPoints.color = new Color32(255, 4, 229, 255);
                break;
        }
    }

    private string RunDrawMessages()
    {
        int choice = rand.Next(0, drawMessages.Count);
        return drawMessages[choice];
    }

    private string RunVeryCloseWin()
    {
        int choice = rand.Next(0, veryCloseWin.Count);
        return veryCloseWin[choice];
    }

    private string RunCloseWin()
    {
        int choice = rand.Next(0, closeWin.Count);
        return closeWin[choice];
    }

    private string RunGeneralWin()
    {
        int choice = rand.Next(0, generalWin.Count);
        return generalWin[choice];
    }

    private string RunBigWin()
    {
        int choice = rand.Next(0, bigWin.Count);
        return bigWin[choice];
    }

    private string RunVeryCloseLoss()
    {
        int choice = rand.Next(0, veryCloseLoss.Count);
        return veryCloseLoss[choice];
    }

    private string RunCloseLoss()
    {
        int choice = rand.Next(0, closeLoss.Count);
        return closeLoss[choice];
    }

    private string RunGeneralLoss()
    {
        int choice = rand.Next(0, generalLoss.Count);
        return generalLoss[choice];
    }

    private string RunBigLoss()
    {
        int choice = rand.Next(0, bigLoss.Count);
        return bigLoss[choice];
    }
}
