using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class JumpPad : MonoBehaviour
{
    private float jumpForce = 10f;
    [SerializeField] private float maxForce = 5f;
    [SerializeField] private float minForce = 0.5f;

    [SerializeField] private PlayerConvert playerConvert;


    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPoint;

    private Coroutine spawnRoutine;
    private bool isSpawning;



    private void OnTriggerEnter(Collider other)
    {
      
        var player = other.GetComponent<SimpleRunnerMovement>();
        var anim = other.GetComponentInChildren<Animator>();

        player.StartJumpSection();
        
        int foodCount = CoinManager.Instance.Coin;

        

        float extraForce = Remap(foodCount, 0f, 30f, minForce, maxForce);

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
        isSpawning = true;
         spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
            isSpawning = false;
            StopCoroutine(spawnRoutine);
           
    }
    IEnumerator SpawnLoop()
    {
        while (isSpawning)

        {
            
            Instantiate(poopPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(0.2f); // spawn aralığı
        }
    }
    float Remap(float value, float srcMin, float srcMax, float dstMin, float dstMax)
    {
        float t = Mathf.InverseLerp(srcMin, srcMax, value);//value min max aralığının yüzde kaçı ?   0-30 15 ise t = 0.5
        return Mathf.Lerp(dstMin, dstMax, t); // min force max force degerinin tam ortasına git t = 0.5 ise mesela
                                              // 15 food aldığımızda force değeri 5 olcak mesela
    }
}
