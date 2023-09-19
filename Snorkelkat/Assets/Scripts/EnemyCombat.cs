using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    int currentHealth;

    public int damageAmount;
    public float attackRange;
    [Header("Attack rate (Attacks per second)")]
    public float attackRate;
    float nextAttackTime = 0f;
    private SpriteRenderer sprite;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if(distance < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(damageAmount);    
                nextAttackTime = Time.time + 1f / attackRate;
                
            }
        }
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
