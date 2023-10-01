using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blur : MonoBehaviour
{
    [SerializeField] float blurSpeed = 0.5f;

    private SpriteRenderer rbSprite;

    bool startBlur = false;

    // Start is called before the first frame update
    void Start()
    {
        rbSprite = GetComponent<SpriteRenderer>();
    }

    public void StartBlur()
    {
        startBlur = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startBlur)
        {
            Color tmp = rbSprite.color;
            tmp.a += blurSpeed * Time.deltaTime;

            if (tmp.a >= 1) SceneManager.LoadScene("EndGame Menu");
            rbSprite.color = tmp;
        }
    }
}
