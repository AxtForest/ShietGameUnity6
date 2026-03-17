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

    [SerializeField] private GameObject finalRoad;

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

    void LoadLevel() // load ile destroyu ayır
    {

        //if (currentLevel != null)
            Destroy(currentLevel);

        int index = CurrentLevel % levels.Count;  // sonsuz level döngüsü 

        //  0-Level1 1-Level2 2-Level3 3-Level4 4-Level5


        // 0. 0 % 5 = 0  Level1 
        // 1. 1 % 5 = 1  Level2 
        // 2. 2 % 5 = 2  Level3 
        // 3. 3 % 5 = 3  Level4 
        // 4. 4 % 5 = 4  Level5 
        // 5. 5 % 5 = 0  Level1 
        // 6. 6 % 5 = 1  Level2 
        // 7. 7 % 5 = 2  Level3 
        // 8. 8 % 5 = 3  Level4 
        // 9. 9 % 5 = 4  Level5 
        // 10. 10 % 5 = 0  Level1 




       currentLevel = Instantiate(levels[index]);
        SplineComputer levelSpline = currentLevel.GetComponentInChildren<SplineComputer>(); //prefabin içindeki spline

       
        player.AssignNewSpline(levelSpline);
        

        Transform endPoint = currentLevel.transform.Find("LevelEnd");//fix

        
        finalRoad.transform.SetPositionAndRotation(endPoint.position, endPoint.rotation);
        

        cameraChanger.SetDefaultCamera();
    }
    public void NextLevel()
    {
        //successUI.SetActive(false);
        CurrentLevel++;
        //player.ResetPlayer();
        //LoadLevel();
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
    public  void RetryButton()
    {
        //successUI.SetActive(false);
        //player.ResetPlayer();
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
   
}
