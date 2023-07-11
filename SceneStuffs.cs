using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneStuffs : MonoBehaviour
{
    [SerializeField] RawImage blackScreen;
    public float fadeSpeed = 1f;

    private void Start() 
    {
        StartCoroutine(FadeIn());    
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));
    }

    public void RestartGame()
    {
        MainGame.Instance.NewGame();
    }

    public void LoadMenu()
    {
        StartCoroutine(FadeOut(1));
    }

    public void LoadMainGame()
    {
        SoundHandler.Instance.PlayDragonFlight();
        StartCoroutine(FadeOut(2));
    }

    public void LoadClanSelection()
    {
        StartCoroutine(FadeOut(4));
    }

    public void LoadWinLose()
    {
        StartCoroutine(FadeOut(3));
    }

    public void LoadWeekwinner()
    {
        StartCoroutine(FadeOut(5));
    }

    public void LoadTutorial()
    {
        StartCoroutine(FadeOut(6));
    }

    public void LoadWinConditions()
    {
        ScoreDataTransfer.Instance.PlayerScore = ScoreMaster.Instance.PlayerScore;
        ScoreDataTransfer.Instance.CompyScore = ScoreMaster.Instance.CompyScore;
        ScoreDataTransfer.Instance.CatScore = ScoreMaster.Instance.HouseCatScore;
        ScoreDataTransfer.Instance.FoxScore = ScoreMaster.Instance.HouseFoxScore;
        ScoreDataTransfer.Instance.DragonScore = ScoreMaster.Instance.HouseDragonScore;
        ScoreDataTransfer.Instance.FalconScore = ScoreMaster.Instance.HouseFalconScore;
        ScoreDataTransfer.Instance.UpdatePlayerPrefs();
    }

    IEnumerator FadeOut(int sceneIndex)
    {
        blackScreen.gameObject.SetActive(true);

        Color colorRef = blackScreen.color;

        float timer = 0f;

        while(timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer /fadeSpeed);
            blackScreen.color = new Color(colorRef.r, colorRef.g, colorRef.b,alpha);
            yield return null;
        }

        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
            case 1:             
                SceneManager.LoadScene(1);
                break;
            case 2:
                SceneManager.LoadScene(2);
                break;
            case 3:
                LoadWinConditions();
                SceneManager.LoadScene(3);
                break;
            case 4:
                SceneManager.LoadScene(4);
                break;
            case 5:
                SceneManager.LoadScene(5);
                break;
            case 6:
                SceneManager.LoadScene(6);
                break;
        }
    }

    private IEnumerator FadeIn()
    {
        blackScreen.gameObject.SetActive(true);

        Color currentColor = blackScreen.color;
        float timer = 0f;

        while (timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeSpeed);
            blackScreen.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
