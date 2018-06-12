/// FILE: DialogueManager.cs
/// PURPOSE: this class manages the dialogue system
/// 
/// FUNCTIONS:
/// 
/// void Start()
///     x
/// 
/// public void StartDialogue(Dialogue dialogue)
///     x
/// 
/// public void DisplayNextSentence()
///     x
/// 
/// void FixedUpdate()
///     x
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogue_box;     //textbox element to be dragged into inspector
    public GameObject portrait;         //^
    public Text text_name;              //^
    public Text text_dialogue;          //^

    public GameObject panel;

    private Queue<string> sentences;    //store lines of dialogue

    public float elapsed_time;          //keep track of time for lerping
    public bool has_started;            //check dialogue's state
    public bool has_ended;              //^

    public float x_pos_start_box        = 2.5f;     //the start position of each dialogue box component
    public float x_pos_start_portrait   = -6.0f;    //^
    public float x_pos_end_box          = 16.0f;    //^
    public float x_pos_end_portrait     = -12.0f;   //^

	// Use this for initialization
	void Start()
	{
        sentences = new Queue<string>(); //FIFO system
	}

    // Allows for enter key to display next sentence
    void Update() {
        if(!has_ended && has_started) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                DisplayNextSentence();
            }
        }
    }
    //TODO: more comments

    public void StartDialogue(Dialogue dialogue)
    {
        Vector2 current_pos = dialogue_box.transform.position;
        dialogue_box.transform.position = new Vector2(-16.0f, current_pos.y);    //reset position for lerp
        elapsed_time = 0;
        has_started = true;
        has_ended = false;

        text_name.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // sentences haven't ended
        if(!has_ended)
        {
            //are there more sentences in the queue?
            if(sentences.Count == 0)
            {
                elapsed_time = 0;
                has_ended = true;
                has_started = false;

                // switch out of dialogue
                GameControllerV2.Instance.DialogueSwitch();

                return;
            }
        }

        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(3);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //looping through each character, creating a typewriter effect
    IEnumerator TypeSentence(string sentence)
    {
        text_dialogue.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            text_dialogue.text += letter;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        // TODO: too messy, change to DOTween code (done partially)
        // moving the textbox smoothly
        if(has_started)
        {
            panel.GetComponent<Image>().raycastTarget = true;
            panel.GetComponent<Image>().DOColor(new Color32(50, 50, 50, 150), 0.5f);

            elapsed_time += Time.deltaTime;

            //dialogue box start:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_start_box, current_pos.y),
                0.5f * elapsed_time);

            //portrait start:
            /*Vector2 current_pos_portait = portrait.transform.position;
            portrait.transform.position = Vector2.Lerp(
                current_pos_portait,
                new Vector2(x_pos_start_portrait, current_pos_portait.y),
                0.5f * elapsed_time);*/
            portrait.transform.DOMove(new Vector2(x_pos_start_portrait, current_pos.y), 0.8f);
        }

        if(has_ended)
        {
            panel.GetComponent<Image>().raycastTarget = false;
            panel.GetComponent<Image>().DOColor(new Color32(50, 50, 50, 0), 0.5f);

            elapsed_time += Time.deltaTime;

            //dialogue box end:
            Vector2 current_pos = dialogue_box.transform.position;
            dialogue_box.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(x_pos_end_box, current_pos.y),
                0.5f * elapsed_time);

            //portrait end:
            /*Vector2 current_pos_portait = portrait.transform.position;
            portrait.transform.position = Vector2.Lerp(
                current_pos_portait,
                new Vector2(x_pos_end_portrait, current_pos_portait.y),
                0.5f * elapsed_time);*/
            portrait.transform.DOMove(new Vector2(x_pos_end_portrait, current_pos.y), 0.5f);
        }
    }
}
