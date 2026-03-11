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


        CameraChanger();

        SimpleRunnerMovement movement = other.GetComponent<SimpleRunnerMovement>();

            
            movement.StopHorizontalControl();
            movement.AlignToCenter();
            movement.GetActiveAnimator();

        movement.gameObject.transform.Rotate(0f, 0f, 0f);
        
    }

    public void CameraChanger()
    {
        runCam.Priority = 0;
        finishCam.Priority = 10;
    }
    public void SetDefaultCamera()
    {
        runCam.Priority = 10;
        finishCam.Priority = 0;
    }
}
