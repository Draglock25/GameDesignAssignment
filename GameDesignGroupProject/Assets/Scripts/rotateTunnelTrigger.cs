using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTunnelTrigger : MonoBehaviour
{
    [SerializeField] rotateTunnel rt;
    [SerializeField] int rotate = 75;
    


    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rotateTunnel();
            
        }
    }

    private void rotateTunnel()
    {
        rt.rTunnel(rotate);
    }

    

}
