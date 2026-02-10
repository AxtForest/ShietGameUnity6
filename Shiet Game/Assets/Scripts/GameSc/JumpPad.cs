using UnityEngine;
using Dreamteck.Splines;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float extraForcePerFood = 0.2f;
    [SerializeField] private float maxForce = 5f;

    [SerializeField] private PlayerConvert playerConvert;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var follower = other.GetComponentInParent<SplineFollower>();
        var rb = other.GetComponent<Rigidbody>();
        var anim = other.GetComponentInChildren<Animator>();
        

        follower.follow = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;


        int foodCount = CoinManager.Instance.Currency;
        float extraForce = Mathf.Clamp(foodCount * extraForcePerFood, 0f, maxForce);

        Debug.Log("Extraforce =" + extraForce);//dengeleme testi


        Vector3 jumpDir = (other.transform.forward + Vector3.up).normalized;
        rb.AddForce(jumpDir * (jumpForce + extraForce), ForceMode.Impulse);


        anim.CrossFade("Idle", 0f, 0); //jump animle değişcek



    }
}
