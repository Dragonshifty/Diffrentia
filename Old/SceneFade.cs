using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    [SerializeField] RawImage blackScreen;
    SceneStuffs sceneStuffs;
    public float fadeSpeed = 1f;
    // private bool isFading = false;

    private void Awake() 
    {
        sceneStuffs = FindObjectOfType<SceneStuffs>();
    }
    private void Start() 
    {
        StartFadeIn();    
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut(0));
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToMainGame()
    {
        StartCoroutine(FadeOut(3));
    }

    IEnumerator LoadMainGame()
    {
        sceneStuffs.LoadMainGame();
        yield return null;
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
        sceneStuffs.LoadMainGame();
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
