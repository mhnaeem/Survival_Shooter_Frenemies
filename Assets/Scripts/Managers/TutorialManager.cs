using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popupIndex;

    // Start is called before the first frame update
    void Start()
    {
        popupIndex = 0;
        popUps[0].SetActive(true);
        for (int i = 1; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (popupIndex == 0)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)){
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }
        else  if(popupIndex == 1) {
            popUps[popupIndex].SetActive(true);
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) //add the arrow keys or statement
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }

        else if (popupIndex == 2)
        {
            popUps[popupIndex].SetActive(true);
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }

        else if (popupIndex == 3)
        {
            popUps[popupIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space))
            {  
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }


    }
}
