using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    [SerializeField] float runSpeed = 20.0f;

    [SerializeField] float timeFreeze = 0.5f;

    private bool freeze = false;

    float timer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setFreeze( bool set)
    {
        freeze = true;
        rb.velocity = Vector3.zero;
        Debug.Log(rb.velocity);
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
                Debug.Log(rb.velocity);
                timer += Time.deltaTime;
                rb.velocity = Vector3.zero;
                transform.rotation = transform.rotation;
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
        if (CommonInfo.timePaused || freeze) return;

        // Use the movement
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

}
