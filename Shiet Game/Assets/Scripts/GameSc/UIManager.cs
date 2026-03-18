using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject successPanel;



    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        successPanel.SetActive(false);
    }
    public void Success()
    {

        successPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
      // iyi bir çözüm mü ?
    public void GetGameOver()
    {
        Invoke("GameOver", 1.5f);
    }
    public void GetSuccess()
    {
        Invoke("Success", 1.5f);
    }

}
