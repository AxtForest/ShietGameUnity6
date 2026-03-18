using UnityEngine;
using Dreamteck.Splines;
using System.Collections;

public class JumpPad : MonoBehaviour
{
    private float minForce = 1f;
    private float maxForce = 20f; //ayarlanabilir böyle güzel ama leveldekinin yarısını alsan 3xe gitmediği durumlar oluyor 


    [SerializeField] private PlayerConvert playerConvert;
    [SerializeField] private SimpleRunnerMovement Movement;
    
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPoint;

    

    private Coroutine spawnRoutine;
    private bool isSpawning;



    private void OnTriggerEnter(Collider other)
    {

        var player = other.GetComponent<SimpleRunnerMovement>();
        player.StartJumpSection();

        int foodCount = CoinManager.Instance.Coin;
        int maxFoodCount = LevelManager.CurrentLevelData.maxFood;

        float extraForce = Remap(foodCount, 0f, maxFoodCount, minForce, maxForce);

        
        Debug.Log(extraForce);
        
       Vector3 jumpDir = (other.transform.forward + Vector3.up).normalized;

        player.rb.AddForce(jumpDir * extraForce  , ForceMode.Impulse);


        player.JumpAnim();
        Invoke("StartSpawning",0.5f);

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
