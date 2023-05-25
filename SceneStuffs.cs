using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStuffs : MonoBehaviour
{
    public SceneFade sceneFade;
    private void Awake() 
    {
        sceneFade = FindObjectOfType<SceneFade>();    
    }
    public void RestartGame()
    {
        // SceneManager.LoadScene(0);
        MainGame.Instance.NewGame();
    }

    public void LoadMainGame()
    {
        sceneFade.StartFadeOut();
        SceneManager.LoadScene(0);
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
        SceneManager.LoadScene(1);
    }
}
