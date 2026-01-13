using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class ForwardMove : MonoBehaviour
{


    [SerializeField] SplineComputer splineComputer;
    [SerializeField] SplineFollower splineFollower;

    [SerializeField] float forwardSpeed;

    [SerializeField] Animator animator;


    bool isMoving;

    void Start()
    {
        splineFollower.follow = false;
        animator.SetBool("Run", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Input.touchCount > 0)
        {
            StartForwardMove();
            isMoving = true;
        }

    }
    void StartForwardMove()
    {
        splineFollower.follow = true;
        splineFollower.followSpeed = forwardSpeed;
        animator.SetBool("Run", true); // run
    }
    
}
