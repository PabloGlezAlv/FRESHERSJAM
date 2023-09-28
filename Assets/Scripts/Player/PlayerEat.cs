using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.name);

        if(collision.gameObject.GetComponent<Cell>())
        {
            Destroy(collision.gameObject);
        }
    }
}
