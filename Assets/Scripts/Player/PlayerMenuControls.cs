using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuControls : MonoBehaviour
{
    GameObject pauseMenuInstance;
    
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when the player presses the Pause Button
    public void OnPause()
    {
        if(!pauseMenuInstance)
        {
            Time.timeScale = 0; // The cheap, hacky way to pause.
            pauseMenuInstance = Instantiate(pauseMenu); // Places the Pause Menu on the screen.
        }
        else
        {
            Time.timeScale = 1; // Unpause
            Destroy(pauseMenuInstance);
        }
    }
}
