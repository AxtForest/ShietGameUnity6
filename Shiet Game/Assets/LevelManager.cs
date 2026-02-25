using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void OnNextLevelButton()
    {
        Test.LoadNextLevel();
    }

    public void OnMainMenuButton()
    {
        Test.ReturnToMainMenu();
    }
    public void OnRetryButton()
    {
        Test.RetryButton();
    }
}
