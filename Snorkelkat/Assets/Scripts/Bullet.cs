using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10;
    public int speed;
    public int damageAmount;

    public LayerMask enemieLayers;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        Move();
    }

    public void Move()
    {
        rb.velocity = transform.right * speed;

        
        //Vector2 dir = new Vector2();
        //Vector2 force = dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageAmount);
            Debug.Log("HIT" + other.gameObject.name);
        }
    }
}
