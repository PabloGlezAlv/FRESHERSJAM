using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] // Size of the cell
    private int size = 1;
    [SerializeField]
    protected float speed = 10.0f;
    protected Rigidbody2D rb;

    public int getSize()
    {
        return size;
    }
}
