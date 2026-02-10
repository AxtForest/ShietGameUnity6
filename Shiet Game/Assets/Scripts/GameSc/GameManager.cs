using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Text coinText;

    // Update is called once per frame
    void Update()
    {
        coinText.text = CoinManager.Instance.Currency.ToString();
    }
}
