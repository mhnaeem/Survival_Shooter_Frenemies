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
    bool specialWeapon = false;


    //Code for grenade launcher
    public static bool grenadeLauncher = false;
    float timeBetweenBombs = 0.5f;
    float rangeGrenade = 8f;
    int damagePerBomb = 30;
    bool grenadeLauncherBeingUsed = false;

    //Code for shotgun
    public static bool shotgun = false;
    float timeBetweenShotGun = 0.7f;
    float rangeShotGun = 3f;
    int damagePerShotGun = 15;
    bool shotgunBeingUsed = false;


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

        explosiveEffects = GameObject.Find("ExplParticle");
        shotgunEffects = GameObject.Find("ShotgunParticle");
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer * 0.1 >= timeBetweenBombs && Time.timeScale != 0)
        {
            grenadeLauncher = true;
        }

        if (timer * 0.3 >= timeBetweenShotGun && Time.timeScale != 0)
        {
            shotgun = true;
        }

        if (shotgun && Input.GetButton("Fire3"))
        {
            shotgun = false;
            shotgunBeingUsed = true;
            Shoot();
            shotgunBeingUsed = false;
        }

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

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        changeGunLineColour();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        float tempRange = rangeSelect();

        if (Physics.Raycast(shootRay, out shootHit, tempRange, shootableMask))
        {
            if (grenadeLauncherBeingUsed)
            {
                //Code for explosive effects
                explosiveEffects.transform.position = shootHit.transform.position;
                explosiveEffects.GetComponent<ParticleSystem>().Play();
            }
            else if (shotgunBeingUsed)
            {
                shotgunEffects.transform.position = shootHit.transform.position;
                shotgunEffects.GetComponent<ParticleSystem>().Play();
            }

            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                if (grenadeLauncherBeingUsed || shotgunBeingUsed)
                {
                    Vector3 enemyHitPos = enemyHealth.transform.position;

                    foreach (Collider i in Physics.OverlapSphere(enemyHitPos, 5f))
                    {
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
        else
        {
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
