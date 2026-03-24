using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    
    public int Coin;
   
    
    [SerializeField] private Text coinText;
    [SerializeField] private Text finalCoinText;
    private void Awake()
    {
        
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }
    private void Update()
    {
        
    }
    public void Add(int amount)
    {
        Coin += amount;
        coinText.text = Coin.ToString();

    }
    public void Remove(int amount)
    {
        if(Coin > 0)
        Coin -= amount;
        coinText.text = Coin.ToString();

    }
    public void ApplyLanding(int multiplier)
    {
        Coin *= multiplier;
        Debug.Log("Final Score: " + Coin);

        finalCoinText.text = Coin.ToString();
    }


}
