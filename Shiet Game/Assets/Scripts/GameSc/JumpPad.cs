using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float extraForcePerFood = 0.2f;
    [SerializeField] private float maxForce = 5f;

    [SerializeField] private PlayerConvert playerConvert;


    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPoint;

    private Coroutine spawnRoutine;


    private void OnTriggerEnter(Collider other)
    {
      
        var player = other.GetComponent<SimpleRunnerMovement>();
        var anim = other.GetComponentInChildren<Animator>();

        player.StartJumpSection();
        
        int foodCount = CoinManager.Instance.Coin;

        float extraForce = Mathf.Clamp(foodCount * extraForcePerFood, 0f, maxForce);

        Debug.Log("Extraforce =" + extraForce);//dengeleme testi

        
        Vector3 jumpDir = (other.transform.forward + Vector3.up).normalized;
        player.rb.AddForce(jumpDir * (jumpForce + extraForce), ForceMode.Impulse);

        //player.anim.CrossFade("Jump", 0f, 0); donuşmeden kaynaklı
        //veri kaybı sebebiyle çalışmıyo

        anim.CrossFade("Jump", 0f, 0);


        StartSpawning();

    }
     public void StartSpawning()
    {
         spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            
            Instantiate(poopPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.2f); // spawn aralığı
        }
    }
}
