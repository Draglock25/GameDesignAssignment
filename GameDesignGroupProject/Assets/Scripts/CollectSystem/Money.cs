using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("You gain Money");
        Destroy(gameObject);
    }

}
