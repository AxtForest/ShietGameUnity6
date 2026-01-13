using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  
    [SerializeField] float horizontalSpeed = 0.01f;
    [SerializeField] float minX = -2f;
    [SerializeField] float maxX = 2f;


    [SerializeField] float maxRotateAngle = 15f;
    [SerializeField] float rotateSmooth = 10f;

    [SerializeField] float smoothSpeed = 10f;

    float currentRotate = 0f;

    float deltaPosX;




    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        HandleTouch();

       
    }

    void HandleTouch()
    {
        if (Input.touchCount == 0)
        {
            
            currentRotate = Mathf.Lerp(currentRotate, 0f, Time.deltaTime * rotateSmooth);
            transform.rotation = Quaternion.Euler(0, currentRotate, 0);
            return;
        }

        Touch touch = Input.GetTouch(0);
         deltaPosX = touch.deltaPosition.x;

        if (touch.phase == TouchPhase.Moved)
        {
            
            MovePlayer();
            RotatePlayer();
        }
       
        
    }
    void MovePlayer()
    {
        Vector3 targetPos = transform.position;

        float clampX = Mathf.Clamp(targetPos.x+deltaPosX, minX, maxX);
        targetPos.x = clampX;
        transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * smoothSpeed);


    }
    void RotatePlayer()
    {

        float targetRotate = Mathf.Clamp(deltaPosX, -1f, 1f) * maxRotateAngle;
        currentRotate = Mathf.Lerp(currentRotate, targetRotate, Time.deltaTime * rotateSmooth);
        transform.rotation = Quaternion.Euler(0, currentRotate, 0);
    }

}




