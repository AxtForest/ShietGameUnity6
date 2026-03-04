using UnityEngine;
using Unity.Cinemachine;
public class FinishLineSc : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticle;

    [SerializeField] CinemachineCamera runCam;
    [SerializeField] CinemachineCamera finishCam;


    void OnTriggerEnter(Collider other)
    {

        finishParticle.Play();

        runCam.Priority = 0;
        finishCam.Priority = 10;


        SimpleRunnerMovement movement = other.GetComponent<SimpleRunnerMovement>();

            
            movement.StopHorizontalControl();
            movement.AlignToCenter();
            movement.GetActiveAnimator();

        movement.gameObject.transform.Rotate(0f, 0f, 0f);
        
    }
}
