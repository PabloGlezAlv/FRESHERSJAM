using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("LMAO2!");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<Collider2D>().enabled = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("LMAO!");
        Debug.Log(collision.name);
        if (collision.CompareTag("Player")) {
            
            collision.gameObject.GetComponent<Collider2D>().enabled = true;

        }
    }
}
