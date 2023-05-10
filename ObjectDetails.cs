using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDetails : MonoBehaviour
{
    int cardValue;
    string house;

    public TextMeshProUGUI cardValueText;
    public TextMeshProUGUI houseText;

    public int CardValue
    { 
        get { return cardValue;} 
        set { cardValue = value; } 
    }

    public string House
    { 
        get { return house;} 
        set { house = value; }  
    }
}
