using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovingImagesAndText : MonoBehaviour {

    private DialogueManager dialog; // -------

    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public Text helloItIsAnnText;
    public GameObject arrow;

    //public Text textToMove;
    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;
    private Rigidbody2D helloItIsAnnTextRB;
    private Rigidbody2D arrowRB;

    //public Rigidbody2D texoToMoveRB;
    private Vector2 velocity;

    // Use this for initialization
    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();
        helloItIsAnnTextRB = helloItIsAnnText.GetComponent<Rigidbody2D>();
        arrowRB = arrow.GetComponent<Rigidbody2D>();
        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();


        envelope.SetActive(false);// Wait to display the envelope with sentence 4:
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dialog.currentSentenceDisplayed == 3)
        {
            dialog.setProceed(false); // Lock the user from proceeding 

            if(boy.transform.position.x > 7.0) //Move boy pic:
            {
                boyRB.MovePosition(boyRB.position + velocity * Time.fixedDeltaTime);
            }
            if (girl.transform.position.x < -7.0) //Move girl pic:
            {
                girlRB.MovePosition(girlRB.position + velocity * (-1) * Time.fixedDeltaTime);
            }
            //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
            if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
            {
                envelope.SetActive(true);
                envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
            }

            if(envelope.transform.position.x >= 4.3) // If the envelope in its posstion:
            {
                dialog.setProceed(true); // Let the user continue
            }
        }

        //If the sentennce number displayed is 4 and the position of the text in not at posstion y=3:
        if (dialog.currentSentenceDisplayed == 4 && helloItIsAnnText.transform.position.y > 3)
        {
            boy.SetActive(false);
            girl.SetActive(false);
            envelope.SetActive(false);

            helloItIsAnnTextRB.MovePosition(helloItIsAnnTextRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
        }

        if (dialog.currentSentenceDisplayed == 5 && arrow.transform.position.x < 3.85)
        {
            dialog.setProceed(false);
            arrow.SetActive(true);
            arrowRB.MovePosition(arrowRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
        if(arrow.transform.position.x >= 3.85)
        {
            dialog.setProceed(true);
        }

    }

    /*public void MovingEnvelope()
    {
        while(envelope.transform.position.x < 4.7)
        {
            envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
    }
    */
    IEnumerator MovingEnvelope()
    {
        //Wait for a little before displaying the message:
        yield return new WaitForSeconds(1.0f);//Wait for 4 seconds.

        if (envelope.transform.position.x < 4.7)
        {
            envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
    }
}
