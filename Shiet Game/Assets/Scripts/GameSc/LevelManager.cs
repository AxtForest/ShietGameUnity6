using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Dreamteck.Splines;
public class LevelManager : MonoBehaviour
{
    [SerializeField] SimpleRunnerMovement player;
    [SerializeField] private List<GameObject> levels;
    

    [SerializeField] private GameObject finalRoad;
    private GameObject currentLevel;

    public static LevelDataSc CurrentLevelData;

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

        int index = CurrentLevel % levels.Count;
        currentLevel = Instantiate(levels[index]);

        LevelDataSc levelData = currentLevel.GetComponent<LevelDataSc>();
        CurrentLevelData = levelData;


        player.AssignNewSpline(levelData.levelSpline);
        finalRoad.transform.SetPositionAndRotation(levelData.levelEnd.position, levelData.levelEnd.rotation);
   
    }
    
    public void NextLevel()
    {
        
        CurrentLevel++;
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
    public  void RetryButton()
    {
        
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

}
