using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerScale player = other.GetComponent<PlayerScale>();
        if (player != null)
        {
            player.SubScale(1);
            //Destroy(gameObject); destroy degil partikul effect cikcak 
        }
    }
}
