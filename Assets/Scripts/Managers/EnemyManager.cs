using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    // Code by Muhammad Hammad
    //This is a trigger that can be set to true anywhere in the program, once true it will change the random spawn point
    static bool makeNewSpawnPoint = false;
    private float seconds = 20f;
    private float timer = 0f;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    // Code by Muhammad Hammad
    private void Update()
    {
        timer += Time.deltaTime;

        // If the more than 20seconds have passed inbetween the last random location change and the score is divisible by 50 then
        if(timer >= seconds && ScoreManager.score != 0 && ScoreManager.score % 50 == 0)
        {
            
            timer = 0;
            
            //Set the trigger to true
            makeNewSpawnPoint = true;

            //Play the alarm clock sound, indicating the change of spawn points
            gameObject.GetComponent<AudioSource>().Play();

            //If there are more than 3 original spawn points then delete the last one
            if (GameObject.Find("EnemyManager").GetComponents<EnemyManager>().Length > 3)
            {
                deleteRandomSpawnPoint();
            }
        }

        //If trigger is true then create a new spawn point and trigger goes back to false
        if (makeNewSpawnPoint)
        {
            makeNewRandomSpawnPoint();
            makeNewSpawnPoint = false;
        }
    }

    // Deletes the the last random spawn point
    public static void deleteRandomSpawnPoint()
    {
        // Find the enemy manager object then get all it's EnemyManager components and destroy the last one (random one).
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        EnemyManager[] enemyManagers = enemyManagerObject.GetComponents<EnemyManager>();
        DestroyImmediate(enemyManagers[enemyManagers.Length - 1]);
        
    }

    //Creates a new random spawn point
    public void makeNewRandomSpawnPoint()
    {
        // Find the enemy manager object
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");

        // Add a new EnemyManager script to the object for a random spawnpoint
        enemyManagerObject.AddComponent<EnemyManager>();

        // Array of all the EnemyManager components
        EnemyManager[] enemyManagers = enemyManagerObject.GetComponents<EnemyManager>();

        // Get the last last EnemyManager script which is the new one just added
        EnemyManager newEnemyManager = enemyManagers[enemyManagers.Length - 1];

        // Initialize the spawn point position in the newly added random EnemyManager script
        newEnemyManager.spawnPoints = new Transform[1];
        newEnemyManager.spawnPoints[0] = GameObject.Find("RandomSpawnPoint").GetComponent<Transform>();

        // Select a random coordinate for the new spawn point
        newEnemyManager.spawnPoints[0].position = new Vector3(Random.Range(-25, 26), 0, Random.Range(-25, 26));
        
        // Load Zombunny as the type of spawn enemy
        newEnemyManager.enemy = Resources.Load("Zombunny") as GameObject;

        // Set the player health and spawn intervals
        newEnemyManager.playerHealth = playerHealth;
        newEnemyManager.spawnTime = 15f;
    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
