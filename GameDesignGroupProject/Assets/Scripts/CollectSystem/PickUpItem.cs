using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PickUpItem : MonoBehaviour
{
    public bool inRange;
    private Inputs controls;

    public GameObject gb;
    public Material material1;
    public Material material2;
    public Renderer rend;
    public UnityEvent Event;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Inputs();
        rend.GetComponent<Renderer>().material = material1;
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        changeMaterial();
        interact();

    }

   
    private void interact()
    {
        if (inRange && controls.Player.Interact.triggered)
        {
            Debug.Log("Item has been picked up!");
            Event.Invoke();
            Destroy(gb);
        }
    }
    
    private void changeMaterial()
    {
        if (inRange)
        {
            rend.GetComponent<Renderer>().material = material2;
        }
        else
        {
            rend.GetComponent<Renderer>().material = material1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
