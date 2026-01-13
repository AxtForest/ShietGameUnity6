using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    public int scale = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddScale(int scaleVal)
    {
        scale += scaleVal;
        Debug.Log("scale 1 eklendi");
    }
    public void SubScale(int scaleVal)
    {
        scale -= scaleVal;
        Debug.Log("scale 1 azaldı");
    }
}
