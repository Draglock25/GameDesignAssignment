using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputText : MonoBehaviour
{
    private Inputs mInput;

    private void Awake()
    {
        mInput = new Inputs();
    }
    void Start()
    {

        
    }

    private void Update()
    {
        if (mInput.Player.Jump.triggered)
        {
            Debug.Log("item1");
        }
    }
}
