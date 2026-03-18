using UnityEngine;

public class LandingManager : MonoBehaviour
{

    public int multiplier = 0;
    [SerializeField] private JumpPad poopSpawner;
    [SerializeField] private SimpleRunnerMovement Movement;
    [SerializeField] private UIManager UIManagerSc;



    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
            return;

        poopSpawner.StopSpawning();
        Movement.LandingAnim(); 
        Movement.PlayLandEffect(); 


        if(multiplier <= 0)
        {

            UIManagerSc.GetGameOver();

        }
        else
        {

            CoinManager.Instance.ApplyLanding(multiplier);
            UIManagerSc.GetSuccess();

        }
    }
    }
