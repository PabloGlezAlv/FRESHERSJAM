using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerEat : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int numberOfPulses;

    private float timer = 0;
    [SerializeField] float PULSETIMER = 0.2f;
    private float sizeToGrow = 0;
    private bool growing = false;
    private int nPulses = 0;
    private Vector3 scaleChange = Vector3.one;

    private Vector3 scaleSmall = Vector3.one;

    [SerializeField] AudioSource eatSound;
    [SerializeField] AudioSource damageSound;
    private bool reset = false;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scaleSmall = new Vector3(player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
        player.transform.localScale = new Vector3(player.transform.localScale.x + 0.5f, player.transform.localScale.y + 0.5f, player.transform.localScale.z);
    }

    private void Update()
    {
        if (reset)
        {
            timer += Time.deltaTime;
            if (timer >= PULSETIMER)
            {
                //Make the player bigger
                player.transform.localScale += scaleChange;

                nPulses++;
                timer = 0;
                if (nPulses >= numberOfPulses)
                {
                    nPulses = 0;
                    reset = false;
                    CommonInfo.timePaused = false;
                }
            }
        }
        else if(growing)
        {
            timer += Time.deltaTime;
            if (timer >= PULSETIMER)
            {
                //Make the player bigger
                player.transform.localScale += scaleChange;

                //CHECK DEAD
                if(player.transform.localScale.x < (scaleSmall.x - 0.0f))
                {
                    Debug.Log("Local scale x = " + player.transform.localScale.x);
                    SceneManager.LoadScene("Dead Menu");
                }

                nPulses++;
                timer = 0;
                if (nPulses >= numberOfPulses)
                {
                    nPulses = 0;
                    growing = false;
                    CommonInfo.timePaused = false;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(reset) return;

        if (collision.gameObject.GetComponent<Cell>() && collision.gameObject.GetComponent<Cell>().getSizeToEat() <= player.transform.localScale.x )
        {
            float scaleFactor;
            growing = true;
            //CommonInfo.timePaused = true;

            
            if (collision.gameObject.GetComponent<Boss>())
            {
                scaleFactor = -(player.transform.localScale.x - 0.5f - scaleSmall.x) / (float) numberOfPulses;
                reset = true;
                growing = false;
                timer = 0;

                nPulses = 0;
                CommonInfo.timePaused = true;
            }
            else
            {
                sizeToGrow = collision.gameObject.GetComponent<Cell>().getSize();
                scaleFactor = sizeToGrow / numberOfPulses;
            }

            //Debug.Log(scaleFactor);

            scaleChange = new Vector3(scaleFactor, scaleFactor, 0);
            Destroy(collision.gameObject);

            eatSound.Play();
        }
    }

    public void reduceSize(float size)
    {
        if (reset) return;
        damageSound.Play();
        //if (growing) return;
        growing = true;
        //CommonInfo.timePaused = true;
        scaleChange = new Vector3(-size / numberOfPulses, -size / numberOfPulses, 0);
    }

}
