using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Dreamteck.Splines;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject successUI;
    [SerializeField] SimpleRunnerMovement player;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] FinishLineSc cameraChanger;

    private GameObject currentLevel;
    public static int CurrentLevel 
    {
        get => PlayerPrefs.GetInt("Level", 0);
        set => PlayerPrefs.SetInt("Level", value);
    }

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {

        if (currentLevel != null)
            Destroy(currentLevel);

        int index = CurrentLevel % levels.Count; // güvenli mod
        currentLevel = Instantiate(levels[index]);
        SplineComputer levelSpline = currentLevel.GetComponentInChildren<SplineComputer>();

        if (levelSpline != null)
        {
            player.AssignNewSpline(levelSpline);
        }
        else
        {
            Debug.LogWarning("popo");
        }

        cameraChanger.SetDefaultCamera();
    }
    public void NextLevel()
    {
        successUI.SetActive(false);
        CurrentLevel++;
        player.ResetPlayer();
        LoadLevel();
    }
    public  void RetryButton()
    {
        successUI.SetActive(false);
        player.ResetPlayer();
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ResetProgress()
    {
        CurrentLevel = 0;
    }
}
