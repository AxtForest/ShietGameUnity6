using UnityEngine;

public class AirMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerConvert playerConvert;

    [Header("Air Boost")]
    [SerializeField] float boostCooldown = 1f;   // kaç sn'de bir
    [SerializeField] float upForce = 6f;
    [SerializeField] int foodCost = 5;

    private float timer;
    bool active;
    void Update()
    {


        if (!active) return;
        if (playerConvert.totalFood < foodCost) return;

        timer += Time.deltaTime;

        if (timer >= boostCooldown)
        {
            Boost();
            timer = 0f;
        }
    }

    void Boost()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        playerConvert.Consume(foodCost);
        // kaka spawn burada
    }
    public void StartAirMovement()
    {
        active = true;
        timer = 0f;
    }

    public void StopAirMovement()
    {
        active = false;
    }
}
