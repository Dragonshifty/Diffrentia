using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    SceneStuffs sceneStuffs;
    Collider2D colliderScreen;
    bool canStart = false;
    void Awake() 
    {
        sceneStuffs = FindObjectOfType<SceneStuffs>();
        colliderScreen = GetComponent<Collider2D>();
        canStart = true;
    }
    
    
    void Start()
    {
        
    }

    
    IEnumerator AllowBegin()
    {
        yield return new WaitForSeconds(1);
        colliderScreen.enabled = true;
    }

    void OnMouseDown() 
    {
        SoundHandler.Instance.PlayDragonBegin();
        if (gameObject != null && canStart == true)
        {
            sceneStuffs.LoadMenu();
        }
    }


}
