using Unity.Cinemachine;
using UnityEngine;


public class PlayerConvert : MonoBehaviour
{
    [Header("Stages")]
    [SerializeField] private GameObject[] players;
    [SerializeField] private int foodPerStage = 2;

    private int foodCount;
    private int currentStage;
    private int previousStage = -1;



    

    private void Start()
    {
        UpdateStage();
        
    }

    public void AddFood(int amount)
    {
        foodCount += amount;
        TryChangeStage(+1);
    }

    public void SubFood(int amount)
    {
        foodCount -= amount;
        TryChangeStage(-1);
    }

    private void TryChangeStage(int direction)
    {
        int targetStage = foodCount / foodPerStage;
        targetStage = Mathf.Clamp(targetStage, 0, players.Length - 1);

        if (targetStage == currentStage) return;

        currentStage = targetStage;
        UpdateStage();
    }

    private void UpdateStage()
    {
        float savedX = 0f;

        if (previousStage >= 0)
            savedX = players[previousStage].transform.localPosition.x;

        for (int i = 0; i < players.Length; i++)
            players[i].SetActive(i == currentStage);

        Transform activePlayer = players[currentStage].transform;
        Vector3 pos = activePlayer.localPosition;
        pos.x = savedX;
        activePlayer.localPosition = pos;


        previousStage = currentStage;
    }
}
