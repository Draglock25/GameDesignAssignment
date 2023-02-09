using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var collectable = other.GetComponent<ICollectable>();
        if(collectable != null)
        {
            collectable.Collect();
        }
    }
}
