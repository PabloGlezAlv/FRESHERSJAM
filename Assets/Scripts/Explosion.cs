using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float time = 0;
    [SerializeField] float explosionTime = 5f;
    [SerializeField] float explosionDuration = 0.4f;

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

        if (timer > MAX_TIME && !explode)
        {
            timer = 0;
            CommonInfo.cameraMoving = true;

            Invoke("CreateExplosion", CommonInfo.TimeMoving);
        }
        else if (timer > explosionDuration && !explode)
        {
            explode = false;
            timer = 0;
        }
    }

    public void CreateExplosion()
    {
        explode = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused && explode)
        {
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
        }
    }
}
