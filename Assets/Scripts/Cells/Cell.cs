using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] // Size of the cell
    private float size = 0.5f;
    [SerializeField]
    protected float speed = 10.0f;
    protected Rigidbody2D rb;

    public float getSize()
    {
        return size;
    }
}
