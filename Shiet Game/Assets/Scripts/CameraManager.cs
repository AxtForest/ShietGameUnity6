using UnityEngine;
using Unity.Cinemachine; 
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera mainCam;
    [SerializeField] private Vector3 finishOffset = new Vector3(2.6f, 5.87f, -9f); // finish offset
    [SerializeField] private float transitionDuration = 1.5f; 
    [SerializeField] Vector3 targetAngle = new Vector3(20f, -12.8f, 0f); //finish açısı


    private bool isFinished = false;

   

    public void MoveCameraOffset()
    {
       

        mainCam.LookAt = null;

        var composer = mainCam.GetComponent<CinemachinePositionComposer>();

        if (composer != null)
        {
            // DOTween.To() metodu ile özel bir değişkeni hareket ettirebilirz
            // 1. Parametre: Neyi değiştireceğiz? (Mevcut değerim)
            // 2. Parametre: Değer değiştikçe nereye yazacağız?
            // 3. Parametre: hedef değer (finishOffset)
            // 4. Parametre: ne kadar sürecek (transitionDuration)

            DOTween.To(() => composer.TargetOffset, x => composer.TargetOffset = x, finishOffset, transitionDuration)
                   .SetEase(Ease.InOutSine); 
        }
        

        mainCam.transform.DORotate(targetAngle, transitionDuration, RotateMode.Fast).SetEase(Ease.InOutSine);

    }
}

