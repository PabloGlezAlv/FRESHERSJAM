using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : Cell
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float travelTime = 1f;

    [SerializeField]
    private float travelTimeModifier = 0.25f;
    private float currTravelTime = 0f;
    private Vector2 direction = Vector2.zero;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currTravelTime -= Time.fixedDeltaTime;
        if(currTravelTime <= 0)
        {
            direction = Random.insideUnitCircle.normalized;
            currTravelTime = travelTime * Random.Range(-travelTimeModifier, travelTimeModifier);
        }

        rb.velocity = direction * speed ;
    }
}
