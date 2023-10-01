#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [SerializeField] AudioSource audio;

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
        SceneManager.LoadScene("SamDevScene");

        audio.Play();
    }


    // Quits to desktop. If testing in the editor, ends play mode.
    public void OnGameQuit()
    {
        audio.Play();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
