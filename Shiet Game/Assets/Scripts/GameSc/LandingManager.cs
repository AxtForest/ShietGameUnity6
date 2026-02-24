using UnityEngine;

public class LandingManager : MonoBehaviour
{

    public int multiplier = 0;
    [SerializeField] private JumpPad poopSpawner;
    [SerializeField] private PlayerConvert playerConvertSC;
    [SerializeField] private SimpleRunnerMovement Movement;


    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject successPanel;

    private void OnCollisionEnter(Collision collision)
    {
        

       

        poopSpawner.StopSpawning();

        Movement.LandingAnim(); 

        
        playerConvertSC.PlayLandEffect();


        if(multiplier <= 0)
        {

            Invoke("GameOver", 1.5f);
        }
        else
        {
            CoinManager.Instance.ApplyLanding(multiplier);
            Invoke("Success", 1.5f);
            
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        successPanel.SetActive(false);
    }
    private void Success()
    {
        
        successPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    }
