using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] // Size of the cell
    private float size = 0.5f;
    [SerializeField] // Size of the cell
    private float sizeToEat = 1.0f;
    [SerializeField]
    protected float speed = 10.0f;
    protected Rigidbody2D rb;

    [SerializeField]
    private float sizeHitDecrease = 0.5f;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public float getSizeHit()
    {
        return sizeHitDecrease;
    }

    public float getSize()
    {
        return size;
    }
    public float getSizeToEat()
    {
        return sizeToEat;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMov>()) 
        {
            PlayerEat peat = collision.gameObject.GetComponentInChildren<PlayerEat>();
            peat.reduceSize(sizeHitDecrease);
        }
    }
}
