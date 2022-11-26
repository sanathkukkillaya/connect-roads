using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int moves;
    public int stars;
    public static Stats instance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Stats");
        if (instance == null)
        {
            instance = this;
        }
        moves = 0;
        stars = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
