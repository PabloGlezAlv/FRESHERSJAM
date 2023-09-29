using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Boss : Cell
{

    [SerializeField]
    private RoomTrigger trigger;

    [SerializeField]
    private ChangeColor changeColor;


    Vector2 direction;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        trigger.enabled = false;
        player.GetComponent<Collider2D>().enabled = false;
        changeColor.ChangeColorStart();
    }

    void FixedUpdate()
    {
        direction = 
            new Vector2(- transform.position.x + player.transform.position.x, - transform.position.y + player.transform.position.y).normalized;

        rb.velocity = direction * speed;
    }

   
}
