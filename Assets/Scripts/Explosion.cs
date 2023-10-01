using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Properties;
using UnityEngine;
using static System.TimeZoneInfo;

public class Explosion : MonoBehaviour
{
    private float time = 0;
    [SerializeField] float explosionTime = 12f;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] float startSpeed = 1f;
    [SerializeField] GameObject redVein;

    SpriteRenderer redVeinImage;

    float timer = 0;
    bool explode = false;
    bool startExplosion = false;
    bool alreadyStopped = false;

    float speed = 3.5f;

    float belowMARGIN = 0f;
    int countDown = 1;
    // Start is called before the first frame update
    void Start()
    {
        redVeinImage = redVein.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!explode && !startExplosion)
        {
            if (timer > explosionTime)
            {
                timer = 0;
                CommonInfo.cameraMoving = true;
                startExplosion = true;
                Invoke("CreateExplosion", CommonInfo.TimeMoving);
            }
        }
        if(startExplosion)
        {
            UnityEngine.Color tmp = redVeinImage.color;
            tmp.a += speed * Time.deltaTime;

            if (tmp.a <= belowMARGIN && speed < 0)
            {
                speed = -speed;
                
            }
            if(tmp.a >= 1)
            {
                speed = -speed;
            }

            redVeinImage.color = tmp;
        }
        if (timer > explosionDuration && explode && !startExplosion)
        {
            explode = false;
            alreadyStopped = false;
            timer = 0;

            UnityEngine.Color tmp = redVeinImage.color;
            tmp.a = 0;
            redVeinImage.color = tmp;
        }
    }

    public void CreateExplosion()
    {
        startExplosion = false;
        timer = 0;
        explode = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused && explode)
        {
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
            alreadyStopped = true;
            timer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused && explode && !alreadyStopped)
        {
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
            alreadyStopped = true;
            timer = 0;
        }
    }

}
