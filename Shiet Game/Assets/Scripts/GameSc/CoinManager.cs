using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private int currency;
    public int Currency => currency;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Add(int amount)
    {
        currency += amount;
    }
    public void Remove(int amount)
    {
        currency -= amount;
    }
}
