using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEat : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int numberOfPulses;

    private float timer = 0;
    private const float PULSETIMER = 0.2f;
    private float sizeToGrow = 0;
    private bool growing = false;
    private int nPulses = 0;
    private Vector3 scaleChange = Vector3.one;

    private Vector3 scaleSmall = Vector3.one;


    private void Init()
    {
        scaleSmall = transform.localScale;
    }

    private void Update()
    {
        if(growing)
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
                    growing = false;
                    CommonInfo.timePaused = false;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Cell>())
        {
            float scaleFactor;
            growing = true;
            CommonInfo.timePaused = true;

            if (false)
            {
                scaleFactor = -(transform.localScale.x - scaleSmall.x) / numberOfPulses;
                scaleChange = new Vector3(scaleFactor, scaleFactor, 1);

            }
            else
            {
                sizeToGrow = collision.gameObject.GetComponent<Cell>().getSize();
                scaleFactor = sizeToGrow / numberOfPulses;
            }

            scaleChange = new Vector3(scaleFactor, scaleFactor, 1);
            Destroy(collision.gameObject);
        }
    }

}
