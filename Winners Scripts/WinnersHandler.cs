using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnersHandler : MonoBehaviour
{
    [SerializeField] Sprite centreLogo;
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
        winningScoreText.text = $"{winningScore} Points";
        if (winner != null)
        {
            switch (winner)
            {
                case "Fox":
                    centreLogo = foxSprite;
                    winnersText.text = "Fox WINS";
                    break;
                case "Cat":
                    centreLogo = catSprite;
                    winnersText.text = "Cat WINS";
                    break;
                case "Dragon":
                    centreLogo = dragonSprite;
                    winnersText.text = "Dragon WINS";
                    break;
                case "Falcon":
                    centreLogo = falconSprite;
                    winnersText.text = "Falcon WINS";
                    break;
            }
        }

        ScoreDataTransfer.Instance.ClearWeek();
    }
    

}
