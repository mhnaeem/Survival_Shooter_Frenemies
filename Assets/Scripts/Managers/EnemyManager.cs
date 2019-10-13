using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    static bool makeNewSpawnPoint = false;

    static int x = 0;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    private void Update()
    {
        // Change makeNewSpawnPoint to true anywhere in the scripts and it will make a new spawn point
        if(x < 2)
        {
            makeNewSpawnPoint = true;
            if (x == 1)
            {
                // This will delete the last created spawn point
                deleteRandomSpawnPoint();
            }
            x++;
        }

        if (ScoreManager.score == 0 && makeNewSpawnPoint)
        {
            makeNewRandomSpawnPoint();
        }
    }

    public static void deleteRandomSpawnPoint()
    {
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        EnemyManager[] enemyManagers = enemyManagerObject.GetComponents<EnemyManager>();
        Destroy(enemyManagers[enemyManagers.Length - 1]);
        
    }

    public void makeNewRandomSpawnPoint()
    {
        makeNewSpawnPoint = false;
        GameObject enemyManagerObject = GameObject.Find("EnemyManager");
        enemyManagerObject.AddComponent<EnemyManager>();
        EnemyManager[] enemyManagers = enemyManagerObject.GetComponents<EnemyManager>();

        EnemyManager newEnemyManager = enemyManagers[enemyManagers.Length - 1];
        newEnemyManager.spawnPoints = new Transform[1];
        newEnemyManager.spawnPoints[0] = GameObject.Find("RandomSpawnPoint").GetComponent<Transform>();

        newEnemyManager.spawnPoints[0].position = new Vector3(Random.Range(-25, 26), 0, Random.Range(-25, 26));
        
        newEnemyManager.enemy = Resources.Load("Zombunny") as GameObject;
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
