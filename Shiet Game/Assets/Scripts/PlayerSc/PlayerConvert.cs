using Unity.Cinemachine;
using UnityEngine;
using DG.Tweening;



public class PlayerConvert : MonoBehaviour
{
    
    [Header("Stages")]
    [SerializeField] private GameObject[] players;
    [SerializeField] private int foodPerStage = 2;

    private int foodCounter;
    private int currentPlayer;
    private float lastX;

    [SerializeField] ParticleSystem transformFX;

    [SerializeField] ParticleSystem obtacleEffect;

    [SerializeField] GameObject popTarget;
    Vector3 baseScale;
    [SerializeField] float popScale = 0.7f;
    [SerializeField] float popTime = 0.12f;

    private void Start()
    {

        baseScale = popTarget.transform.localScale;
        SetPlayer(0);
        
    }

    public void AddFood(int amount)
    {
        foodCounter += amount;
        PlayPop();
        if (foodCounter >= foodPerStage)
        {
            foodCounter = 0;
            SetPlayer(currentPlayer + 1);
            
        }
    }

    public void SubFood(int amount)
    {
        foodCounter -= amount;
        obtacleEffect.Play();
        if (foodCounter <= -foodPerStage)
        {
            foodCounter = 0;
            SetPlayer(currentPlayer - 1);
        }
    }

    private void SetPlayer(int targetPlayer)
    {

        targetPlayer = Mathf.Clamp(targetPlayer, 0, players.Length -1);
        if (targetPlayer == currentPlayer) return;


        

        lastX = players[currentPlayer].transform.localPosition.x;
        currentPlayer = targetPlayer;

        for (int i = 0; i < players.Length; i++)
            players[i].SetActive(i == currentPlayer);
        transformFX.Play();

        //Transform activePlaver = players[currentPlayer].transform;
        //activePlaver.localPosition = new Vector3(lastX, activePlaver.localPosition.y, activePlaver.localPosition.z);
    }

    public void PlayPop()
    {
        popTarget.transform.DOKill();
        popTarget.transform.DOScale(popScale, popTime).SetEase(Ease.OutSine).OnComplete(() =>
            {
                popTarget.transform.DOScale(baseScale, popTime).SetEase(Ease.InSine);
            });
    }


}
