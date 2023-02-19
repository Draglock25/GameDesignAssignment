using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTunnel : MonoBehaviour
{
    [SerializeField] private float roatate = 0.0f;
    [SerializeField] private GameObject tunnel;

    [Header("Rotation Settings")]
    public Transform startPos;
    public int endPos;
    [SerializeField] private float duratation = 5f;
    [SerializeField] private float elaspedTime;

    //[SerializeField] GameObject start;

    private void Start()
    {
       
        
    }
    private void Update()
    {
        elaspedTime = Time.deltaTime;
        float percentDone = elaspedTime * duratation;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(endPos, 0, 0), percentDone);
    }

    public void rTunnel(int f)
    {

        endPos = f;

    }


    public void rTest()
    {
        elaspedTime = Time.deltaTime;
        float percentDone = elaspedTime / duratation;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(75, 0, 0), percentDone);   
    }
    
}
