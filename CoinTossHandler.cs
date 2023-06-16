using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTossHandler : MonoBehaviour
{
    public GameObject coinTossObject;
    TextMeshProUGUI coinTossText;
    private float fadeTotalTime = 1.2f;
    private float fadeOutTime = 1f;
    private float fadeIn;
    private float fadeOut;
    private bool showText = false;
    private void Awake() 
    {
        coinTossText = coinTossObject.GetComponentInChildren<TextMeshProUGUI>();
        fadeIn = fadeTotalTime;
        fadeOut = fadeOutTime;
        // coinTossObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (showText){
            if (fadeIn > 0f)
            {
                fadeIn -= Time.deltaTime;
                float opacity = Mathf.Lerp(0f, 1f, 1f - (fadeIn / fadeTotalTime));
                SetOpacity(opacity);
                coinTossObject.gameObject.SetActive(true);
            } else
            {
                fadeOut -= Time.deltaTime;
                float opacity = Mathf.Lerp(1f, 0f, 1f - (fadeOut / fadeOutTime));
                SetOpacity(opacity);
            }

            if (fadeOut <= 0f)
            {
                coinTossObject.gameObject.SetActive(false);
            }
        }
    }

    public void WhoStarts(bool isPlayer, bool doStart)
    {
        if (isPlayer)
        {
            coinTossText.text = "Player\nStarts";
        } else 
        {
            coinTossText.text = "Computer\nStarts";
        }
        showText = doStart;
    }

    void SetOpacity(float opacity)
    {
        Color textCol = coinTossText.color;
        textCol.a = opacity;
        coinTossText.color = textCol;
    }
}
