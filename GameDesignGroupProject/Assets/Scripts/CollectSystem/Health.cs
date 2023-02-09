using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("You gain health");
        Destroy(gameObject);
    }
}
