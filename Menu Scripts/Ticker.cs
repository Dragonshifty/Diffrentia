using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ticker : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private RectTransform textScroll;
    private bool canScroll;
    private TextMeshProUGUI tickerScroll;

    void Start()
    {
        textScroll = GetComponent<RectTransform>();
        tickerScroll = GetComponent<TextMeshProUGUI>();
    }

    
    void Update()
    {
        if (canScroll)
        {
            textScroll.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            if (textScroll.anchoredPosition.x < -textScroll.rect.width)
            {
                textScroll.anchoredPosition = new Vector2(Screen.width, textScroll.anchoredPosition.y);
            }  
        }
        
    }

    public void StartScrolling(string leader, int leaderScore, string clanChoice,  int playerScore, bool doStart)
    {
        tickerScroll.text = $"{leader} is in the lead with {leaderScore.ToString()}points. You Clan {clanChoice} has {playerScore.ToString()}points.";
        canScroll = doStart;
    }
}
