using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class ChangeColor : MonoBehaviour
{
    
    [SerializeField]
    private Color color;
    [SerializeField]
    private float transitionTime;

    private SpriteRenderer roomSprite;
    private float currTransitionTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        roomSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorStart()
    {
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        currTransitionTime = 0f;
        Color lastColor = roomSprite.color;
        while (currTransitionTime <= transitionTime)
        {
            currTransitionTime += Time.deltaTime;
            roomSprite.color = Color.Lerp(lastColor, color, currTransitionTime / transitionTime);
            yield return null;
        }
    }
}
