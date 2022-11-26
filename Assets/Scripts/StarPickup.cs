using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
    //Stats stats;
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
        Debug.Log("entered collider");
        if(other.gameObject.tag == "Player")
        {
            Stats.instance.stars++;
            FindObjectOfType<AudioManager>().Play("Star pickup");
            Destroy(gameObject);
            Debug.Log("Stars: " + Stats.instance.stars);
        }
    }
}
