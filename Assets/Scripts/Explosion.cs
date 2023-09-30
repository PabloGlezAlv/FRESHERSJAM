using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float time = 0;
    [SerializeField] float explosionTime = 5f;

    [SerializeField] GameObject explosion;

    float timer = 0;
    float MAX_TIME = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > MAX_TIME)
        {
            timer = 0;

            CommonInfo.cameraMoving = true;

            Invoke("CreateExplosion", CommonInfo.TimeMoving);
        }
    }

    public void CreateExplosion()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("eXPLOSITON TOCA");
        if(other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused)
        {
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
        }
    }
}
