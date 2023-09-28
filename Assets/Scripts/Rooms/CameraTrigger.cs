using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraPosition;
    [SerializeField]
    private float cameraSize;
    [SerializeField]
    private float transitionTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("LMAO!");
        //Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveCamera());
        }

        
    }

    private IEnumerator MoveCamera()
    {
        currTransitionTime = 0f;
        Vector3 lastCameraPosition = maincamera.transform.position;
        float lastCameraSize = maincamera.orthographicSize;
        while (currTransitionTime <= transitionTime)
        {
            currTransitionTime += Time.deltaTime;
            maincamera.transform.position = Vector3.Lerp(lastCameraPosition, cameraPosition.transform.position, currTransitionTime / transitionTime);
            maincamera.orthographicSize = Mathf.Lerp(lastCameraSize, cameraSize, currTransitionTime / transitionTime);
            yield return null;
        }
    }

}
