using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowShimmer : MonoBehaviour
{
    public Image arrow;

    public float shimmerSpeed = 2f;
    public float shimmerRange = 0.2f;

    private float time;
    
    void Awake()
    {
        arrow = GetComponent<Image>();
    }

    void Update()
    {
        time += Time.deltaTime;
        float shimmer = Mathf.PingPong(time * shimmerSpeed, shimmerRange);
        // Color shimmerColor = new Color(0f, 0f, 0f, 1f - shimmer);
        Color shimmerColour = arrow.color;
        shimmerColour.a = 1f - shimmer;
        arrow.color = shimmerColour;
    }
}
