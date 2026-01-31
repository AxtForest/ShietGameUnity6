using UnityEngine;
using Dreamteck.Splines;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 12f;
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
        //var anim = other.GetComponentInChildren<Animator>();

        follower.follow = false;

        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;

        Vector3 jumpDir = (other.transform.forward + Vector3.up).normalized;
        rb.AddForce(jumpDir * jumpForce, ForceMode.Impulse);

        //anim?.CrossFade("Jump", 0f, 0);
        //jumpFX?.Play();

    }
}
