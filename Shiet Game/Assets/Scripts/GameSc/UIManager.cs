using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject successPanel;


    public RectTransform cursor;
    public GameObject arrowUI;


    public static UIManager Instance;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        AnimateCursor();
    }

     void GameOver()
    {
        gameOverPanel.SetActive(true);
        successPanel.SetActive(false);
    }
     void Success()
    {

        successPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
      // iyi bir çözüm mü sanmam?
    public void GetGameOver()
    {
        Invoke("GameOver", 1.5f);
    }
    public void GetSuccess()
    {
        Invoke("Success", 1.5f);
    }


    void AnimateCursor()
    {
        // İlk pozisyonu -315 olarak ayarla
        cursor.anchoredPosition = new Vector2(-315f, cursor.anchoredPosition.y);

        // -315'ten 520'ye hareket etsin, oradan tekrar -315'e dönsün
        cursor.DOAnchorPosX(520f, 2f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }
    public void CloseUI()
    {
       

            cursor.gameObject.SetActive(false); 
            arrowUI.SetActive(false);

        Debug.Log("Swipe UI kapatıldı");
    }

}
