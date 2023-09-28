using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    [SerializeField] float runSpeed = 20.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CommonInfo.timePaused)
        {
            // Get movement
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            // Body rotation
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

}
