using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popupIndex;
    public float start, end, start1, end1, start2, end2;
    public Color noAplha;
    Text shotgunText;
    Text gLText;




    // Start is called before the first frame update
    void Start()
    {
        popUps = new GameObject[5];
        popUps[0] = GameObject.Find("moveLR");
        popUps[1] = GameObject.Find("moveWS");
        popUps[2] = GameObject.Find("shoot");
        popUps[3] = GameObject.Find("SGActive");
        popUps[4] = GameObject.Find("GLActive");
        popupIndex = 0;
        for (int i = 1; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
        //turning the shotgun and gl transparent
        popUps[3].GetComponent<CanvasRenderer>().SetAlpha(0f);
        popUps[4].GetComponent<CanvasRenderer>().SetAlpha(0f);

        start1 = 0f;
        end1 = 6f;
        start2 = 0f;
        end2 = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (popupIndex <= 4) {
            popUps[popupIndex].SetActive(true);
        }

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
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                popUps[popupIndex].SetActive(false);
                popupIndex++;
            }
        }

        else if (popupIndex == 3 && PlayerShooting.shotgun)
        {
            shotgunText = popUps[popupIndex].GetComponent<Text>();
            popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(255f);
            start1 += Time.deltaTime;
            if (start1 >= end1)
            {
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
            gLText = popUps[popupIndex].GetComponent<Text>();
            popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(255f);
            start2 += Time.deltaTime;
            if (start2 >= end2)
            {
                popUps[popupIndex].GetComponent<CanvasRenderer>().SetAlpha(0f);
                popupIndex++;

            }
            if (start2 >= 2)
            {
                gLText.text = "Check the bomb icon on the bottom-left" + "\n" + "next time when its available";
            }

        }
        


    }
}
