using UnityEngine;
using Lean.Touch;
using Dreamteck.Splines;
public class SimpleRunnerMovement : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 8f;
    [SerializeField]
    private float horizontalSpeed = 0.01f;
    [Space]
    [SerializeField]
    private float maxHorizontalMovement = 2f;
    [Space]
    [SerializeField]
    private float horizontalSmooth = 20f;
    [SerializeField]
    private float rotateSmooth = 15f;
    [Space]
    [SerializeField]
    private float maxRotateAngle = 15f;
    [SerializeField]
    private float rotationPower = 40f;

    private bool started;
    private bool dragging;

    private float targetX;
    private float currentRotate;
    private Quaternion baseRot;

    private Animator animator;


    [SerializeField] SplineFollower splineFollower;



    void Start()
    {
        baseRot = transform.rotation;
        animator = GetComponent<Animator>();
        targetX = transform.position.x;
        
    }

    void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerUpdate += OnFingerUpdate;
        
    }

    void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
        LeanTouch.OnFingerUpdate -= OnFingerUpdate;
    }

    void Update()
    {
        if (!started) return;

        Vector3 pos = transform.position;

        float oldX = pos.x;
        float newX = Mathf.Lerp(oldX, targetX, Time.deltaTime * horizontalSmooth);

        pos.x = newX;
        //pos.z += forwardSpeed * Time.deltaTime;
        transform.position = pos;

        float xDifference = newX - oldX;
        float targetRotate = Mathf.Clamp(xDifference * rotationPower, -maxRotateAngle, maxRotateAngle);
        if (!dragging) targetRotate = 0f;

        currentRotate = Mathf.Lerp(currentRotate, targetRotate, Time.deltaTime * rotateSmooth);
        transform.rotation = baseRot * Quaternion.Euler(0f, currentRotate, 0f);
    }


    void OnFingerDown(LeanFinger finger)
    {
        if (!started)
        {
            animator.CrossFade("Run", 0f, 0);
            started = true;
            splineFollower.follow = true;

        }

        dragging = true;
        targetX = transform.position.x;
    }

    void OnFingerUp(LeanFinger finger)
    {
        dragging = false;
    }

    void OnFingerUpdate(LeanFinger finger)
    {
        if (!dragging) return;
        targetX = Mathf.Clamp(targetX + finger.ScaledDelta.x * horizontalSpeed / 100f, -maxHorizontalMovement, maxHorizontalMovement);
    }
}
