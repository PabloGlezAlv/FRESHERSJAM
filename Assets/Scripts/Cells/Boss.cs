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
    [SerializeField]
    private Transform[] spawnPositions;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private GameObject spawnEntity;
    [SerializeField]
    private int maxEnemies;

    private float currentSpawnTimer = 0f;
    private int currentSpawnPoint = 0;


    Vector2 direction;
    GameObject player;
    List<GameObject> enemies;

    void Start()
    {
        enemies = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //Was Deleted
        {
            Debug.Log(gameObject.transform.parent.gameObject.name);
            trigger.enabled = false;
            if (player != null) player.GetComponent<Collider2D>().enabled = false;
            changeColor.ChangeColorStart();
            foreach (GameObject soldier in enemies)
            {
                if (soldier != null)
                {
                    Destroy(soldier);
                }
            }
        }
        
    }



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

        

        direction = 
            new Vector2(- transform.position.x + player.transform.position.x, - transform.position.y + player.transform.position.y).normalized;

        rb.velocity = direction * speed;

        List<GameObject> destroy = new List<GameObject>();
        foreach (GameObject soldier in enemies)
        {
            if (soldier == null)
            {
                destroy.Add(soldier);
            }
        }
        destroy.ForEach(d => enemies.Remove(d));
        Spawn();
    }

    void Spawn()
    {
        currentSpawnTimer += Time.fixedDeltaTime;

        if (currentSpawnTimer < spawnTimer) return;
        currentSpawnTimer = 0f;
        if (enemies.Count >= maxEnemies) return;
        Vector3 position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;

        if ((player.transform.position - position).magnitude < player.transform.localScale.x + player.transform.localScale.x * 0.5f) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (position - transform.position).normalized, (position - transform.position).magnitude);
        if (hit.collider != null) return;

        enemies.Add(Instantiate(spawnEntity, position, Quaternion.identity));

    }

   
}
