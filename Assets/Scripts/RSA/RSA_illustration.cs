using UnityEngine;
using UnityEngine.UI;
using System;

public class RSA_illustration : MonoBehaviour {

    private DialogueManager dialog; // -------

    public GameObject blackBackground; // Turn it off at the start to see better the images
    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public GameObject message;
    public GameObject sceneElevenToCheck;
    public GameObject private_frame;
    public GameObject public_frame;
    public GameObject lock_image;
    public GameObject back_btn;

    //Public/private keys:
    private GameObject[] keys;
    public GameObject key_1;//alice private
    public GameObject key_2;//alice public
    public GameObject key_3;//bob private
    public GameObject key_4;//bob public

    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;
    private Rigidbody2D messageRB;
    private Rigidbody2D private_frameRB;
    private Rigidbody2D public_frameRB;
    private Rigidbody2D key_1RB;
    private Rigidbody2D key_4RB;

    private Vector2 velocity;
    private bool communication;
    // Use this for initialization
    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();
        messageRB = message.GetComponent<Rigidbody2D>();
        private_frameRB = private_frame.GetComponent<Rigidbody2D>();
        public_frameRB = public_frame.GetComponent<Rigidbody2D>();
        key_1RB = key_1.GetComponent<Rigidbody2D>();
        key_4RB = key_4.GetComponent<Rigidbody2D>();

        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        //controller = GameObject.Find("OneTimePadSceneController").GetComponent<OneTimePadSceneController>();

        blackBackground.SetActive(false); //DO NOT FORGET TO TURN IT ON AT THE END OF THE DEMONSTRATION !!!!!!
        envelope.SetActive(false);// Wait to display the envelope with sentence 4:

        communication = true;

        back_btn.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(dialog.getFinished_typing());

        if(dialog.currentSentenceDisplayed == 5 || dialog.currentSentenceDisplayed == 6)
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

            if(communication)
            {
                //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
                if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
                {
                    envelope.SetActive(true);
                    envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
                }
                if (envelope.transform.position.x >= 4.3) // If the envelope in its posstion:
                {
                    communication = false;
                    Debug.Log("Now false");
                }
            }
            else
            {
                //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
                if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x > -4.1)
                {
                    envelopeRB.MovePosition(envelopeRB.position + velocity * Time.fixedDeltaTime);
                }
                if (envelope.transform.position.x <= -4.1) // If the envelope in its posstion:
                {
                    communication = true;
                }
            }

            if(boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0) // If the envelope in its posstion:
            {
                dialog.setProceed(true); // Let the user continue
            }
        }
        if (dialog.currentSentenceDisplayed == 6)
        {
            dialog.setProceed(false); // Lock the user from proceeding 
            key_1.SetActive(true);
            key_2.SetActive(true);
            key_3.SetActive(true);
            key_4.SetActive(true);
            dialog.setProceed(true); // Lock the user from proceeding
        }

        if (dialog.currentSentenceDisplayed == 7)
        {
            envelope.SetActive(false);
        }

        //In sentence 8 we want to 
        if (dialog.currentSentenceDisplayed == 8)
        {
            dialog.setProceed(false);

            if (message.transform.position.y > 2.5)
            {
                messageRB.MovePosition(messageRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
            }
            else
                dialog.setProceed(true);
        }

        if (dialog.currentSentenceDisplayed == 9)
        {
            dialog.setProceed(false);

            if (private_frame.transform.position.x > 0)
            {
                private_frameRB.MovePosition(private_frameRB.position + new Vector2(-5, 0) * Time.fixedDeltaTime);
            }
            else
                dialog.setProceed(true);
        }

        if (dialog.currentSentenceDisplayed == 10)
        {
            dialog.setProceed(false);

            if (public_frame.transform.position.x <= 0) //Move girl pic:
            {
                public_frameRB.MovePosition(public_frameRB.position + velocity * (-1) * Time.fixedDeltaTime);
            }
            else
                dialog.setProceed(true);
        }

        if (dialog.currentSentenceDisplayed == 11)
        {
            if (dialog.getFinished_typing())
                dialog.setProceed(true);
            else
                dialog.setProceed(false);

            message.SetActive(false);
            private_frame.SetActive(false);
            public_frame.SetActive(false);
            lock_image.SetActive(true);
        }

        if (dialog.currentSentenceDisplayed == 12)
        {
            if (key_1.transform.position.x <= 0) //Move key to the right
            {
                dialog.setProceed(false);
                key_1RB.MovePosition(key_1RB.position + new Vector2(2.5f, 0) * Time.fixedDeltaTime);
            }
            else
            {
                if (key_4.transform.position.x > 0)
                {
                    key_4RB.MovePosition(key_4RB.position + new Vector2(-2.5f, 0) * Time.fixedDeltaTime);
                }
                else
                    dialog.setProceed(true);
            }
        }

        if (dialog.currentSentenceDisplayed == 14)
        {
            //Start the quiz
            blackBackground.SetActive(true);
        }
    }   
}