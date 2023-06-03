using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shimmer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float shimmerSpeed = 2f;
    public float shimmerRange = 0.2f;

    private float time;

    void Update()
    {
        time += Time.deltaTime;
        float shimmer = Mathf.PingPong(time * shimmerSpeed, shimmerRange);
        Color shimmerColor = new Color(1f, 1f, 1f, 1f - shimmer);
        textMeshPro.color = shimmerColor;
    }
}
