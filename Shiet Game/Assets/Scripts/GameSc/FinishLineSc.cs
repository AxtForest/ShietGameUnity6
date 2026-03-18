using UnityEngine;
using Unity.Cinemachine;
public class FinishLineSc : MonoBehaviour
{
    [SerializeField] ParticleSystem finishParticle;

    [SerializeField] CinemachineCamera runCam;
    [SerializeField] CinemachineCamera finishCam;
    [SerializeField] CameraManager camManager;

    void OnTriggerEnter(Collider other)
    {

        finishParticle.Play();


        camManager.MoveCameraOffset();
       
        
        SimpleRunnerMovement movement = other.GetComponent<SimpleRunnerMovement>();

            
            movement.StopHorizontalControl();
            movement.AlignToCenter();
            movement.GetActiveAnimator();

        movement.gameObject.transform.Rotate(0f, 0f, 0f);
        
    }

    
}
