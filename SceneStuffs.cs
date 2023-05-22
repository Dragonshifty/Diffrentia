using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStuffs : MonoBehaviour
{
    public void RestartGame()
    {
        // SceneManager.LoadScene(0);
        MainGame.Instance.NewGame();
    }
}
