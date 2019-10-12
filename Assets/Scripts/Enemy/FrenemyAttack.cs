using UnityEngine;
using System.Collections;

public class FrenemyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth frenemyHealth;

    GameObject enemy;

    bool enemyInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        frenemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        enemy = gameObject.GetComponent<FrenemyMovement>().enemy;
        if (other.gameObject == this.enemy)
        { 
            enemyInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == enemy)
        {
            enemyInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && enemyInRange && frenemyHealth.currentHealth > 0 && enemy != null)
        {
            if (enemy.GetComponent<EnemyHealth>().currentHealth > 0)
            {
                Attack(); 
            }
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0 && enemy.GetComponent<EnemyHealth>().currentHealth > 0)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage,enemy.transform.position);
            frenemyHealth.TakeDamage(attackDamage, gameObject.transform.position);
        }
    }
}
