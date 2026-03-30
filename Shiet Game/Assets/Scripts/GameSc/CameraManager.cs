using UnityEngine;
using Unity.Cinemachine; 
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera mainCam;
    [SerializeField] private float transitionDuration = 1.5f;
    [SerializeField] private Transform finishPoint;


    public void MoveCameraOffset()
    {

        var finishOffset = finishPoint.localPosition;
        var targetAngle = finishPoint.localEulerAngles;

        mainCam.LookAt = null;

        var cinemachineComposer = mainCam.GetComponent<CinemachinePositionComposer>(); //cinemachinenin composeri (component)

        if (cinemachineComposer != null)
        {
         

         
            DOTween.To(() => cinemachineComposer.TargetOffset, x => cinemachineComposer.TargetOffset = x,finishOffset,transitionDuration);
        }


        mainCam.transform.DORotate(targetAngle, transitionDuration, RotateMode.Fast).SetEase(Ease.InOutSine);
      

    }
}



