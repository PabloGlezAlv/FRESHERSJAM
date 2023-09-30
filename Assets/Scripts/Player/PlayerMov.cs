using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    [SerializeField] float runSpeed = 20.0f;

    [SerializeField] float timeFreeze = 1.0f;

    private bool freeze = false;

    float timer = 0;

    Vector2 acceleration = new Vector2(0, 0);
    Vector2 lastMovementDirection = new Vector2(1, 1);

    [SerializeField]
    float MAX_ACCELERATION = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setFreeze( bool set)
    {
        freeze = true;
    }

    private void Update()
    {
        if (!CommonInfo.timePaused)
        {
            if(!freeze)
            {           
                // Get movement
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                // Body rotation - only happens if the game is not paused (i.e., timeScale != 0)
                if (Time.timeScale != 0)
                {
                    Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
            {
                timer += Time.deltaTime;
                if(timer >= timeFreeze)
                {
                    timer = 0;
                    freeze = false;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.rotation = transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (CommonInfo.timePaused) return;

        // Use the movement
        if (horizontal == 0)
        {
            if (lastMovementDirection.x > 0)
            {
                acceleration.x -= Time.deltaTime;
                if (acceleration.x < 0)
                    acceleration.x = 0;
                else if (acceleration.x > MAX_ACCELERATION)
                    acceleration.x = MAX_ACCELERATION;
            }
            else if (lastMovementDirection.x < 0)
            {
                acceleration.x += Time.deltaTime;
                if (acceleration.x > 0)
                    acceleration.x = 0;
                else if (acceleration.y < MAX_ACCELERATION * -1)
                    acceleration.y = MAX_ACCELERATION * -1;
            }
        }
        else
        {
            lastMovementDirection.x = horizontal;
            acceleration.x += (Time.deltaTime * horizontal);
        }

        if (vertical == 0)
        {
            if (lastMovementDirection.y > 0)
            {
                acceleration.y -= Time.deltaTime;
                if (acceleration.y < 0)
                    acceleration.y = 0;
            }
            else if (lastMovementDirection.y < 0)
            {
                acceleration.y += Time.deltaTime;
                if (acceleration.y > 0)
                    acceleration.y = 0;
            }

        }
        else
        {
            lastMovementDirection.y = vertical;
            acceleration.y += (Time.deltaTime * vertical);
        }

        rb.velocity = new Vector2(runSpeed * acceleration.x, runSpeed * acceleration.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.x >= -1 && rb.velocity.x <= 1)
            acceleration.x = rb.velocity.x;

        if (rb.velocity.y >= -1 && rb.velocity.y <= 1)
            acceleration.y = rb.velocity.y;
    }
}
