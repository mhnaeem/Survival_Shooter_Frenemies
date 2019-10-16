using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code written by RonakBParmar, Std No. 201707866

/* This is a tutorial manager class that manages the on screen tutorial poups
   when the game first starts. It interactively teaches the users the game
   controls and making them familiar with their keybinds */

public class TutorialManager : MonoBehaviour
{
    /* These are the variables/object variables that we will be using in the class
       I created a list of GameObjects called popUps that will have all the different 
       text objects in it so we coudl refer to it and set when to diplay the pop-up */
    
    public GameObject[] popUps;
    private int popupIndex;
    
    // These start and end variables are used to adjust how long a popup stays on screen
    public float start, end, start1, end1, start2, end2;
    public Color noAplha;
    
    /* These refer to the Text component of the shotgunText object and the gLtext object so that 
       we could modify the text when we want */
    Text shotgunText;
    Text gLText;


    // Start is called before the first frame update
    void Start()
    {   
        /* Here we assign the popups gameobject object inside the popups list so that we can refer back to
           them. The Objects that I created were moveLR, moveWS, shoot, SGActive, GLActive. As the names explain
           each are popus for an action. For example moveLR is the popup that tells the player to press L or R.
           SGActive is the popup that lets the users know when shotgun is active and same applies for GLAtive for grenade launcher */
           
        popUps = new GameObject[5];
        popUps[0] = GameObject.Find("moveLR");
        popUps[1] = GameObject.Find("moveWS");
        popUps[2] = GameObject.Find("shoot");
        popUps[3] = GameObject.Find("SGActive");
        popUps[4] = GameObject.Find("GLActive");
        
        // Hiding all the popups when the game begins
        popupIndex = 0;
        for (int i = 1; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
        /* Turning the shotgun and gl transparent to the point that they become invisible. This will be helpful in
           turning the popups on and off without having to turn off the whole object */
        popUps[3].GetComponent<CanvasRenderer>().SetAlpha(0f);
        popUps[4].GetComponent<CanvasRenderer>().SetAlpha(0f);

        // Time on how long the shotgun and grenade launcher stays on screen for 
        start1 = 0f;
        end1 = 6f;
        start2 = 0f;
        end2 = 6f;
    }

    // Update is called once per frame
    void Update()
    {   
        // turning all all the popups to true depending on and until popupIndex is not more than 4.
        if (popupIndex <= 4) {
            popUps[popupIndex].SetActive(true);
        }

        /* For lines 76 to 100, the inteaction occurs. The popupIndex is initially zero so the first popup that is the moveLR 
           will be active, but this will stay on screen until user actually presses L or R or left or roght arrow so this brings
           interaction into play so the player can learn the controls while using them. The same works for the other popus when 
           index is increased */
        if (popupIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }
        else if (popupIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) //add the arrow keys or statement
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }

        else if (popupIndex == 2)
        {   
            // if left click or left control is pressed
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }
        
        /* For lines it checks if the shotgun/grenade launcher is available, and once it is, it sets the popup 
        active and sets the text to full opaque. The text is not interactive here becasue the user might want to save
        the shotgun so might not press the buttons, so instead it relies on a timer and the text fades to transperent*/
        
        else if (popupIndex == 3 && PlayerShooting.shotgun)
        {   
            shotgunText = popUps[popupIndex].GetComponent<Text>();
            popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(255f);
            start1 += Time.deltaTime;
            if (start1 >= end1)
            {
                // time over, set to transparent
                popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(0f);
                popupIndex++;

            }
            if (start1 >= 2)
            {
                shotgunText.text = "Check the shotgun icon on the bottom-left" +"\n" +"next time when its available";
            }
            

        }

        else if (popupIndex == 4 && PlayerShooting.grenadeLauncher)
        {   
            /* Here we are getting the text component to set the transparency and also change the text in-between the 
               timer becasue the text could be long so we devide it and change it in between to conserve space */
            gLText = popUps[popupIndex].GetComponent<Text>();
            popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(255f);
            start2 += Time.deltaTime;
            if (start2 >= end2)
            {
                popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(0f);
                popupIndex++;

            }
            // After 2 seconds change the text to this
            if (start2 >= 2)
            {
                gLText.text = "Check the bomb icon on the bottom-left" + "\n" + "next time when its available";
            }

        }

    }
    
}
