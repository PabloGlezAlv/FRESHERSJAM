using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraMovement : MonoBehaviour
{
    
    private float currTransitionTime = 0f;
    private Camera maincamera;

    // Start is called before the first frame update
    void Start()
    {
        maincamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCameraMovement(float newSize, Vector3 newPosition, float transitionTime)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCamera( newSize,  newPosition,  transitionTime));
    }

    private IEnumerator MoveCamera(float newSize, Vector3 newPosition, float transitionTime)
    {
        currTransitionTime = 0f;
        Vector3 lastCameraPosition = maincamera.transform.position;
        float lastCameraSize = maincamera.orthographicSize;
        while (currTransitionTime <= transitionTime)
        {
            currTransitionTime += Time.deltaTime;
            maincamera.transform.position = Vector3.Lerp(lastCameraPosition, newPosition, currTransitionTime / transitionTime);
            maincamera.orthographicSize = Mathf.Lerp(lastCameraSize, newSize, currTransitionTime / transitionTime);
            yield return null;
        }
    }
}
