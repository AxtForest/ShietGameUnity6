using UnityEngine;
using Unity.Cinemachine;
public class FinishLineSc : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticle;

    [SerializeField] CinemachineCamera runCam;
    [SerializeField] CinemachineCamera finishCam;


    void OnTriggerEnter(Collider other)
    {
        //if (!other.CompareTag("Player")) return; //TAGDEN KURTUL TO DO
        finishParticle.Play();

        runCam.Priority = 0;
        finishCam.Priority = 10;
        SimpleRunnerMovement movement = other.GetComponent<SimpleRunnerMovement>();


        if (movement != null)
        {
           
            movement.StopHorizontalControl();
            movement.AlignToCenter();
        }

        
    }
}
