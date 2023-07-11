using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject playerScoreCircles;
    [SerializeField] GameObject compyScoreCircles;
    [SerializeField] GameObject clanScoreCircles;
    [SerializeField] GameObject aceCat;
    [SerializeField] GameObject kingFox;
    [SerializeField] GameObject sevenDragon;
    [SerializeField] GameObject fiveFox;
    [SerializeField] GameObject middle;

    [SerializeField] TextMeshProUGUI displayMessage;

    SceneStuffs sceneStuffs;
    Collider2D colliderScreen;

    public float delay = 0.04f;

    private string originalMessage;
    private string currentText;
    private float timer;

    int index;

    string[] messages;
    
    private void Awake() 
    {
        sceneStuffs = FindObjectOfType<SceneStuffs>();
        colliderScreen = GetComponent<Collider2D>();    
        currentText = "";
        
    }

    void Start()
    {
        messages = new string[]{
            "Firstly, lets have a look at the game board.",
            "The number on the top left is your total points for the round.",
            "Directly underneath that shows what you scored on your last move.",
            "Similarly, on the right is your opponent's score and their last scored points.",
            "These numbers at the bottom are the clan scores, but we'll go into that later.",
            "After the cards have been dealt a virtual coin toss will randomly pick whether you or the computer goes first.",
            "You want to play a card with a higher value than the card in play - the card in the middle.",
            "If you lay a higher card you will gain the difference between the two cards' values in points.",
            "So if you lay the Ace of Cats here you will score 3 points (aces are high).",
            "However, if you match the clan you score double. So this King of Foxes will score you 4.",
            "If you can only lay a lower card you will lose the difference in points. Playing this seven would lose you 4 points.",
            "However, if you match the clan on a lower-played card you will 'block' and no points will be lost.",
            "Now, the clan system.",
             "When you first play the game you will be asked to choose a clan.", 
            "You can change this later by pressing reset at the menu screen; all points will be reset.",
            "The computer will play for the other three.",
            "You gain clan points by playing a scoring move with your chosen clan.",
            "In this example we're playing for clan Fox, so playing the King of Foxes would also get you four clan points.",
            "You only get to keep those points if you win the round!",
            "Get the most points for your clan by 6pm every Sunday.",
            "That's it! GOOD LUCK!"
        };
        originalMessage = messages[index];
        StartCoroutine(TypeWriter());
    }


    void OnMouseDown() 
    {
        if (index >= messages.Length - 1)
        {
            sceneStuffs.LoadMenu();
            return;
        }
        
        index++;
        
        switch(index)
        {
            case 1:
                playerScoreCircles.SetActive(true);
                break;
            case 3:
                playerScoreCircles.SetActive(false);
                compyScoreCircles.SetActive(true);
                break;
            case 4:
                compyScoreCircles.SetActive(false);
                clanScoreCircles.SetActive(true);
                break;
            case 5:
                clanScoreCircles.SetActive(false);
                break;
            case 6:
                middle.SetActive(true);
                break;
            case 7:
                middle.SetActive(false);
                break;
            case 8:
                kingFox.SetActive(true);
                break;
            case 9:
                kingFox.SetActive(false);
                aceCat.SetActive(true);
                break;
            case 10:
                aceCat.SetActive(false);
                sevenDragon.SetActive(true);
                break;
            case 11:
                sevenDragon.SetActive(false);
                fiveFox.SetActive(true);
                break;
            case 12:
                fiveFox.SetActive(false);
                clanScoreCircles.SetActive(true);
                break;
            case 17:
                aceCat.SetActive(true);
                break;
            case 18:
                aceCat.SetActive(false);
                clanScoreCircles.SetActive(false);
                break;
        }
        
        if (messages.Length > index) 
        {
            originalMessage = messages[index];
            StartCoroutine(TypeWriter());
        }
        
    }

    IEnumerator TypeWriter()
    {
        if (originalMessage.Length != currentText.Length)
        {
            yield return null;
        }
        
        for (int i = 0; i < originalMessage.Length; i++)
        {
            currentText = originalMessage.Substring(0, i + 1);
            displayMessage.text = currentText;

            yield return new WaitForSeconds(delay);
        }
    }
}
