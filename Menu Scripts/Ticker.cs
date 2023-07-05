using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Ticker : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private RectTransform textScroll;
    private bool canScroll;
    private TextMeshProUGUI tickerScroll;

    // float delay = 0.04f;

    // private string originalMessage;
    // private string currentText;
    // private float timer;

    void Awake()
    {
        textScroll = GetComponent<RectTransform>();
        tickerScroll = GetComponent<TextMeshProUGUI>();

        
        // currentText = "";
        // timer = 0f;
    }

    
    void Update()
    {
        if (canScroll)
        {
            textScroll.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            if (textScroll.anchoredPosition.x < -textScroll.rect.width)
            {
                // textScroll.anchoredPosition = new Vector2(Screen.width, textScroll.anchoredPosition.y);
                Destroy(gameObject);
            } 

        //     if (currentText.Length < originalMessage.Length)
        // {
        //     timer += Time.deltaTime;
        //     if (timer >= delay)
        //     {
        //         timer = 0f;
        //         currentText = originalMessage.Substring(0, currentText.Length + 1);
        //         tickerScroll.text = currentText;
        //     }
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
            // originalMessage = tickerScroll.text;
        }
        canScroll = true;
        processPoints = null;
        battleInfo = null;
    }
}
