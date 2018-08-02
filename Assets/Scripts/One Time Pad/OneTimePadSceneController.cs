using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneTimePadSceneController : MonoBehaviour {

    //public GameObject obj;
    private DialogueManager dialog;
    private MovingImagesAndText movingObjects; // object to the script to move images on the screen

    private GameObject scn_main;
    public GameObject demonstademonstration; // drag and drop the the demonstademonstration child object of the scn_one_time_pad

	// Use this for initialization
	void Awake () {
        
        // displays opening text
        GameObject.Find("dlg_one_time_pad").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        //Deactivating the scn_main to show the animation better without the background:
        scn_main = GameObject.Find("scn_main");
        scn_main.SetActive(false);

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void FixedUpdate()
    {
        switch(dialog.currentSentenceDisplayed)
        {
            case(0):
            {
                    
            };break;
            case (1):
            {


            }; break;
            case (2):
            {


            }; break;
            case (3):
            {
                demonstademonstration.SetActive(true);
                //scn_main.SetActive(true);
            }; break;
            case (4):
            {

            }; break;
            case (5):
            {
                //movingObjects.MovingEnvelope();
            }; break;
            case (6):
            {
                
            }; break;
            case (7):
            {
                
            }; break;
            case (8):
            {

            }; break;
            case (9):
            {

            }; break;
            case (10):
            {
                
            }; break;
        }
    }
}
