using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Written by RonakBParmar Std.No 201707866

public class PopUpManager : MonoBehaviour
{
    
    /*We initialize the variables/ object variables. GameObject healthPopup refers to the actual 
       popup text for health warning in HUD canvas */
    public GameObject healthPopup;
    public float start, end;
    public Color noAplha;
    // Direct reference to players health
    PlayerHealth health;
    // Reference to text object that will be assigned to the text component of healthPopup
    Text text;

    // Assignment of Variables
    void Awake()
    {
        // Health is assigned to the component called PlayerHealth
        health = GameObject.Find("Player").GetComponent<PlayerHealth>();
        // This finds the healthPopup in HUD canvas
        healthPopup = GameObject.Find("HealthPopup");
        text = healthPopup.GetComponent<Text>();
        healthPopup.SetActive(false);
        start = 0f;
        end = 2f;
    }


    // Update is called once per frame
    void Update()
    {
        // This keeps updating the text of the popup to the current health
        text.text = "Wachout! Your health is less then: " + health.currentHealth;
        
        // Range of health given as a condition to exclude potential cases when health is not an exact multiple of 10
        if (health.currentHealth >= 40 && health.currentHealth <= 50)
        {
            // Set the popup to active and turn it off after 2 seconds
            healthPopup.SetActive(true);

            start += Time.deltaTime;

            if (start > end)
            {
                healthPopup.SetActive(false);
            }
        }
        // This is for the time when health is critically low. It shows the player the popup again to warn him/her
        if (health.currentHealth <= 20)
        {
            text.text = "Wachout! Your health is less then: " + health.currentHealth;
            healthPopup.SetActive(true);
            text.color = Color.Lerp(text.color, noAplha, Time.deltaTime);
        }
       
    }

}
