using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool specialWeapon = false;         //Weapon for the frenemy

    //Code by Muhammad Hammad

    //Code for grenade launcher
    public static bool grenadeLauncher = false; //Switch to make grenade launcher available
    float timeBetweenBombs = 30f;
    float rangeGrenade = 8f;
    int damagePerBomb = 50;
    bool grenadeLauncherBeingUsed = false;      //Trigger to identify whether the grenade launcher is being used

    //Code for shotgun
    public static bool shotgun = false; //Switch to make shotgun available
    float timeBetweenShotGun = 10f;
    float rangeShotGun = 3f;
    int damagePerShotGun = 35;
    bool shotgunBeingUsed = false;    //Trigger to identify whether the shotgun is being used


    //Code for playing gun particles
    public GameObject explosiveEffects;
    public GameObject shotgunEffects;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();

        // These are effects for shotgun and grenade launcher, modification of the hit particles
        explosiveEffects = GameObject.Find("ExplParticle");
        shotgunEffects = GameObject.Find("ShotgunParticle");
    }


    void Update()
    {
        timer += Time.deltaTime;

        // If the score is divisible by 50 and time between bombs has passed then make grenade launcher available
        if(ScoreManager.score % 50 == 0 && timer >= timeBetweenBombs && Time.timeScale != 0 && ScoreManager.score != 0)
        {
            grenadeLauncher = true;
        }

        // If the score is divisible by 20 and time between shotgun has passed then make shotgun available
        if (timer >= timeBetweenShotGun && Time.timeScale != 0 && ScoreManager.score != 0 && ScoreManager.score % 20 == 0)
        {
            shotgun = true;
        }

        //If left shift is pressed and shotgun is available
        if (shotgun && Input.GetButton("Fire3"))
        {
            shotgun = false;
            shotgunBeingUsed = true;
            Shoot();
            shotgunBeingUsed = false;
        }

        //If Spacebar is pressed and grenade launcher is available
        if (Input.GetButton("Jump") && grenadeLauncher)
        {
            grenadeLauncher = false;
            grenadeLauncherBeingUsed = true;
            Shoot();
            grenadeLauncherBeingUsed = false;
        }

        else if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            specialWeapon = Input.GetButton("Fire2");

            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    // Changes the colour of the gunline renderer based on the type of weapon being used
    void changeGunLineColour()
    {
        if (shotgunBeingUsed)
        {
            gunLine.material.color = Color.red;
        }
        else if (grenadeLauncherBeingUsed)
        {
            gunLine.material.color = Color.white;
        }
        else if (specialWeapon)
        {
            gunLine.material.color = Color.blue;
        }
        else
        {
            gunLine.material.color = Color.yellow;
        }
    }

    // Returns the correct range of the gun based on the type of the weapon being used
    float rangeSelect()
    {
        if (grenadeLauncherBeingUsed)
        { 
            return rangeGrenade;
        }
        else if (shotgunBeingUsed)
        {
            return rangeShotGun;
        }
        else
        {
            return range;
        }
    }

    // Edited by Muhammad Hammad, some of it is from the original survival shooter game
    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Change the colour of the gun line
        changeGunLineColour();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Select the correct range based on the weapon
        float tempRange = rangeSelect();

        //Raycast and see what collider it hits
        if (Physics.Raycast(shootRay, out shootHit, tempRange, shootableMask))
        {
            // Play the effects based on what type of weapon is being used
            if (grenadeLauncherBeingUsed)
            {
                explosiveEffects.transform.position = shootHit.transform.position;
                explosiveEffects.GetComponent<ParticleSystem>().Play();
            }
            else if (shotgunBeingUsed)
            {
                shotgunEffects.transform.position = shootHit.transform.position;
                shotgunEffects.GetComponent<ParticleSystem>().Play();
            }

            // Get the EnemyHealth script from the collider that is hit
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            //If an enemy was hit then proceed otherwise go to else
            if (enemyHealth != null)
            {
                if (grenadeLauncherBeingUsed || shotgunBeingUsed)
                {
                    Vector3 enemyHitPos = enemyHealth.transform.position;

                    //Get all the colliders in the radius of 5f
                    foreach (Collider i in Physics.OverlapSphere(enemyHitPos, 5f))
                    {
                        //If the collider is an enemy then deal damage depending on what weapon was used.
                        if (i.GetComponent<EnemyHealth>() != null)
                        {
                            if (grenadeLauncherBeingUsed)
                            {
                                i.GetComponent<EnemyHealth>().TakeDamage(damagePerBomb, i.transform.position);
                            }
                            else if (shotgunBeingUsed)
                            {
                                i.GetComponent<EnemyHealth>().TakeDamage(damagePerShotGun, i.transform.position);
                            }
                        }
                    }
                }
                else if (specialWeapon)
                {
                    enemyHealth.Convert();
                }
                else
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
            }
            gunLine.SetPosition(1, shootHit.point);

        }

        //If no colliders were hit by the raycast then
        else
        {
            //Play the effects for each weapon and change the gunline distance

            if (grenadeLauncherBeingUsed)
            {
                //Code for explosive effects
                explosiveEffects.transform.position = shootRay.origin + shootRay.direction * tempRange;
                explosiveEffects.GetComponent<ParticleSystem>().Play();
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * rangeGrenade);
            }
            else if (shotgunBeingUsed)
            {
                //Code for explosive effects
                shotgunEffects.transform.position = shootRay.origin + shootRay.direction * tempRange;
                shotgunEffects.GetComponent<ParticleSystem>().Play();
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * rangeShotGun);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
