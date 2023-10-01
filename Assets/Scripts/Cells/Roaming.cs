using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : Cell
{
   

    [SerializeField]
    private float travelTime = 1f;

    [SerializeField]
    private float travelTimeModifier = 0.25f;
    private float currTravelTime = 0f;
    private Vector2 direction = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (knockback && currKnockbackTime <= knockbackTime)
        {
            currKnockbackTime += Time.fixedDeltaTime;
            return;
        }

        currKnockbackTime = 0;
        knockback = false;
        if (CommonInfo.timePaused)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = transform.rotation;
            return;
        }

        


        currTravelTime -= Time.fixedDeltaTime;
        if(currTravelTime <= 0)
        {
            direction = Random.insideUnitCircle.normalized;
            currTravelTime = travelTime * Random.Range(-travelTimeModifier, travelTimeModifier);
        }

        rb.velocity = direction * speed ;
    }
}
