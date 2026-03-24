using UnityEngine;

public class MultiplierZone : MonoBehaviour
{
    
    [SerializeField] private JumpPad poopSpawner;
    [SerializeField] private SimpleRunnerMovement Movement;
    
    private void OnCollisionEnter(Collision collision)
    {
        

        var anim = collision.gameObject.GetComponentInChildren<Animator>();

        poopSpawner.StopSpawning();

        
                                  
        anim.CrossFade("Landing", 0f, 0);
         
        Movement.PlayLandEffect();

    }
}
