using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject playerScoreCircles;
    [SerializeField] GameObject compyScoreCircles;
    [SerializeField] GameObject clanScoreCircles;

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
            "Firstly, lets have a look at the UI.",
            "The number circled in red on the left is your total score for the round.",
            "Directly underneath that, circled in green, shows what you scored on your last move.",
            "Similarly, on the right is your opponent's score and their last scored points.",
            "These circled numbers at the bottom are the clan scores for the round, but we'll go into that later.",
            "After the cards have been dealt a virtual coin toss will randomly pick whether you or the computer goes first.",
            "Ideally, you want to play a card with a higher value than the card in play - the card in the middle.",
            "If you lay a higher card you will gain the difference in the two card's values in points. So lay a 10 on an 8 to gain two points.",
            "If you manage to match the clan to the card in play you will score double. So a 10 on a matched 8 will score 4.",
            "If you can only lay a lower card you will lose the difference in points.",
            "However, if you match the clan on a lower-played card you will 'block' and no points will be lost (or gained).",
            "Now, the clan system: when you first play the game you will be asked to choose a clan. You can change this later by pressing reset at the menu screen but you all points will reset, too.",
            "You will be representing that clan and the computer will play for the other three.",
            "You gain clan points by playing a scoring move with your home clan in a game, as will the computer for the other three clans.",
            "However, you only get to keep those points if you win the round!",
            "The aim is gain the most points for your clan by 6pm every Sunday.",
            "That's it! GOOD LUCK!"
        };
        originalMessage = messages[index];
        StartCoroutine(TypeWriter());
    }


    void OnMouseDown() 
    {
        
        // SoundHandler.Instance.PlayDragonBegin();
        if (index >= messages.Length - 1)
        {
            // index = 0;
            sceneStuffs.LoadMenu();
            
        }
        
        index++;
        
        switch(index)
        {
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
            case 11:
                clanScoreCircles.SetActive(true);
                break;
        }
        originalMessage = messages[index];
        if (messages.Length < index) StartCoroutine(TypeWriter());
        
    }

    IEnumerator TypeWriter()
    {

        // while (currentText.Length < originalMessage.Length)
        // {
        //     timer += Time.deltaTime;
        //     if (timer >= delay)
        //     {
        //         timer = 0f;
        //         currentText = originalMessage.Substring(0, currentText.Length + 1);
        //         displayMessage.text = currentText;
        //     }
        // } 
        // yield return null;     

        for (int i = 0; i < originalMessage.Length; i++)
    {
        currentText = originalMessage.Substring(0, i + 1);
        displayMessage.text = currentText;
        yield return new WaitForSeconds(delay);
    }
    }


    public void StartScrolling()
    {

        if (displayMessage != null)
        {
            originalMessage = displayMessage.text;
        }

    }
}
