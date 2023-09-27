using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if(collision.gameObject.GetComponent<RunAway>())
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.GetComponent<Roaming>())
        {
            Destroy(collision.gameObject);
        }
    }
}
