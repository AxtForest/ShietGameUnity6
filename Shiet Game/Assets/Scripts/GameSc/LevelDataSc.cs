using UnityEngine;
using Dreamteck.Splines;
public class LevelDataSc : MonoBehaviour
{
    public Transform levelEnd;
    public SplineComputer levelSpline;
    public int maxFood; 



    void Awake()
    {
        // Level prefabındaki tüm Food objelerini bul
        maxFood = transform.GetComponentsInChildren<Food>().Length;

        Debug.Log("maxfooodoododo" + maxFood);
    }

}
