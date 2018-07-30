using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovingImagesAndText : MonoBehaviour {

    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;

    private DialogueManager dialog; // -------

    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public Text helloItIsAnnText;
    public GameObject arrow;

    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;

    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;

    //public Text textToMove;
    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;

    private Rigidbody2D helloItIsAnnTextRB;
    private Rigidbody2D arrowRB;

    //public Rigidbody2D texoToMoveRB;
    private Vector2 velocity;

    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();

        velocity = new Vector2(-5, 0);

        helloItIsAnnTextRB = helloItIsAnnText.GetComponent<Rigidbody2D>();
        arrowRB = arrow.GetComponent<Rigidbody2D>();
        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();


        envelope.SetActive(false);// Wait to display the envelope with sentence 4:
        arrow.SetActive(false);

        velocity = new Vector2(-5, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(boy.transform.position.x > 7.0){
            boyRB.MovePosition(boyRB.position + velocity * Time.fixedDeltaTime);

        }
        if (girl.transform.position.x < -7.0)
        {
            girlRB.MovePosition(girlRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }

        if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
        {
            envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
    }

    /*public void MovingEnvelope()
    {
        while(envelope.transform.position.x < 4.7)
=======

        if (dialog.currentSentenceDisplayed == 5 && arrow.transform.position.x < 3.85)
        {
            dialog.setProceed(false);
            arrow.SetActive(true);
            arrowRB.MovePosition(arrowRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
        if(arrow.transform.position.x >= 3.85)
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
        if (girl.transform.position.x < -7.0)
        {
            girlRB.MovePosition(girlRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
        if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
        {
            envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }

        }
        if (girl.transform.position.x < -7.0)
        {
            girlRB.MovePosition(girlRB.position + velocity * (-1) * Time.fixedDeltaTime);
        }
        if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
        {
            envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
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
