using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Serializefield] private int size = 1;

    public int getSize()
    {
        return size;
    }
}
