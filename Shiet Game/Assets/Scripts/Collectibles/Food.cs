using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{

    //  SPAWN 
    // Ease.OutBack        - biraz taşarak pop diye büyür sonra yerine oturur
    // Ease.OutBounce      - top gibi sekerek büyür
    // Ease.OutElastic     - lastik gibi esneyerek büyür
    // Ease.OutExpo        - hızlıca fırlar yumuşakça durur
    // Ease.Linear         - düz büyür

    //  SCALE ANİMASYONU
    // DOScale + OutBack        - Büyüme gibi bişi
    // DOScale + InBack         - içeri çekilerek küçülür yok olur
    // DOScale + OutElastic     - esner gibi büyüyo



    //  ROTATION
    // RotateMode.FastBeyond360 - Sürekli ve akıcı dönme 
    // RotateMode.LocalAxisAdd  - Parent rotation varsa güvenli
    // RotateMode.WorldAxisAdd  - Dünya eksenine göre dönme

    //  LOOP 
    // SetLoops(-1)             - Sonsuz döngü
    // LoopType.Restart         - Baştan başlar 
    // LoopType.Yoyo            - İleri-geri Nefes al ver gibi

    // Yukarı–Aşağı
    // Ease.InOutSine      - Smooth,doğal salınım
    // Ease.InOutQuad      - biraz daha sert, mekanik salınım
    // Ease.Linear         - Robotik, düz hareket

    //  COLLECT / DESTROY
    // Ease.InBack         - İçeri çekilip yok olma hissi
    // Ease.InExpo         - hızlıca söner 
    // Ease.InBounce       - zıplaya zıplaya küçülür

    // InSine        -> yavaş başlar, 
    // OutSine       -> hızlı başlar, yumuşakça durur






   
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
        transform.DOKill(); //animasyon durdurma
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
