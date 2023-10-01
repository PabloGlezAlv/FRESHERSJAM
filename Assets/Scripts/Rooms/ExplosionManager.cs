using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private GameObject[] veinsList;
    [SerializeField] GameObject explosion;

    float timer = 0;
    float MAX_TIME = 1;
    void Start()
    {
        veinsList = new GameObject[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > MAX_TIME)
        {
            timer = 0;

            CommonInfo.cameraMoving = true;

            Invoke("CreateExplosion", CommonInfo.TimeMoving);
        }
    }

    public void CreateExplosion()
    {
        Debug.Log("Explosion");
        Transform pos = transform; //Chanfe to random position

        Instantiate(explosion, pos);
    }
}
