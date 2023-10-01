using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeiN : MonoBehaviour
{
    [SerializeField] float blurSpeed = 0.5f;

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        Color tmp = text.color;
        tmp.a = 0;
        text.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {

        Color tmp = text.color;
        tmp.a += blurSpeed * Time.deltaTime;

        if (tmp.a > 1) tmp.a = 1;
        text.color = tmp;
        
    }
}
