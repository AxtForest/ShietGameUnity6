using UnityEngine;
using Lean.Touch;
using Dreamteck.Splines;
public class SimpleRunnerMovement : MonoBehaviour
{
   
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

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }

    [SerializeField] SplineFollower splineFollower;

    

    void Start()
    {
        baseRot = transform.rotation;
      

        anim = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        

        anim.CrossFade("Idle", 0f, 0);

    }

   
    void OnEnable()
    {
        //Ekrana dokunulunca bu fonksiyonu çalıştırmaya yetkilisin demekmiş
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
        

        //Spline follower ile çakıştığı için locale çektik tek yaptığım şey localposition yazmak

        if (!started) return;

        
        float currentX = transform.localPosition.x;

        float newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * horizontalSmooth);

        transform.localPosition = new Vector3(newX, 0f, 0f);

        
        float xDifference = newX - currentX;
        float targetRotate = Mathf.Clamp(xDifference * rotationPower, -maxRotateAngle, maxRotateAngle);
        if (!dragging) targetRotate = 0f;

        currentRotate = Mathf.Lerp(currentRotate, targetRotate, Time.deltaTime * rotateSmooth);
        transform.localRotation = Quaternion.Euler(0f, currentRotate, 0f);


    }


    void OnFingerDown(LeanFinger finger)
    {
        if (!started)
        {
            anim.CrossFade("Run", 0f, 0);

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

    public void StopHorizontalControl()
    {
        dragging = false;
        enabled = false;  //unitynin update ve start gibi fonksiyonları çalışmıyor direkt kapatıyo bu kod
    }
    public void AlignToCenter()
    {
        
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

    }
    public void StartJumpSection()
    {
        splineFollower.follow = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
    }
    public void GetActiveAnimator()
    {
        anim = GetComponentInChildren<Animator>();

    }
    public void LandingAnim()
    {
        anim.CrossFade("Landing", 0f, 0);
    }
    public void JumpAnim()
    {
        anim.CrossFade("Jump", 0f, 0);
    }
}
