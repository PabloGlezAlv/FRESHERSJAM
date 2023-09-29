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
            maincamera.gameObject.GetComponent<CameraMovement>().StartCameraMovement(cameraSize, cameraPosition.transform.position, transitionTime);
        }

        
    }

    

}
