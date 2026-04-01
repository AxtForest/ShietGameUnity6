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

        GUILayout.Space(15);

        // --- YENİ EKLENEN KISIM: BİLGİ VE UYARI EKRANI ---
        if (gen.spline != null && gen.spacing > 0)
        {
            float length = gen.spline.CalculateLength();
            float availableLength = (length - gen.endOffset) - gen.startOffset;

            if (availableLength > 0)
            {
                // Mevcut kodundaki matematiğe göre toplam slot sayısı
                int maxSlots = Mathf.FloorToInt(availableLength / gen.spacing) + 1;
                int requestedItems = gen.foodCount + gen.obstacleCount;

                // Bilgi Kutusu
                EditorGUILayout.HelpBox($"Yol Uzunluğu: {length:F1} birim\nKullanılabilir Alan: {availableLength:F1} birim\nMaksimum Obje Kapasitesi (Slot): {maxSlots}", MessageType.Info);

                // Eğer istenen obje sayısı kapasiteyi aşıyorsa Uyarı Kutusu çıkar
                if (requestedItems > maxSlots)
                {
                    EditorGUILayout.HelpBox($"DİKKAT: İstenen obje sayısı ({requestedItems}), yoldaki maksimum kapasiteyi ({maxSlots}) aşıyor! {requestedItems - maxSlots} adet obje oluşturulamayacak.", MessageType.Warning);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Kullanılabilir alan kalmadı! 'Start Offset' ve 'End Offset' değerlerini küçült veya yolu uzat.", MessageType.Error);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Bir Spline ata ve Spacing değerinin 0'dan büyük olduğundan emin ol.", MessageType.Warning);
        }
        // --------------------------------------------------

        GUILayout.Space(15);

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
        //Find'dan kurtul to do
       

        Transform oldFoods = gen.transform.Find("Foods");
        if (oldFoods != null) Undo.DestroyObjectImmediate(oldFoods.gameObject);

        Transform oldObstacles = gen.transform.Find("Obstacles");
        if (oldObstacles != null) Undo.DestroyObjectImmediate(oldObstacles.gameObject);
    }

    void Generate(LevelGenerator gen)
    {
        if (gen.spline == null)
        {
            Debug.LogError("Spline yok! kardeşşşş");
            return;
        }

        Clear(gen);

        float length = gen.spline.CalculateLength();
        float startLimit = gen.startOffset;
        float endLimit = length - gen.endOffset;
        float availableLength = endLimit - startLimit;

        if (availableLength <= 0) return; 

        int totalSlots = Mathf.FloorToInt(availableLength / gen.spacing);

        List<int> availableSlots = new List<int>();
        for (int i = 0; i <= totalSlots; i++)
        {
            availableSlots.Add(i);
        }

        Transform foodsParent = new GameObject("Foods").transform;
        foodsParent.SetParent(gen.transform);
        foodsParent.localPosition = Vector3.zero;
        foodsParent.localRotation = Quaternion.identity;
        Undo.RegisterCreatedObjectUndo(foodsParent.gameObject, "Generate Foods");

        Transform obstaclesParent = new GameObject("Obstacles").transform;
        obstaclesParent.SetParent(gen.transform);
        obstaclesParent.localPosition = Vector3.zero;
        obstaclesParent.localRotation = Quaternion.identity;
        Undo.RegisterCreatedObjectUndo(obstaclesParent.gameObject, "Generate Obstacles");

        List<int> obstacleSlots = PickRandomSlots(availableSlots, gen.obstacleCount);
        SpawnItems(obstacleSlots, gen.obstaclePrefabs, obstaclesParent, gen, length, gen.obstacleHeightOffset);

        List<int> foodSlots = PickRandomSlots(availableSlots, gen.foodCount);
        SpawnItems(foodSlots, gen.foodPrefabs, foodsParent, gen, length, gen.foodHeightOffset);
    }

    List<int> PickRandomSlots(List<int> availableSlots, int count)
    {
        List<int> pickedSlots = new List<int>();
        for (int i = 0; i < count && availableSlots.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableSlots.Count);
            pickedSlots.Add(availableSlots[randomIndex]);
            availableSlots.RemoveAt(randomIndex);
        }
        return pickedSlots;
    }

    void SpawnItems(List<int> slots, List<GameObject> prefabs, Transform parent, LevelGenerator gen, float totalLength, float heightOffset)
    {
        if (prefabs == null || prefabs.Count == 0) return;

        foreach (int slot in slots)
        {
            float distance = gen.startOffset + (slot * gen.spacing);

            SplineSample sample = gen.spline.Evaluate(distance / totalLength);

            float randomHorizontalOffset = Random.Range(-gen.spawnWidth, gen.spawnWidth);
            Vector3 basePos = sample.position + (sample.right * randomHorizontalOffset);
            Vector3 finalPos = basePos + (sample.up * heightOffset);

            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            go.transform.position = finalPos;
            go.transform.rotation = Quaternion.LookRotation(sample.forward, sample.up);
            go.transform.SetParent(parent);

            Undo.RegisterCreatedObjectUndo(go, "Spawn Item");
        }
    }
}