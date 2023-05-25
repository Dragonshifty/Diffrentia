using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClanSelection : MonoBehaviour
{
    [SerializeField] GameObject falconImage;
    [SerializeField] GameObject catImage;
    [SerializeField] GameObject dragonImage;
    [SerializeField] GameObject foxImage;

    // private void Awake() 
    // {
    //     falconImage = GetComponent<Image>(); 
    //     catImage = GetComponent<Image>();
    //     dragonImage = GetComponent<Image>();
    //     foxImage = GetComponent<Image>();
    // }
    void Start()
    {
        
    }

    void OnMouseDown() 
    {
        if (falconImage != null && falconImage == gameObject) Debug.Log("Falcon");
        if (catImage == gameObject) Debug.Log("Cat"); 
        if (foxImage == gameObject) Debug.Log("Fox"); 
        if (dragonImage == gameObject) Debug.Log("Dragon"); 
           
    }

    void SetClan(string clanName)
    {
        ScoreDataTransfer.Instance.SetClan(clanName);
    }
}
