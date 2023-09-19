using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    int currentHealth;

    public int damageAmount;
    public float attackRate;
    float nextAttackTime = 0f;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ouch, im the enemy and im hurt :(" + damage);
        currentHealth -= damage;
        sprite.color = Color.white;
        StartCoroutine("ChangeColor");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.red;
    }
    public void Die()
    {
        Debug.Log(gameObject.name + " Died");
        Destroy(gameObject);
    }

}
