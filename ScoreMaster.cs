using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    int playerScore;
    int compyScore;
    int lastPlayerScore;
    int lastCompyScore;
    int houseFoxScore;
    int houseCatScore;
    int houseDragonScore;
    int houseOwlScore;

    public int PlayerScore
    {
        get {return playerScore; }
        set {playerScore += value; }
    }

    public int CompyScore
    {
        get { return compyScore; }
        set { compyScore += value; }
    }

    public int LastPlayerScore
    {
        get { return lastPlayerScore; }
        set { lastPlayerScore = value; }
    }

    public int LastCompyScore
    {
        get { return lastCompyScore; }
        set { lastCompyScore = value; }
    }

    public int HouseFoxScore
    {
        get { return houseFoxScore; }
        set { houseFoxScore += value; }
    }

    public int HouseCatScore
    {
        get { return houseCatScore; }
        set { houseCatScore += value; }
    }
    public int HouseDragonScore
    {
        get { return houseDragonScore; }
        set { houseDragonScore += value; }
    }
    public int HouseOwlScore
    {
        get { return houseOwlScore; }
        set { houseOwlScore += value; }
    }
    
    public void HouseScore(string house, int amount)
    {
        switch(house)
        {
            case "Fox":
                houseFoxScore += amount;
                break;
            case "Cat":
                houseCatScore += amount;
                break;
            case "Dragon":
                houseDragonScore += amount;
                break;
            case "Owl":
                houseOwlScore += amount;
                break;
        }
    }

    public void ResetScores()
    {
        playerScore = 0;
        compyScore = 0;
        lastPlayerScore = 0;
        lastCompyScore = 0;
    }
}
