using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmCancel : MonoBehaviour
{
    [SerializeField] GameObject popup;

    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    public void Hidepopup()
    {
        popup.SetActive(false);
    }
}
