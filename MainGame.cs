using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardsRemainingText;
    [SerializeField] Image quadRemaning;
    [SerializeField] Image foxImage;
    [SerializeField] Image catImage;
    [SerializeField] Image dragonImage;
    [SerializeField] Image falconImage;
    [SerializeField] Sprite foxSprite;
    [SerializeField] Sprite catSprite;
    [SerializeField] Sprite dragonSprite;
    [SerializeField] Sprite falconSprite;

    public static MainGame Instance { get; private set; }
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();
    List<GameObject> compyHand = new List<GameObject>();
    List<GameObject> pile = new List<GameObject>();
    MoveCards moveCards;
    CreateCards createCards;
    GameObject topCard;
    GameObject cardInPlay;
    GameObject cardToPlay;
    Compy compy;
    ScoreMaster scoreMaster;
    WinLoseConditions winLoseConditions;
    SceneStuffs sceneStuffs;
    CoinTossHandler coinTossHandler;
    string clan;
    int sortOrderInt = 0;
    bool gameOver = false;
    bool playerWonToss = false;
    
    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        createCards = FindObjectOfType<CreateCards>();
        moveCards = FindObjectOfType<MoveCards>();
        compy = FindObjectOfType<Compy>();
        scoreMaster = FindObjectOfType<ScoreMaster>();
        winLoseConditions = FindObjectOfType<WinLoseConditions>();
        sceneStuffs = FindObjectOfType<SceneStuffs>();
        coinTossHandler = FindObjectOfType<CoinTossHandler>();
        clan = ScoreDataTransfer.Instance.clan;
        ChangeSmallClanImage();
    }
    
    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        gameOver = false;
        ClearAll();
        GetNewDeck();
        SetPlayerHand();
        moveCards.DistributeCards(playerHand, true);
        // EnablePlayerHand(false);
        SetCompyHand();
        moveCards.DistributeCards(compyHand, false);
        InterstitialAd.Instance.LoadAdOrNot();
    }

    public IEnumerator CoinToss()
    {
        System.Random rand = new System.Random();
        int toss = rand.Next(1,3);
        if (toss == 1)
        {
            int pick = rand.Next(0, 5);
            coinTossHandler.WhoStarts(false, true);
            GetCardToPlay(compyHand[pick]);
        } else
        {
            playerWonToss = true;
            coinTossHandler.WhoStarts(true, true);
            StartCoroutine(EnablePlayerHand(true));
        }
        yield return null;
    }

    void ClearAll()
    {
        foreach (GameObject card in deck)
        {
            Destroy(card);
        }
        deck.Clear();
        foreach (GameObject card in pile)
        {
            Destroy(card);
        }
        pile.Clear();
        foreach (GameObject card in playerHand)
        {
            Destroy(card);
        }
        playerHand.Clear();
        foreach (GameObject card in compyHand)
        {
            Destroy(card);
        }
        compyHand.Clear();
        scoreMaster.ResetScores();
        moveCards.newGameIndicator = 0;
        sortOrderInt = 0;
        playerWonToss = false;
    }

    void GetNewDeck()
    {
        deck = createCards.GetNewDeck();
    }

    void SetPlayerHand()
    {
        for (int i = 0; i < 5; i++)
        {
            int lastCard = deck.Count -1;
            playerHand.Add(deck[lastCard]);
            deck.RemoveAt(lastCard);
        }
        SortHand(playerHand);
    }

    public int GetDeckLastCardIndex()
    {
        if (deck.Count != 0) 
        {
            return deck.Count -1;
        } else
        {
            return 0;
        }
    }

    void SetCompyHand()
    {
        for (int i = 0; i < 5; i++)
        {
            int lastCard = deck.Count -1;
            compyHand.Add(deck[lastCard]);
            deck.RemoveAt(lastCard);
        }
        SortHand(compyHand);
    }

    void SortHand(List<GameObject> hand)
    {
        hand.Sort((card1, card2) =>
        {
            int cardValue1 = card1.GetComponent<ObjectDetails>().CardValue;
            int cardValue2 = card2.GetComponent<ObjectDetails>().CardValue;
            return cardValue1.CompareTo(cardValue2);
        });
    }

    public void ShowTop()
    {
        if (deck.Count != 0)
        {
            int lastCard = deck.Count -1;
            deck[lastCard].SetActive(true);
            sortOrderInt++;
            deck[lastCard].GetComponent<ObjectDetails>().RaiseSortingOrder(sortOrderInt);
        } 
    }

    public void GetCardToPlay(GameObject cardPick)
    {
        if (deck.Count == 0)
        {
            gameOver = true;
            // StartCoroutine(EndGame());
        }

        if (!gameOver){
            cardToPlay = cardPick;
            bool isPlayer = playerHand.Contains(cardToPlay);
            cardToPlay.GetComponent<ObjectDetails>().SetCollider(false);
            cardToPlay.GetComponent<ObjectDetails>().RaiseSortingOrder(sortOrderInt);

            
            if (pile.Count == 0)
            {
                if (isPlayer)
                {
                    pile.Add(cardToPlay);
                    int playerHandIndex = playerHand.IndexOf(cardToPlay);
                    moveCards.MoveCardToPile(playerHand[playerHandIndex], isPlayer);
                    playerHand.RemoveAt(playerHandIndex);
                    cardInPlay = cardToPlay;
                    GetNewCard(isPlayer);
                } else
                {
                    pile.Add(cardToPlay);
                    int compyHandIndex = compyHand.IndexOf(cardToPlay);
                    moveCards.MoveCardToPile(compyHand[compyHandIndex], isPlayer);
                    compyHand.RemoveAt(compyHandIndex);
                    cardInPlay = cardToPlay;
                    GetNewCard(isPlayer);
                }
            } else
            {
                if (isPlayer)
                {
                    pile.Add(cardToPlay);
                    int playerHandIndex = playerHand.IndexOf(cardToPlay);
                    RunPoints(isPlayer);
                    moveCards.MoveCardToPile(playerHand[playerHandIndex], isPlayer);
                    playerHand.RemoveAt(playerHandIndex);
                    cardInPlay = cardToPlay;
                    GetNewCard(isPlayer);
                } else
                {
                    pile.Add(cardToPlay);
                    int compyHandIndex = compyHand.IndexOf(cardToPlay);
                    RunPoints(isPlayer);
                    moveCards.MoveCardToPile(compyHand[compyHandIndex], isPlayer);
                    compyHand.RemoveAt(compyHandIndex);
                    // StartCoroutine(UpdateRemainingCardsDelay());
                    cardInPlay = cardToPlay;
                    GetNewCard(isPlayer);
                }
            }
        }
    }

    void GetNewCard(bool isPlayer){
        if (deck.Count != 0)
        {
            if (isPlayer)
            {
                playerHand.Add(deck[GetDeckLastCardIndex()]);
                deck.RemoveAt(GetDeckLastCardIndex());
                cardsRemainingText.text = deck.Count.ToString();
                UpdateQuadRemaning();
                SortHand(playerHand);
                moveCards.DistributeCards(playerHand, true);
                ShowTop();
                EnablePlayerHand(false);
                if (deck.Count == 0)
                {
                    gameOver = true;
                    // StartCoroutine(EndGame());
                    sceneStuffs.LoadWinLose();

                } else
                {
                    RunCompyTurn();
                }
            } else
            {
                compyHand.Add(deck[GetDeckLastCardIndex()]);
                deck.RemoveAt(GetDeckLastCardIndex());
                StartCoroutine(UpdateRemainingCardsDelay());
                SortHand(compyHand);
                moveCards.DistributeCards(compyHand, false);
                ShowTop();
                if (deck.Count == 0)
                {
                    gameOver = true;
                    StartCoroutine(EndGame());
                    // sceneStuffs.LoadWinLose();
                } 
            }
        }
    }

    void RunCompyTurn()
    {
        int deckCount = deck.Count;
        if (deckCount == 0)
        {
            gameOver = true;
            // StartCoroutine(EndGame());
        }
        GetCardToPlay(compy.CompyChoice(compyHand, cardInPlay, deckCount));
    }

    public IEnumerator EnablePlayerHand(bool isEnabled)
    {
        foreach (GameObject card in playerHand)
        {
            card.GetComponent<ObjectDetails>().SetCollider(isEnabled);
        }
        yield return null;
    }


    public void RunPoints(bool isPlayer)
    {
        if (deck.Count > 0)
        {
            int cardInPlayValue = cardInPlay.GetComponent<ObjectDetails>().CardValue;
            int cardToPlayValue = cardToPlay.GetComponent<ObjectDetails>().CardValue;

            int highLowDraw = cardToPlayValue.CompareTo(cardInPlayValue);

            string cardInPlayHouse = cardInPlay.GetComponent<ObjectDetails>().House;
            string cardToPlayHouse = cardToPlay.GetComponent<ObjectDetails>().House;  

            bool isAMatch = cardInPlayHouse.Equals(cardToPlayHouse);

            switch (highLowDraw)
                    {
                        case 0:
                            if (!isPlayer)
                                {
                                    StartCoroutine(UpdateCompyScoreDelay(0, cardToPlayHouse, false, false));
                                } else
                                {
                                    scoreMaster.LastPlayerScore = 0;
                                }
                            break;
                        case 1:
                            if (isAMatch)
                            {
                                int difference = cardToPlayValue - cardInPlayValue;
                                difference *= 2;
                                if (!isPlayer)
                                {
                                    StartCoroutine(UpdateCompyScoreDelay(difference, cardToPlayHouse, true, true));
                                } else
                                {
                                    scoreMaster.PlayerScore = difference;
                                    scoreMaster.LastPlayerScore = difference;
                                    if (cardToPlayHouse.Equals(clan))
                                    {
                                        scoreMaster.HouseScore(cardToPlayHouse, difference);
                                    }
                                }
                            }
                            else
                            {
                                int difference = cardToPlayValue - cardInPlayValue;
                                if (!isPlayer)
                                {
                                    StartCoroutine(UpdateCompyScoreDelay(difference, cardToPlayHouse, true, true));
                                } else
                                {
                                    scoreMaster.PlayerScore = difference;
                                    scoreMaster.LastPlayerScore = difference;
                                    if (cardToPlayHouse.Equals(clan))
                                    {
                                        scoreMaster.HouseScore(cardToPlayHouse, difference);
                                    }
                                }
                            }
                            break;
                        case -1:
                            if (isAMatch)
                            {
                                if (!isPlayer)
                                {
                                    StartCoroutine(UpdateCompyScoreDelay(0, cardToPlayHouse, false, false));
                                } else
                                {
                                    scoreMaster.LastPlayerScore = 0;
                                }
                            }
                            else
                            {
                                int difference = cardInPlayValue - cardToPlayValue;
                                if (!isPlayer)
                                {
                                    StartCoroutine(UpdateCompyScoreDelay(-difference, cardToPlayHouse, true, false));
                                } else
                                {
                                    scoreMaster.PlayerScore = -difference;
                                    scoreMaster.LastPlayerScore = -difference;
                                }
                            }
                            break;
                    }
        }
    }

    IEnumerator UpdateCompyScoreDelay(int score, string cardToPlayHouse, bool both, bool notMinus)
    {
        yield return new WaitForSeconds((float)1.6);
        
        if (both)
        {
        scoreMaster.CompyScore = score;
        scoreMaster.LastCompyScore = score;
        if (!cardToPlayHouse.Equals(clan) && notMinus)
            {
                scoreMaster.HouseScore(cardToPlayHouse, score);
            }
        } else
        {
            scoreMaster.LastCompyScore = score;
        }
    }

    IEnumerator UpdateRemainingCardsDelay()
    {
        yield return new WaitForSeconds((float)1.6);
        cardsRemainingText.text = deck.Count.ToString();
        UpdateQuadRemaning();
    }

    IEnumerator EndGame()
    {
        float waitTime = playerWonToss ? 2.5f : 4f;
        yield return new WaitForSeconds((float)waitTime);
        
        // sceneStuffs.LoadWinConditions();
        sceneStuffs.LoadWinLose();
    }
    
    void UpdateQuadRemaning()
    {
        float fillLevel = (float)deck.Count / 42f;
        quadRemaning.fillAmount = fillLevel;
    }

    void ChangeSmallClanImage()
    {
        switch (clan)
        {
            case "Fox":
                foxImage.sprite = foxSprite;
                break;
            case "Cat":
                catImage.sprite = catSprite;
                break;
            case "Dragon":
                dragonImage.sprite = dragonSprite;
                break;
            case "Falcon":
                falconImage.sprite = falconSprite;
                break;     
        }
    }

}
