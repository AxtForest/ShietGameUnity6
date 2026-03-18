using UnityEngine;

public class MultiplierZone : MonoBehaviour
{
    public int multiplier = 0;
    [SerializeField] private JumpPad poopSpawner;
    [SerializeField] private SimpleRunnerMovement Movement;
    
    private void OnCollisionEnter(Collision collision)
    {
        CoinManager.Instance.ApplyLanding(multiplier);

        var anim = collision.gameObject.GetComponentInChildren<Animator>();

        poopSpawner.StopSpawning();

        
                                  
        anim.CrossFade("Landing", 0f, 0);
         
        Movement.PlayLandEffect();

    }
}
