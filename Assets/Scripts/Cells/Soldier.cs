using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Cell
{

    Vector2 direction;
    RunAway[] boidsInScene;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        boidsInScene = FindObjectsOfType<RunAway>();
    }

    void FixedUpdate()
    {
        if (CommonInfo.timePaused)
        {
            rb.velocity = Vector3.zero;
            transform.rotation = transform.rotation;
            return;
        }

        List<RunAway> delete = new List<RunAway>();
        foreach (RunAway boid in boidsInScene)
        {
            if (boid == null) {
                delete.Add(boid);
            }
        }


        direction = new Vector2(- transform.position.x + player.transform.position.x, - transform.position.y + player.transform.position.y).normalized;
        AlignWithOthers();
        MoveToCenter();
        AvoidOtherBoids();
        //transform.Translate(direction * (speed * Time.deltaTime));

        // Move senteces
        rb.velocity = direction * speed;
        //rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal") * speed, 0.8f),
        //                                     Mathf.Lerp(0, Input.GetAxisRaw("Vertical") * speed, 0.8f));
    }

    [SerializeField]
    float moveToCenterStrength;//factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
    [SerializeField] float localBoidsDistance;//effective distance to calculate the center
    void MoveToCenter()
    {

        Vector2 positionSum = transform.position;//calculate sum of position of nearby boids and get count of boid
        int count = 0;

        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, localBoidsDistance) )
        {
            if(coll.GetComponent<Soldier>() != null) {
                positionSum += (Vector2)coll.transform.position;
                count++;
            }
        }

        //foreach (RunAway boid in boidsInScene)
        //{
        //    float distance = Vector2.Distance(boid.transform.position, transform.position);
        //    if (distance <= localBoidsDistance)
        //    {
        //        positionSum += (Vector2)boid.transform.position;
        //        count++;
        //    }
        //}

        if (count == 0)
        {
            return;
        }

        //get average position of boids
        Vector2 positionAverage = positionSum / count;
        positionAverage = positionAverage.normalized;
        Vector2 faceDirection = (positionAverage - (Vector2)transform.position).normalized;

        //move boid toward center
        float deltaTimeStrength = moveToCenterStrength * Time.fixedDeltaTime;
        direction = direction + deltaTimeStrength * faceDirection / (deltaTimeStrength + 1);
        direction = direction.normalized;
    }

    [SerializeField] float avoidOtherStrength;//factor by which boid will try to avoid each other. Higher it is, higher the turn rate to avoid other.
    [SerializeField] float collisionAvoidCheckDistance;//distance of nearby boids to avoid collision
    void AvoidOtherBoids()
    {

        Vector2 faceAwayDirection = Vector2.zero;//this is a vector that will hold direction away from near boid so we can steer to it to avoid the collision.

        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, collisionAvoidCheckDistance))
        {
            if (coll.GetComponent<Cell>() != null)
            {
                faceAwayDirection = faceAwayDirection + (Vector2)(transform.position - coll.transform.position);
            }
        }

        //we need to iterate through all boid
        //foreach (RunAway boid in boidsInScene)
        //{
        //    float distance = Vector2.Distance(boid.transform.position, transform.position);

        //    //if the distance is within range calculate away vector from it and subtract from away direction.
        //    if (distance <= collisionAvoidCheckDistance)
        //    {
        //        faceAwayDirection = faceAwayDirection + (Vector2)(transform.position - boid.transform.position);
        //    }
        //}

        faceAwayDirection = faceAwayDirection.normalized;//we need to normalize it so we are only getting direction

        direction = direction + avoidOtherStrength * faceAwayDirection / (avoidOtherStrength + 1);
        direction = direction.normalized;
    }

    [SerializeField] float alignWithOthersStrength;//factor determining turn rate to align with other boids
    [SerializeField] float alignmentCheckDistance;//distance up to which alignment of boids will be checked. Boids with greater distance than this will be ignored

    void AlignWithOthers()
    {
        //we will need to find average direction of all nearby boids
        Vector2 directionSum = Vector3.zero;
        int count = 0;

        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, alignmentCheckDistance))
        {
            if (coll.GetComponent<Soldier>() != null)
            {
                directionSum += coll.GetComponent<Soldier>().direction;
                count++;
            }
        }

        //foreach (RunAway boid in boidsInScene)
        //{
        //    float distance = Vector2.Distance(boid.transform.position, transform.position);
        //    if (distance <= localBoidsDistance)
        //    {
        //        directionSum += boid.direction;
        //        count++;
        //    }
        //}

        Vector2 directionAverage = directionSum / count;
        directionAverage = directionAverage.normalized;

        //now add this direction to direction vector to steer towards it
        float deltaTimeStrength = alignWithOthersStrength * Time.fixedDeltaTime;
        direction = direction + deltaTimeStrength * directionAverage / (deltaTimeStrength + 1);
        direction = direction.normalized;

    }
}
