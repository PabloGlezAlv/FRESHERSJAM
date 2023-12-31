using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    private float horizontal;
    private float vertical;

    [SerializeField] float runSpeed = 20.0f;

    [SerializeField] float timeFreeze = 5f;
    [SerializeField] float dieSpeed = 1f;
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] GameObject endBlur;
    [SerializeField] Animator animation;

    private bool freeze = false;

    float timer = 0;
    float rotate = 0;

    Vector2 acceleration = new Vector2(0, 0);   // not true acceleration, mathematically, but I don't want to confuse it with the RB's velocity var
    Vector2 lastMovementDirection = new Vector2(1, 1);  // to be used when calculating "acceleration" after the player releases the WASD keys.

    [SerializeField] float MAX_ACCELERATION = 1; // To stop the player's velocity from increasing forever.
    [SerializeField] float DRAG_COEFFICIENT = 5;    // Increase drag to give the player more control.

    Queue<Vector3> mousePos = new Queue<Vector3>();

    bool dying = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
    }

    public void die()
    {
        dying = true;
    }
    public void setFreeze(bool set)
    {
        freeze = true;
        rb.velocity = Vector3.zero;
        animation.enabled = false;
        Debug.Log("cONGELADO");
    }

    private void Update()
    {

        if (!CommonInfo.timePaused)
        {
            if (!freeze)
            {
                // Get movement
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
                SetPlayerRotation();
            }
            else
            {
                timer += Time.deltaTime;

                if (timer >= timeFreeze)
                {
                    timer = 0;
                    animation.enabled = true;
                    freeze = false;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (dying)
        {
            Color tmp = rbSprite.color;
            tmp.a -= dieSpeed * Time.deltaTime;
            if (tmp.a <= 0)
            {
                endBlur.GetComponent<Blur>().StartBlur();
            }
            rbSprite.color = tmp;
        }
    }

    private void FixedUpdate()
    {
        if (CommonInfo.timePaused || freeze) return;

        // Use the movement
        if (horizontal == 0)    // if there is no horizontal input, we slow down velocity over time.
        {
            DriftHorizontal();
        }
        // if the player is holding down A or D
        else
        {
            if (lastMovementDirection.x == horizontal * -1)
                DriftHorizontal();
            else
            {
                // This is why "acceleration" isn't true acceleration - I'm adding time to it here, which technically makes it velocity
                if ((acceleration.x < 0 && horizontal > 0) || (acceleration.x > 0 && horizontal < 0))
                    acceleration.x += (Time.deltaTime * horizontal * DRAG_COEFFICIENT);
                else
                    acceleration.x += (Time.deltaTime * horizontal);

                if (horizontal > 0 && acceleration.x > MAX_ACCELERATION)
                    acceleration.x = MAX_ACCELERATION;
                else if (horizontal < 0 && acceleration.x < (MAX_ACCELERATION * -1))
                    acceleration.x = MAX_ACCELERATION * -1;
            }

            lastMovementDirection.x = horizontal;
        }

        // Same as above, but for vertical movement.
        if (vertical == 0)
        {
            DriftVertical();
        }
        // If the player is holding down W or S
        else
        {
            if ((acceleration.y < 0 && vertical > 0) || (acceleration.y > 0 && vertical < 0))
                acceleration.y += (Time.deltaTime * vertical * DRAG_COEFFICIENT);
            else
                acceleration.y += (Time.deltaTime * vertical);

            if (vertical > 0 && acceleration.y > MAX_ACCELERATION)
                acceleration.y = MAX_ACCELERATION;
            else if (vertical < 0 && acceleration.y < (MAX_ACCELERATION * -1))
                acceleration.y = MAX_ACCELERATION * -1;

            lastMovementDirection.y = vertical;
        }

        // Here is where we actually set the rigidbody's velocity
        rb.velocity = new Vector2(runSpeed * acceleration.x, runSpeed * acceleration.y);
    }

    /*
     * When a collision lowers our velocity significantly, we set acceleration to this low number to mimic the physics of the collision.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.x >= -1 && rb.velocity.x <= 1)
            acceleration.x = rb.velocity.x;

        if (rb.velocity.y >= -1 && rb.velocity.y <= 1)
            acceleration.y = rb.velocity.y;
    }

    /*
     * To be called when the player releases W or S.
     * Simulates momentum, causing the player to come to a stop slowly, over time.
     */
    private void DriftHorizontal()
    {
        if (lastMovementDirection.x > 0)    // If they were moving right, they continue to drift right.
        {
            acceleration.x -= Time.deltaTime * DRAG_COEFFICIENT;   // But, acceleration slows.
            // This if keeps acceleration bounded between 0 and its max value.
            if (acceleration.x < 0)
                acceleration.x = 0;
            else if (acceleration.x > MAX_ACCELERATION)
                acceleration.x = MAX_ACCELERATION;
        }
        else if (lastMovementDirection.x < 0)   // Same as above, but for drifting left.
        {
            acceleration.x += Time.deltaTime * DRAG_COEFFICIENT;

            if (acceleration.x > 0)
                acceleration.x = 0;
            else if (acceleration.x < MAX_ACCELERATION * -1)
                acceleration.x = MAX_ACCELERATION * -1;
        }
    }

    /*
    * To be called when the player releases A or D.
    * Simulates momentum, causing the player to come to a stop slowly, over time.
    */
    private void DriftVertical()
    {
        if (lastMovementDirection.y > 0)
        {
            acceleration.y -= Time.deltaTime * DRAG_COEFFICIENT;

            if (acceleration.y < 0)
                acceleration.y = 0;
            else if (acceleration.y > MAX_ACCELERATION)
                acceleration.y = MAX_ACCELERATION;
        }
        else if (lastMovementDirection.y < 0)
        {
            acceleration.y += Time.deltaTime * DRAG_COEFFICIENT;

            if (acceleration.y > 0)
                acceleration.y = 0;
            else if (acceleration.y < MAX_ACCELERATION * -1)
                acceleration.y = MAX_ACCELERATION * -1;
        }
    }

    /*
     * Rotates the player - but with some input lag.
     */
    private void SetPlayerRotation()
    {
        // Body rotation - only happens if the game is not paused (i.e., timeScale != 0)
        if (Time.timeScale != 0)
        {
            /*mousePos.Enqueue(Input.mousePosition);

            if (mousePos.Count > 10)
            {
                Vector3 dir = mousePos.Dequeue() - Camera.main.WorldToScreenPoint(transform.position);
                float mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
            }*/

            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float mouseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);

        }
    }
}
