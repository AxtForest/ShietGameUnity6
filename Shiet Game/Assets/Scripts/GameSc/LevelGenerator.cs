using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public SplineComputer spline;

    [Header("General Settings")]
    public float spacing = 3f;
    public bool clearBeforeGenerate = true;

    [Header("Food Settings")]
    public List<GameObject> foodPrefabs;
    [Range(0, 1)] public float foodProbability = 0.8f;

    [Header("Obstacle Settings")]
    public List<GameObject> obstaclePrefabs;
    [Range(0, 1)] public float obstacleProbability = 0.2f;

    [Header("Height Settings")]
    public float foodHeightOffset = 0f; 
    public float obstacleHeightOffset = 1f;

    [Header("Spawn Settings")]
    // Player'ın ne kadar ilerisinden spawn olmaya başlayacak (1. Sorunun çözümü)
    public float startOffset = 15f;

    public float endOffset = 20f;


    // Objelerin sağa ve sola ne kadar dağılabileceği (2. Sorunun çözümü)
    public float spawnWidth = 3f;

    [HideInInspector] public Transform generatedParent;
}
