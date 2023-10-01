using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
