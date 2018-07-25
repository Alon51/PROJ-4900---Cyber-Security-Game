using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimePadSceneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        // displays opening text
        GameObject.Find("dlg_one_time_pad").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
	}
}
