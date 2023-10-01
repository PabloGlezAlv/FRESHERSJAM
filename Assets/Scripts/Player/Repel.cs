using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repel : MonoBehaviour
{

    [SerializeField]
    private float knockback = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RepelCells()
    {

        foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, transform.localScale.x + 1f)) {
            
            if (coll.GetComponent<Cell>() != null)
            {
                coll.GetComponent<Cell>().KnockBack((coll.transform.position - transform.position).normalized * knockback);
            }
        }
    }
}
