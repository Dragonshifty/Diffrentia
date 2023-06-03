using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSplash : MonoBehaviour
{
    float spinSpeed = 30f;
    // bool isRotating = true;
    public bool isOuter = false;
    public bool isOuterInner = false;
    public bool isInner = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOuter) transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        if (isOuterInner) transform.Rotate(Vector3.forward, -spinSpeed * Time.deltaTime);

        if (isInner) transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);

    }
}
