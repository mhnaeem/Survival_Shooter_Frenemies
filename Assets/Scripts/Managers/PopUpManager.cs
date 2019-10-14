using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int health;
    public GameObject healthPopup;
    public Text myText;
    public float start, end;
    public Color newColor;

    Text text;

    void Awake()
    {
        healthPopup = GameObject.Find("HealthPopup");
        text = healthPopup.GetComponent<Text>();
        healthPopup.SetActive(false);
        health = 100;
        start = 0f;
        end = 2f;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        text.text = "Wachout! Your health is: " + health;
        if (health == 50)
        {
            healthPopup.SetActive(true);
            start += Time.deltaTime;
            
            if (start > end)
            {
                healthPopup.SetActive(false);
            }
        }
        if (health == 20)
        {
            healthPopup.SetActive(true);
            text.color = Color.Lerp(text.color, newColor, Time.deltaTime);

        }
    }

}
