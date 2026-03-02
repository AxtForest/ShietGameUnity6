using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public static int CurrentLevel //my first property :)
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }

    public static void StartGame()
    {
        Debug.Log("popo");
        SceneManager.LoadScene("Level" + CurrentLevel);
    }

    public static void LoadNextLevel()
    {
        CurrentLevel++;

        // Eğer level sayısını geçtiyse başa sar
        if (CurrentLevel > SceneManager.sceneCountInBuildSettings - 1)
        {
            CurrentLevel = 1;
        }

        SceneManager.LoadScene("Level" + CurrentLevel);
    }
    public static void RetryButton()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ResetProgress()
    {
        CurrentLevel = 1;
    }
}
