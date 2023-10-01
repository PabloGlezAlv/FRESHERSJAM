using Microsoft.Unity.VisualStudio.Editor;
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
    float MAX_TIME = 1;
    bool explode = false;
    bool startExplosion = false;
    bool alreadyStopped = false;

    float speed = 1;

    float belowMARGIN = 0.5f;
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
            if (timer > MAX_TIME)
            {
                timer = 0;
                CommonInfo.cameraMoving = true;
                startExplosion = true;
                Debug.Log("AVISO DE EXPLOTAR");
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
                
                belowMARGIN += (float)(0.5 * countDown);
                countDown = countDown / 4;
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
            Debug.Log("FIN EXPLOSION");
        }
    }

    public void CreateExplosion()
    {
        Debug.Log("ESTOY EXPLOTANDO");
        startExplosion = false;
        timer = 0;
        explode = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused && explode)
        {
            Debug.Log("CHOCADO");
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
            alreadyStopped = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMov>() && !CommonInfo.timePaused && explode && !alreadyStopped)
        {
            Debug.Log("CHOCADO");
            other.gameObject.GetComponent<PlayerMov>().setFreeze(true);
            alreadyStopped = true;
        }
    }

}
