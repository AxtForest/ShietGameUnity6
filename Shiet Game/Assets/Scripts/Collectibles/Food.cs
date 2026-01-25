using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{

    private Vector3 startScale;
    private Vector3 startPos;
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
        transform.DOKill(); 
        transform.DOScale(0f, 0.12f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerConvert manager = FindObjectOfType<PlayerConvert>();

        if (manager != null)
        {
            manager.AddFood(1);

            Collect();
        }
    }
}
