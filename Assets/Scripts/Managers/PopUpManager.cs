using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public static int health;
    public GameObject healthPopup;
    public float start, end;
    public Color noAplha;
    PlayerHealth health;
    Text text;

    void Awake()
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        healthPopup = GameObject.Find("HealthPopup");
        text = healthPopup.GetComponent<Text>();
        healthPopup.SetActive(false);
        start = 0f;
        end = 2f;
    }


    // Update is called once per frame
    void Update()
    {
        text.text = "Wachout! Your health is less then: " + health.currentHealth;
        if (health.currentHealth >= 40 && health.currentHealth <= 50)
        {
            
            healthPopup.SetActive(true);

            start += Time.deltaTime;

            if (start > end)
            {
                healthPopup.SetActive(false);
            }
        }
        if (health.currentHealth <= 20)
        {
            text.text = "Wachout! Your health is less then: " + health.currentHealth;
            healthPopup.SetActive(true);
            text.color = Color.Lerp(text.color, noAplha, Time.deltaTime);
        }

       
    }

}
