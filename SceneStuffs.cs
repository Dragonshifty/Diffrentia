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
        // SceneManager.LoadScene(0);
        MainGame.Instance.NewGame();
    }

    public void LoadMainGame()
    {
        SoundHandler.Instance.PlayDragonFlight();
        StartCoroutine(FadeOut(2));
        // SceneManager.LoadScene(0);
    }

    public void LoadClanSelection()
    {
        StartCoroutine(FadeOut(1));
    }

    public void LoadWinLose()
    {
        StartCoroutine(FadeOut(3));
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
        // SceneManager.LoadScene(1);
    }

    IEnumerator FadeOut(int sceneIndex)
    {
        // isFading = true;
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

        // isFading = false;
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
        }
    }

    private IEnumerator FadeIn()
    {
        // isFading = true;
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

        // isFading = false;
        blackScreen.gameObject.SetActive(false);
    }
}
