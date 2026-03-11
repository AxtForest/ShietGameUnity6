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
        targetX = transform.localPosition.x;
        
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


        enabled = false;
        currentRotate = 0f;
        transform.rotation = Quaternion.identity;



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
    public void ResetPlayer()
    {
        // Animasyonu idle yap
        anim.CrossFade("Idle", 0f, 0);

        // Başlamadı olarak işaretle
        started = false;

        // Horizontal kontrol ve rotation sıfırla
        dragging = false;
        targetX = 0f;
        currentRotate = 0f;

        // --- EKLENEN/DÜZELTİLEN KISIMLAR ---

        enabled = true;

        // 2. Spline üzerinde karakteri en başa alıyoruz
        if (splineFollower != null)
        {
            splineFollower.SetPercent(0f); // Spline'ın başına (%0) atar. (Alternatif: splineFollower.SetDistance(0f);)
            splineFollower.follow = false; // Ekrana tekrar dokunulana kadar ilerlemesini durdurur
        }

        // 3. Eğer StartJumpSection çalıştıysa yerçekimi açılmıştır, onu da başa sarıyoruz
        if (rb != null)
        {
            rb.useGravity = false; // Oyunun başında gravity kapalıysa false yap.
            rb.linearVelocity = Vector3.zero; // Üzerinde kalan fiziksel bir hız varsa sıfırla
        }

        // Player pozisyonunu garanti olsun diye yine sıfırla (Spline ezecek olsa bile local'i sıfırlamak iyidir)
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;

    }
    public void AssignNewSpline(SplineComputer newSpline)
    {
        if (splineFollower != null && newSpline != null)
        {
            // Karakterin takip edeceği yeni yolu ata
            splineFollower.spline = newSpline;

            // Yolu atadıktan sonra garanti olsun diye karakteri yolun en başına al
            splineFollower.SetPercent(0f);
        }
    }
}
