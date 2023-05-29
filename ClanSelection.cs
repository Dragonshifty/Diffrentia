using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClanSelection : MonoBehaviour
{
    [SerializeField] GameObject falconImage;
    [SerializeField] GameObject catImage;
    [SerializeField] GameObject dragonImage;
    [SerializeField] GameObject foxImage;
    [SerializeField] TextMeshProUGUI falconText;
    [SerializeField] TextMeshProUGUI catText;
    [SerializeField] TextMeshProUGUI dragonText;
    [SerializeField] TextMeshProUGUI foxText;
    [SerializeField] GameObject startButton;
    [SerializeField] TextMeshProUGUI chooseClanText;
    // private bool hasChosen = false;

    private void Awake() 
    {
        // falconImage = GetComponent<Image>(); 
        // catImage = GetComponent<Image>();
        // dragonImage = GetComponent<Image>();
        // foxImage = GetComponent<Image>();

    }
    void Start()
    {
        CheckForClan();
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
            SetClan("Falcon");
        }

        if (catImage!= null && catImage == gameObject)
        {
            SetClan("Cat");
        }

        if (foxImage != null && foxImage == gameObject)
        {
            SetClan("Fox");
        }

        if (dragonImage != null && dragonImage == gameObject)
        {
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
        chooseClanText.text = "Clan Chosen";
        switch (clanName)
        {
            case "Falcon":
                falconText.text = "Falcon";
                catText.text = "";
                dragonText.text = "";
                foxText.text = "";              
                break;
            case "Cat":
                falconText.text = "";
                catText.text = "Cat";
                dragonText.text = "";
                foxText.text = "";
                break;
            case "Dragon":
                falconText.text = "";
                catText.text = "";
                dragonText.text = "Dragon";
                foxText.text = "";
                break;
            case "Fox":
                falconText.text = "";
                catText.text = "";
                dragonText.text = "";
                foxText.text = "Fox";
                break;
        }
    }
}
