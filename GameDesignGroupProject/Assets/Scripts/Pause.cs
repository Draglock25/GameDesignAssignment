using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
    
}
