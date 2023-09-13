using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime >= 0)
        {
            Destroy(gameObject);
        }

        Move();
    }

    public void Move()
    {
        Vector2 dir = new Vector2();
        Vector2 force = dir * speed * Time.deltaTime;
    }
}
