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

    Vector2 acceleration = new Vector2(0, 0);   // not true acceleration, mathematically, but I don't want to confuse it with the RB's velocity var
    Vector2 lastMovementDirection = new Vector2(1, 1);  // to be used when calculating "acceleration" after the player releases the WASD keys.

    [SerializeField]
    float MAX_ACCELERATION = 5; // To stop the player's velocity from increasing forever.

    Queue<Vector3> mousePos = new Queue<Vector3>();    // This queue stores multiple frames of mouse positions, to get some input lag.

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setFreeze(bool set)
    {
        freeze = true;
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
        if (horizontal == 0)    // if there is no horizontal input, we slow down velocity over time.
        {
            DriftHorizontal();
        }
        // if the player is holding down W or S
        else
        {
            lastMovementDirection.x = horizontal;
            // This is why "acceleration" isn't true acceleration - I'm adding time to it here, which technically makes it velocity
            acceleration.x += (Time.deltaTime * horizontal);
        }

        // Same as above, but for vertical movement.
        if (vertical == 0)
        {
            DriftVertical();
        }
        // If the player is holding down A or D
        else
        {
            lastMovementDirection.y = vertical;
            acceleration.y += (Time.deltaTime * vertical);
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
            acceleration.x -= Time.deltaTime;   // But, acceleration slows.
                                                // This if keeps acceleration bounded between 0 and its max value.
            if (acceleration.x < 0)
                acceleration.x = 0;
            else if (acceleration.x > MAX_ACCELERATION)
                acceleration.x = MAX_ACCELERATION;
        }
        else if (lastMovementDirection.x < 0)   // Same as above, but for drifting left.
        {
            acceleration.x += Time.deltaTime;

            if (acceleration.x > 0)
                acceleration.x = 0;
            else if (acceleration.y < MAX_ACCELERATION * -1)
                acceleration.y = MAX_ACCELERATION * -1;
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
            acceleration.y -= Time.deltaTime;

            if (acceleration.y < 0)
                acceleration.y = 0;
            else if (acceleration.y > MAX_ACCELERATION)
                acceleration.y = MAX_ACCELERATION;
        }
        else if (lastMovementDirection.y < 0)
        {
            acceleration.y += Time.deltaTime;

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
            // Enqueue the current mouse position.
            mousePos.Enqueue(Input.mousePosition);

            // The mouse position will always be 100 frames behind.
            if (mousePos.Count > 100)
            {
                Vector3 dir = mousePos.Dequeue() - Camera.main.WorldToScreenPoint(transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
