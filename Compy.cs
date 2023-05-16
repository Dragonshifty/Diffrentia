using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compy : MonoBehaviour
{
    int returnIndex;

    public GameObject CompyChoice(List<GameObject> compyHand, GameObject cardInPlay)
    {
        int[] scoreTemp = new int[5];
        int cardInPlayValue = cardInPlay.GetComponent<ObjectDetails>().CardValue;
        string cardInPlayHouse = cardInPlay.GetComponent<ObjectDetails>().House;

        for (int i = 0; i < compyHand.Count; i++)
        {
            int cardValue = compyHand[i].GetComponent<ObjectDetails>().CardValue;
            int highLowDraw = cardValue.CompareTo(cardInPlayValue);
            bool isAMatch = compyHand[i].GetComponent<ObjectDetails>().House.Equals(cardInPlayHouse);
            int difference = 0;
            switch (highLowDraw)
                {
                    case 0:
                        scoreTemp[i] = 0;
                        break;
                    case 1:
                        difference = cardValue - cardInPlayValue;
                        difference *= 2;
                        scoreTemp[i] = isAMatch ? difference * 2 : difference;
                        break;
                    case -1:
                        difference = cardInPlayValue - cardValue;
                        scoreTemp[i] = isAMatch ? 0 : -difference;
                        break;
                }

        }

        int max = scoreTemp[0];
            for (int i = 0; i < scoreTemp.Length; i++)
            {
                if (max < scoreTemp[i])
                {
                    max = scoreTemp[i];
                    returnIndex = i;
                }
            }

        return compyHand[returnIndex];
    }
}
