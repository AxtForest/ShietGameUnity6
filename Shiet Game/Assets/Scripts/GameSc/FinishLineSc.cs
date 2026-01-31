using UnityEngine;

public class FinishLineSc : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticle;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; //TAGDEN KURTUL TO DO
        finishParticle.Play();

        SimpleRunnerMovement movement = other.GetComponent<SimpleRunnerMovement>();


        if (movement != null)
        {
           
            movement.StopHorizontalControl();
        }
    }
}
