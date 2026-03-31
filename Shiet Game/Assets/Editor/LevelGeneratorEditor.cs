using UnityEditor;
using UnityEngine;
using Dreamteck.Splines;
using System.Collections.Generic;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Inspector'ı normal şekilde çizdiriyoruz
        DrawDefaultInspector();

        LevelGenerator gen = (LevelGenerator)target;

        GUILayout.Space(10);

        if (GUILayout.Button("GENERATE LEVEL", GUILayout.Height(35)))
        {
            Generate(gen);
        }

        if (GUILayout.Button("CLEAR", GUILayout.Height(25)))
        {
            Clear(gen);
        }
    }

    void Clear(LevelGenerator gen)
    {
        if (gen.generatedParent != null)
        {
            DestroyImmediate(gen.generatedParent.gameObject);
        }
    }

    void Generate(LevelGenerator gen)
    {
        if (gen.spline == null)
        {
            Debug.LogError("Spline yok! laaaa");
            return;
        }

        // Yeni üretim yapmadan önce eskileri temizle
        Clear(gen);

        float length = gen.spline.CalculateLength();
        float distance = gen.startOffset;
        float endLimit = length - gen.endOffset;

       

        gen.generatedParent = new GameObject("GeneratedObjects").transform;
        gen.generatedParent.SetParent(gen.transform);

        // Belirlenen aralıkta objeleri oluşturmaya başla
        while (distance < endLimit)
        {
            SplineSample sample = gen.spline.Evaluate(distance / length);

            // Sağa veya sola rastgele sapma miktarı
            float randomHorizontalOffset = Random.Range(-gen.spawnWidth, gen.spawnWidth);

            // Taban pozisyonunu hesapla
            Vector3 basePos = sample.position + (sample.right * randomHorizontalOffset);

            // Yükseklik offsetlerini ekle
            Vector3 foodPos = basePos + (sample.up * gen.foodHeightOffset);
            Vector3 obstaclePos = basePos + (sample.up * gen.obstacleHeightOffset);

            // 1. Önce engel koymayı dene
            bool spawnedObstacle = TrySpawn(gen.obstaclePrefabs, gen.obstacleProbability, obstaclePos, gen.generatedParent, sample);

            // 2. Eğer engel oluşmadıysa (aynı kordinatta çakışmamaları için) yiyecek koymayı dene
            if (!spawnedObstacle)
            {
                TrySpawn(gen.foodPrefabs, gen.foodProbability, foodPos, gen.generatedParent, sample);
            }

            distance += gen.spacing;
        }
    }

    bool TrySpawn(List<GameObject> prefabs, float prob, Vector3 pos, Transform parent, SplineSample sample)
    {
        if (prefabs == null || prefabs.Count == 0) return false;

        // Olasılık tutmazsa false dön
        if (Random.value > prob) return false;

        GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        go.transform.position = pos;
        // Objenin yönünü ve yukarı eksenini yolun eğimine uydur
        go.transform.rotation = Quaternion.LookRotation(sample.forward, sample.up);
        go.transform.SetParent(parent);

        // Başarıyla oluşturulduysa true dön
        return true;
    }
}