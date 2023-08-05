using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class Ticker : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private RectTransform textScroll;
    private bool canScroll;
    private TextMeshProUGUI tickerScroll;
    private DateTime startTime;
    private Vector3 startPosition;
    private Vector3 startPosition2;

    void Awake()
    {
        textScroll = GetComponent<RectTransform>();
        tickerScroll = GetComponent<TextMeshProUGUI>();
        startTime = DateTime.Now;
        startPosition = textScroll.position;
        float partial = textScroll.rect.width / 16;
        startPosition2 = new Vector3(startPosition.x + partial, textScroll.position.y, textScroll.position.z);
    }

    
    void Update()
    {
        if (canScroll)
        {
            textScroll.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            DateTime elapsedTime = DateTime.Now;
            TimeSpan gap = elapsedTime - startTime;
            if (gap.Seconds % 30 == 0 && gap.Seconds > 0)
            {
                textScroll.position = startPosition;
            }             
            // if (textScroll.anchoredPosition.x < -textScroll.rect.width)
            // {
            //     Destroy(gameObject);
            // } 
        }
        
    }


    public void StartScrolling(BattleInfo battleInfo)
    {
        ProcessPoints processPoints = new ProcessPoints();
        string messageText = processPoints.Process(battleInfo);

        tickerScroll.enableAutoSizing = true;
        tickerScroll.enableWordWrapping = false;

        if (messageText != null)
        {
            tickerScroll.text = messageText;
        }
        canScroll = true;
        processPoints = null;
        battleInfo = null;
    }
}
