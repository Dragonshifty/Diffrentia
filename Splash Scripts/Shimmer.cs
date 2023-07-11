using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Shimmer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float shimmerSpeed = 2f;
    public float shimmerRange = 0.2f;

    public float delayBetweenCharacters = 0.1f;

    private float time;
        
    void Update()
    {
        time += Time.deltaTime;
        float shimmer = Mathf.PingPong(time * shimmerSpeed, shimmerRange);
        Color shimmerColor = new Color(0f, 0f, 0f, 1f - shimmer);
        textMeshPro.color = shimmerColor;
    }
}
