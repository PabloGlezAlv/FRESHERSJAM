using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOutButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float blurSpeed = 1f;

    private Image text;
    // Start is called before the first frame update
    void Start()
    {
        CommonInfo.timePaused = true;

        text = GetComponent<Image>();

        Color tmp = text.color;
        text.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = text.color;
        tmp.a -= blurSpeed * Time.deltaTime;

        if (tmp.a <= 0)
        {
            Destroy(this);
            CommonInfo.timePaused = false;
        }
        text.color = tmp;

    }
}
