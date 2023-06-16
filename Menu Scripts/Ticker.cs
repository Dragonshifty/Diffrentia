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
    private BattleInfo battleInfo;

    void Awake()
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

    public void StartScrolling(BattleInfo battleInfo)
    {
        this.battleInfo = battleInfo;
        tickerScroll.text = $"{battleInfo.CurrentLeader} is in the lead with {battleInfo.LeaderPoints.ToString()} points. Your Clan {battleInfo.PlayerClan} has {battleInfo.PlayerPoints.ToString()} points.";
        canScroll = true;
    }
}
