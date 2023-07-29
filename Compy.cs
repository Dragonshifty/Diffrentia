using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compy : MonoBehaviour
{
    public GameObject CompyChoice(List<GameObject> compyHand, GameObject cardInPlay, int deckCount)
    {
        int[] scoreTemp = new int[5];
        int[] baseValues = new int[5];
        int cardInPlayValue = cardInPlay.GetComponent<ObjectDetails>().CardValue;
        string cardInPlayHouse = cardInPlay.GetComponent<ObjectDetails>().House;
        

        for (int i = 0; i < compyHand.Count; i++)
        {
            int cardValue = compyHand[i].GetComponent<ObjectDetails>().CardValue;
            int highLowDraw = cardValue.CompareTo(cardInPlayValue);
            bool isAMatch = compyHand[i].GetComponent<ObjectDetails>().House.Equals(cardInPlayHouse);
            baseValues[i] = cardValue;
            int difference = 0;
            switch (highLowDraw)
                {
                    case 0:
                        scoreTemp[i] = 0;
                        break;
                    case 1:
                        difference = cardValue - cardInPlayValue;
                        scoreTemp[i] = isAMatch ? difference * 2 : difference;
                        break;
                    case -1:
                        difference = cardInPlayValue - cardValue;
                        scoreTemp[i] = isAMatch ? 0 : -difference;
                        break;
                }
        }

        int returnIndex = RunPossibleChoice(scoreTemp, deckCount, baseValues);

        return compyHand[returnIndex];
    }

    int RunPossibleChoice(int[] scoreTemp, int deckCount, int[] baseValues)
    {
        int max = scoreTemp[0];
        int secondHighest = int.MinValue;
        int returnIndex = 0;
        int returnIndex2 = -1;

        for (int i = 0; i < scoreTemp.Length; i++)
        {
            if (max < scoreTemp[i])
            {
                secondHighest = max;
                returnIndex2 = returnIndex;
                max = scoreTemp[i];
                returnIndex = i;
            } else if (scoreTemp[i] < max && scoreTemp[i] > secondHighest)
            {
                secondHighest = scoreTemp[i];
                returnIndex2 = i;
            }
        }

        if (deckCount > 20 && max <= 10 && secondHighest >= 2)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 0) returnIndex = returnIndex2;
        }

        if (deckCount > 28 && max < 6)
        {
            int rand = UnityEngine.Random.Range(0, 3);
            if (rand == 0) returnIndex = SaveHigherCard(returnIndex, scoreTemp, baseValues);
        }
       
        return returnIndex;
    }

    int SaveHigherCard(int returnIndex, int[] scoreTemp, int[] baseValues)
    {
        if (baseValues[4] > 9)
        {
            for (int i = 3; i >= 0; i--)
            {
                if (scoreTemp[i] > 0) returnIndex = i;
            }
        }

        if (baseValues[3] > 9)
        {
            for (int i = 2; i >= 0; i--)
            {
                if (scoreTemp[i] > 0) returnIndex = i;
            }
        }
        return returnIndex;
    }
}
