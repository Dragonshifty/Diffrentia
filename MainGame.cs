using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
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
    int SortOrderInt = 0;
    bool gameOver = false;
    
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
    }
    
    void Start()
    {
        NewGame();
        // GetNewDeck();
        // SetPlayerHand();
        // moveCards.DistributeCards(playerHand, true);
        // ShowTop();
        // SetCompyHand();
        // moveCards.DistributeCards(compyHand, false);
        // ShowTop();
    }

    void NewGame()
    {
        gameOver = false;
        ClearAll();
        GetNewDeck();
        SetPlayerHand();
        moveCards.DistributeCards(playerHand, true);
        // ShowTop();
        SetCompyHand();
        moveCards.DistributeCards(compyHand, false);
        // ShowTop();
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
        int lastCard = deck.Count -1;
        deck[lastCard].SetActive(true);
        SortOrderInt++;
        deck[lastCard].GetComponent<ObjectDetails>().RaiseSortingOrder(SortOrderInt);
    }

    public void GetCardToPlay(GameObject cardPick)
    {
        if (deck.Count == 0) gameOver = true;
        if (!gameOver){
            cardToPlay = cardPick;
            bool isPlayer = playerHand.Contains(cardToPlay);
            cardToPlay.GetComponent<ObjectDetails>().SetCollider(false);
            cardToPlay.GetComponent<ObjectDetails>().RaiseSortingOrder(SortOrderInt);

            
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
                    cardInPlay = cardToPlay;
                    GetNewCard(isPlayer);
                }
            }
        }
    }

    void GetNewCard(bool isPlayer){
        if (GetDeckLastCardIndex() != 0)
        {
            if (isPlayer)
            {
                playerHand.Add(deck[GetDeckLastCardIndex()]);
                deck.RemoveAt(GetDeckLastCardIndex());
                SortHand(playerHand);
                moveCards.DistributeCards(playerHand, true);
                ShowTop();
                EnablePlayerHand(false);
                RunCompyTurn();
            } else
            {
                compyHand.Add(deck[GetDeckLastCardIndex()]);
                deck.RemoveAt(GetDeckLastCardIndex());
                SortHand(compyHand);
                moveCards.DistributeCards(compyHand, false);
                ShowTop();
            }
        }
    }

    void RunCompyTurn()
    {
        if (deck.Count == 0) gameOver = true;
        GetCardToPlay(compy.CompyChoice(compyHand, cardInPlay));
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
                                    scoreMaster.LastCompyScore = 0;
                                } else
                                {
                                    scoreMaster.LastCompyScore = 0;
                                }
                            break;
                        case 1:
                            if (isAMatch)
                            {
                                int difference = cardToPlayValue - cardInPlayValue;
                                difference *= 2;
                                if (!isPlayer)
                                {
                                    scoreMaster.CompyScore = difference;
                                    scoreMaster.LastCompyScore = difference;
                                } else
                                {
                                    scoreMaster.PlayerScore = difference;
                                    scoreMaster.LastPlayerScore = difference;
                                    scoreMaster.HouseScore(cardToPlayHouse, difference);
                                }
                            }
                            else
                            {
                                int difference = cardToPlayValue - cardInPlayValue;
                                if (!isPlayer)
                                {
                                    scoreMaster.CompyScore = difference;
                                    scoreMaster.LastCompyScore = difference;
                                } else
                                {
                                    scoreMaster.PlayerScore = difference;
                                    scoreMaster.LastPlayerScore = difference;
                                    scoreMaster.HouseScore(cardToPlayHouse, difference);
                                }
                            }
                            break;
                        case -1:
                            if (isAMatch)
                            {
                                if (!isPlayer)
                                {
                                    scoreMaster.LastCompyScore = 0;
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
                                    scoreMaster.CompyScore = -difference;
                                    scoreMaster.LastCompyScore = difference;
                                } else
                                {
                                    scoreMaster.PlayerScore = -difference;
                                    scoreMaster.LastPlayerScore = -difference;
                                }
                            }
                            break;
                    }
            Debug.Log($"Player: {scoreMaster.PlayerScore} Last: {scoreMaster.LastPlayerScore}");
            Debug.Log($"Compy: {scoreMaster.CompyScore} Last: {scoreMaster.LastCompyScore}");
            Debug.Log($"Fox: {scoreMaster.HouseFoxScore} Cat: {scoreMaster.HouseCatScore} Dragon: {scoreMaster.HouseDragonScore} Owl: {scoreMaster.HouseOwlScore}");
        }
    }
}
