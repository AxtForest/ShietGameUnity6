using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 startPos;

    [SerializeField] private PlayerConvert Manager;

    void Start()
    {
        startScale = transform.localScale; 
        startPos = transform.localPosition;

        transform.localScale = Vector3.zero;
        transform.DOScale(startScale, 0.4f).SetEase(Ease.OutBack);

        // Dönme
        transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

        // Yukarı–aşağı
        transform.DOLocalMoveY(startPos.y + 0.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    public void Collect()
    {
        transform.DOKill(); //animasyon durdurma
        transform.DOScale(0f, 0.12f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        
            Manager.AddFood(1);
            CoinManager.Instance.Add(1);
            Collect();
        
    }
}
