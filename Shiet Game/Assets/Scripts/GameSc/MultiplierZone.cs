using UnityEngine;

public class MultiplierZone : MonoBehaviour
{
    public int multiplier = 1;
    [SerializeField] private JumpPad poopSpawner;
    //[SerializeField] private SimpleRunnerMovement Movement;
    private void OnCollisionEnter(Collision collision)
    {
        CoinManager.Instance.ApplyLanding(multiplier);

        var anim = collision.gameObject.GetComponentInChildren<Animator>();

        poopSpawner.StopSpawning();

        //Movement.LandingAnim(); //donuşunce inactive diyor veri kaybı söz konusu

        anim.CrossFade("Landing", 0f, 0);
        //+effect 
    }
}
