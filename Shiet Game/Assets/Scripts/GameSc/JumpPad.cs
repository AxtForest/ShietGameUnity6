using UnityEngine;
using Dreamteck.Splines;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var follower = other.GetComponentInParent<SplineFollower>();
        var rb = other.GetComponent<Rigidbody>();
        var anim = other.GetComponentInChildren<Animator>();

        var air = other.GetComponent<AirMovement>();
        if (air != null)
            air.StartAirMovement();



        follower.follow = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;


        

        Vector3 jumpDir = (other.transform.forward + Vector3.up).normalized; 
        rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);


        anim.CrossFade("Idle", 0f, 0); //jump animle değişcek


        void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Ground")) return;

            var air = GetComponent<AirMovement>();
            if (air != null)
                air.StopAirMovement();
        }

    }
}
