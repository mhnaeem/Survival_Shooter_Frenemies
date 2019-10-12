using UnityEngine;
using System.Collections;

public class FrenemyMovement : MonoBehaviour
{
    PlayerHealth playerHealth;
    EnemyHealth frenemyHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    public GameObject enemy;
    Vector3 dest1, dest2, dest3, dest4;

    //Current destination is where the frenemy should go
    Vector3 currDest;

    public void Awake()
    {
        Debug.Log("You have awakened a " + gameObject.name);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        frenemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        //Different spawn points if no enemies are present, could be changed to any random location on the map
        dest1 = new Vector3(-20.5f, 0f, 12.5f);
        dest2 = new Vector3(22.5f,0f,15f);
        dest3 = new Vector3(0f, 0f, 32f);
        dest4 = new Vector3(0f, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(enemy == null)
        {
            //Check if collided with an enemy
            if (other.gameObject == GameObject.Find("Zombunny(Clone)") || other.gameObject == GameObject.Find("ZomBear(Clone)") || other.gameObject == GameObject.Find("Hellephant(Clone)"))
            {
                enemy = other.gameObject;
                enemyHealth = enemy.GetComponent<EnemyHealth>();
            }
        }
    }

    void Update()
    {
        if (frenemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if(enemy != null)
            {
                //If enemy is active then set current destination to enemy's destination
                if (enemy.activeSelf == true && enemyHealth.currentHealth > 0)
                {
                    currDest = enemy.transform.position;
                }

                //If enemy is not active that means it was turned into a frenemy so change destination to random location and put enemy back to null
                else
                {
                    currDest = dest1;
                    enemy = null;
                }
            }

            //If current destination is one of the random locations then see if location was reached and change destination accordingly
            if(currDest == dest1 || currDest == dest2 || currDest == dest3 || currDest == dest4)
            {
                float dist = Vector3.Distance(gameObject.transform.position, currDest);
                if (dist <= 5f)
                {
                    changeCurrentDestination();
                }
            }
               
            nav.SetDestination(currDest);
        }
        else
        {
            nav.enabled = false;
        }
    }


    //Alternates between the four random locations
    void changeCurrentDestination()
    {
        if (currDest == null || currDest == dest4)
        {
            currDest = dest1;
        }
        else if (currDest == dest1)
        {
            currDest = dest2;
        }
        else if(currDest == dest2)
        {
            currDest = dest3;
        }
        else
        {
            currDest = dest4;
        }
    }
}
