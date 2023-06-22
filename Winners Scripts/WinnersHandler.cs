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
    
    private string winner;
    private List<KeyValuePair<string, int>> winList;

    void Awake()
    {
        winner = ScoreDataTransfer.Instance.Winner;
        winList = ScoreDataTransfer.Instance.WinList;
    }

    void Start()
    {
        if (winner != null)
        {
            switch (winner)
            {
                case "Fox":
                    centreLogo = foxSprite;
                    break;
                case "Cat":
                    centreLogo = catSprite;
                    break;
                case "Dragon":
                    centreLogo = dragonSprite;
                    break;
                case "Falcon":
                    centreLogo = falconSprite;
                    break;
            }
        }
    }
    

    void Update()
    {
        
    }
}
