using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    
    [SerializeField] Image foxImage;
    [SerializeField] Image catImage;
    [SerializeField] Image dragonImage;
    [SerializeField] Image falconImage;
    [SerializeField] Sprite foxSprite;
    [SerializeField] Sprite catSprite;
    [SerializeField] Sprite dragonSprite;
    [SerializeField] Sprite falconSprite;

    [SerializeField] Button newGame;
    [SerializeField] Button menu;

    private void Awake() 
    {
        newGame.interactable = false;
        menu.interactable = false;    
    }
    private void Start() 
    {
        StartCoroutine(EnableButtons());
        drawMessages.Add("Alright, let's call it a draw.");
        drawMessages.Add("A draw! Yay. Well done, you.");

        veryCloseWin.Add("Really down to the wire there.");
        veryCloseWin.Add("Just clinched it.");
        veryCloseWin.Add("A win! Only just, but still, a win!");
        veryCloseWin.Add("I probably shouldn't have bet against you there.");
        veryCloseWin.Add("I really shouldn't cheer and drink at the same time.\nGreat win but so much tea on my screen.");
        veryCloseWin.Add("Awesome win but not so good for my blood pressure!\nAnd relax, deep breaths...");

        closeWin.Add("Nicely done.");
        closeWin.Add("Edge of your seat type stuff there.");
        closeWin.Add("I was rooting for you the whole time. Honest.");
        closeWin.Add("This is promising.");
        closeWin.Add("Do you know I'm actually dancing.\nWould be embarrasing if the webcam was on.");

        generalWin.Add("Very convincing win.");
        generalWin.Add("That'll do it.");
        generalWin.Add("Things are looking up.");
        generalWin.Add("Like a leaf on the wind.");
        generalWin.Add("I would break into victory song if it weren't for the complaints.");

        bigWin.Add("Outstanding!");
        bigWin.Add("Go, you!");
        bigWin.Add("Now do it again!");
        bigWin.Add("And there was much whooping.");
        bigWin.Add("Cool and breezy wins the race. Wait, that's not right?!");

        veryCloseLoss.Add("That's a bad miss.");
        veryCloseLoss.Add("So, so close!");
        veryCloseLoss.Add("Noooo!");
        veryCloseLoss.Add("Harsh.");
        veryCloseLoss.Add("I'm making a low whining noise and now\nit's attracting the local wildlife.");

        closeLoss.Add("That was almost close.");
        closeLoss.Add("Could have gone either way.");
        closeLoss.Add("That was outrageous. I'd file a complaint if I were you.");
        closeLoss.Add("This reminds me of a funny story\nbut you wouldn't want to hear it right now.");

        bigLoss.Add("That could have gone better.");
        bigLoss.Add("I suspect foul play.");
        bigLoss.Add("Moving on...");
        bigLoss.Add("Ouch.");

        generalLoss.Add("Sorry, I missed that. What happened?");
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
                case "Draw":
                    returnInfo[0] = RunDrawMessages();
                    returnInfo[1] = "Draw";
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

        int falconTotal = ScoreDataTransfer.Instance.FalconScore;
        int catTotal = ScoreDataTransfer.Instance.CatScore;
        int foxTotal = ScoreDataTransfer.Instance.FoxScore;
        int dragonTotal = ScoreDataTransfer.Instance.DragonScore;

        string falconTotalString = falconTotal < 1000 ? falconTotal.ToString() : NumberFormat(falconTotal);
        string catTotalString = catTotal < 1000 ? catTotal.ToString() : NumberFormat(catTotal);
        string foxTotalString = foxTotal < 1000 ? foxTotal.ToString() : NumberFormat(foxTotal);
        string dragonTotalString = dragonTotal < 1000 ? dragonTotal.ToString() : NumberFormat(dragonTotal);

        falconPoints.text = falconTotalString;
        catPoints.text = catTotalString;
        foxPoints.text = foxTotalString;
        dragonPoints.text = dragonTotalString;

        playerScore.text = player.ToString();
        compyScore.text = compy.ToString();

        PointsColour();

        conditionsMessage.text = recieveInfo[0];
        winLoseOrDrawText.text = recieveInfo[1];
    }

    string NumberFormat(int number)
    {
        string suffix = "";
        float div = 1f;
        
        if (number >= 1000000)
        {
            suffix = "M";
            div = 1000000f;
        } else if (number >= 1000)
        {
            suffix = "K";
            div = 1000f;
        }
        float smaller = number / div;
        return $"{smaller:F2}{suffix}";
    }

    void PointsColour()
    {
        // Debug.Log(clan);
        switch (clan)
        {
            case "Cat":
                catPoints.color = new Color32(255, 255, 255, 255);
                catImage.sprite = catSprite;
                break;
            case "Dragon":
                dragonPoints.color = new Color32(255, 255, 255, 255);
                dragonImage.sprite = dragonSprite;
                break;
            case "Fox":
                foxPoints.color = new Color32(255, 255, 255, 255);
                foxImage.sprite = foxSprite;
                break;
            case "Falcon":
                falconPoints.color = new Color32(255, 255, 255, 255);
                falconImage.sprite = falconSprite;
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

    IEnumerator EnableButtons()
    {
        yield return new WaitForSeconds(2);
        newGame.interactable = true;
        menu.interactable = true;
    }
}
