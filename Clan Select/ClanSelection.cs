using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClanSelection : MonoBehaviour
{
    [Header("ClanImages")]
    [SerializeField] GameObject falconImage;
    [SerializeField] GameObject catImage;
    [SerializeField] GameObject dragonImage;
    [SerializeField] GameObject foxImage;
    [Header("ClanCircles")]
    [SerializeField] GameObject falconCircle;
    [SerializeField] GameObject catCircle;
    [SerializeField] GameObject foxCircle;
    [SerializeField] GameObject dragonCircle;
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI falconText;
    [SerializeField] TextMeshProUGUI catText;
    [SerializeField] TextMeshProUGUI dragonText;
    [SerializeField] TextMeshProUGUI foxText;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] TextMeshProUGUI chooseClanText;
    // private bool hasChosen = false;

    private void Awake() 
    {
       CheckForClan();
    }

    void Start()
    {
        // CheckForClan();
        // SoundHandler.Instance.PlayMusic(true);
    }

    void CheckForClan()
    {
        if (PlayerPrefs.GetInt("ClanChosen") == 1)
        {
            string tempClan = PlayerPrefs.GetString("Clan");
            ShowClanText(tempClan);
            startButton.gameObject.SetActive(true);
        }
    }

    void OnMouseDown() 
    {
        if (falconImage != null && falconImage == gameObject)
        {
            SoundHandler.Instance.PlayFalconSound();
            SetClan("Falcon");
        }

        if (catImage!= null && catImage == gameObject)
        {
            SoundHandler.Instance.PlayCatSound();
            SetClan("Cat");
        }

        if (foxImage != null && foxImage == gameObject)
        {
            SoundHandler.Instance.PlayFoxSound();
            SetClan("Fox");
        }

        if (dragonImage != null && dragonImage == gameObject)
        {
            SoundHandler.Instance.PlayDragonSound();
            SetClan("Dragon");
        }
           
    }

    void SetClan(string clanName)
    {
        ShowClanText(clanName);
        // hasChosen = true;
        startButton.gameObject.SetActive(true);
        ScoreDataTransfer.Instance.SetClan(clanName);
    }

    void ShowClanText(string clanName)
    {
        chooseClanText.text = "Clan\nChosen";
        switch (clanName)
        {
            case "Falcon":
                falconCircle.SetActive(true);
                catCircle.SetActive(false);
                foxCircle.SetActive(false);
                dragonCircle.SetActive(false);            
                break;
            case "Cat":
                falconCircle.SetActive(false);
                catCircle.SetActive(true);
                foxCircle.SetActive(false);
                dragonCircle.SetActive(false);
                break;
            case "Dragon":
                falconCircle.SetActive(false);
                catCircle.SetActive(false);
                foxCircle.SetActive(false);
                dragonCircle.SetActive(true);
                break;
            case "Fox":
                falconCircle.SetActive(false);
                catCircle.SetActive(false);
                foxCircle.SetActive(true);
                dragonCircle.SetActive(false);
                break;
        }
    }

    public void ClearClanSelection()
    {
        chooseClanText.text = "Choose\nClan";
        falconCircle.SetActive(false);
        catCircle.SetActive(false);
        foxCircle.SetActive(false);
        dragonCircle.SetActive(false);
        startButton.gameObject.SetActive(false);
        ScoreDataTransfer.Instance.ClearPlayerPrefs();
    }
}
