using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [SerializeField]
    private Boss boss;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;

        //Debug.Log("LMAO!");
        //Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<Collider2D>().enabled = true;
            boss.enabled = true;
            enabled = false;
        }

        
    }

}
