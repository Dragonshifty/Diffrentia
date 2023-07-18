using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnersHandler : MonoBehaviour
{
    [SerializeField] Image centreLogo;
    [SerializeField] Sprite foxSprite;
    [SerializeField] Sprite catSprite;
    [SerializeField] Sprite dragonSprite;
    [SerializeField] Sprite falconSprite;
    [SerializeField] TextMeshProUGUI winnersText;
    [SerializeField] TextMeshProUGUI winningScoreText;

    int winningScore;
    
    private string winner;
    private List<KeyValuePair<string, int>> winList;

    void Awake()
    {
        winner = ScoreDataTransfer.Instance.Winner;
        winList = ScoreDataTransfer.Instance.WinList;
        winningScore = ScoreDataTransfer.Instance.WinningScore;
    }

    void Start()
    {
        string winningScoreFormated = NumberFormat(winningScore);
        winningScoreText.text = $"{winningScoreFormated} Points";
        if (winner != null)
        {
            switch (winner)
            {
                case "Fox":
                    centreLogo.sprite = foxSprite;
                    winnersText.text = "Fox WINS";
                    break;
                case "Cat":
                    centreLogo.sprite = catSprite;
                    winnersText.text = "Cat WINS";
                    break;
                case "Dragon":
                    centreLogo.sprite = dragonSprite;
                    winnersText.text = "Dragon WINS";
                    break;
                case "Falcon":
                    centreLogo.sprite = falconSprite;
                    winnersText.text = "Falcon WINS";
                    break;
            }
        }

        ScoreDataTransfer.Instance.ClearWeek();
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
        if (number <= 1000)
        {
            return number.ToString();
        } else 
        {
            float smaller = number / div;
            return $"{smaller:F2}{suffix}";
        }
    }
}
