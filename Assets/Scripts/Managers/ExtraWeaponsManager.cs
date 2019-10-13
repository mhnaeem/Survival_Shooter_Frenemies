using UnityEngine;

public class ExtraWeaponsManager : MonoBehaviour
{
    // Code by Muhammad Hammad
    public bool grenadeIconOn;
    public bool shotgunIconOn;

    // Start is called before the first frame update
    void Awake()
    {
        grenadeIconOn = false;
        shotgunIconOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        grenadeIconOn = PlayerShooting.grenadeLauncher;
        shotgunIconOn = PlayerShooting.shotgun;
        
        // Depending on what type of weapon is avaible for use make their icons visible or invisible.
        if (grenadeIconOn && this.name == "Bomb")
        {
            this.GetComponent<CanvasRenderer>().SetAlpha(255f);
        }
        else if (!grenadeIconOn && this.name == "Bomb")
        {
            this.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }

        if (shotgunIconOn && this.name == "Shotgun")
        {

            this.GetComponent<CanvasRenderer>().SetAlpha(255f);
        }
        else if (!shotgunIconOn && this.name == "Shotgun")
        {
            this.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }
}
