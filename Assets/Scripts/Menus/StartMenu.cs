using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public string firstScene = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads the game's first scene
    public void OnGameStart()
    {
        SceneManager.LoadScene(firstScene);
    }

    // Quits to desktop. If testing in the editor, ends play mode.
    public void OnGameQuit()
    {
        if (Application.isEditor)
        {
            EditorApplication.ExitPlaymode();
        }
        else
            Application.Quit();
    }
}
