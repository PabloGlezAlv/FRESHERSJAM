#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    // Designer set
    public string startMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Unpause the game
    public void OnResume()
    {
        Time.timeScale = 1; // Resume the game
        Destroy(this.gameObject);   // Take the menu off screen
    }

    // Return to the Start Menu
    public void OnQuitToMenu()
    {
        Time.timeScale = 1; // Resume game time before quitting to the menu.
        SceneManager.LoadScene(startMenuScene);
    }

    // Close the game. If playing in the editor, end play mode.
    public void OnQuitToDesktop()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
