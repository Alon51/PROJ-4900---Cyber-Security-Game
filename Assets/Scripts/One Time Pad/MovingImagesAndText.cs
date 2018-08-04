using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class MovingImagesAndText : MonoBehaviour {

    private DialogueManager dialog; // -------

    public GameObject blackBackground; // Turn it off at the start to see better the images
    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public Text helloItIsAnnText;
    public GameObject arrow;
    public GameObject squares; // To move them as a group, located on top of the window
    public Text[] squares_numbers ;
    public GameObject sceneTenToCheck;

    //public Text textToMove;
    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;
    private Rigidbody2D helloItIsAnnTextRB;
    private Rigidbody2D arrowRB;
    private Rigidbody2D squaresRB;
    //private Rigidbody2D[] squares_numbersRB;  -------- not sure if we need it, numbers not suppose to move

    //public Rigidbody2D texoToMoveRB;
    private Vector2 velocity;

    bool arrowInPosition = true; // In sentence 6, when arrow in postion allow the arrow to go from left to right again.

    float timePassed = 0; // For some reason using StartCoroutine() didn't work, had a problem with infinite loop. This varible is to count the time.

    // Use this for initialization
    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();
        helloItIsAnnTextRB = helloItIsAnnText.GetComponent<Rigidbody2D>();
        arrowRB = arrow.GetComponent<Rigidbody2D>();
        squaresRB = squares.GetComponent<Rigidbody2D>();

        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();

        blackBackground.SetActive(false); //DO NOT FORGET TO TURN IT ON AT THE END OF THE DEMONSTRATION !!!!!!
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

        //For senctence 5 I possioned the arrow at  x = -3.80 and will move it to  x=3.85
        if (dialog.currentSentenceDisplayed == 5 && arrow.transform.position.x < 4)
        {
            dialog.setProceed(false); // lock the continue button
            arrow.SetActive(true); // Show the arrow in the screen
            arrowRB.MovePosition(arrowRB.position + velocity * (-1) * Time.fixedDeltaTime);

            if (arrow.transform.position.x >= 3.85)// after the arrow got to the end of the sentence, release the lock
            {
                dialog.setProceed(true);
            }
        }

        //Sentence 6, generate the numbers
        if (dialog.currentSentenceDisplayed == 6)
        {
            dialog.setProceed(false); // lock the continue button

            if(arrowInPosition)
            {
                arrowInPosition = !arrowInPosition; // now the varible is false and we won't repeat that again
                // move the arrow to the first number - for now I chose the easy way 
                arrowRB.transform.position = new Vector3(-8.3f, 2.30f);
                arrow.SetActive(false);    
            }

            // drag out the text upward
            if(helloItIsAnnText.transform.position.y < 8)
            {
                helloItIsAnnTextRB.MovePosition(helloItIsAnnTextRB.position + new Vector2(0, 5) * Time.fixedDeltaTime);    
            }
            // lower the numbers into the screen
            if(squares.transform.position.y > -3) // > 0
            {
                squaresRB.MovePosition(squaresRB.position + new Vector2(0,-5) * Time.fixedDeltaTime);
            }

            if(squares.transform.position.y <= 0) // Show the arrow in it's new postion
            {
                arrow.SetActive(true);  
            }
            if (arrowRB.position.x < 7.70f)
            {
                arrowRB.MovePosition(arrowRB.position + new Vector2(-3, 0) * (-1) * Time.fixedDeltaTime);

                if(arrowRB.position.x > -8f && arrowRB.position.x < -7.8f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[0].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > -6.5f && arrowRB.position.x < -6.3f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[1].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > -5.0f && arrowRB.position.x < -4.8f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[2].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > -3.7f && arrowRB.position.x < -3.5f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[3].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > -2.2f && arrowRB.position.x < -2.0f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[4].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > -0.74f && arrowRB.position.x < -0.60f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[5].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > 0.6f && arrowRB.position.x < 0.74f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[6].text = "" + Random.Range(1, 26); // 6
                }
                if (arrowRB.position.x > 2.0f && arrowRB.position.x < 2.2f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[7].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > 3.5f && arrowRB.position.x < 3.7f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[8].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > 4.8f && arrowRB.position.x < 5.0f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[9].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > 6.3f && arrowRB.position.x < 6.5f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[10].text = "" + Random.Range(1, 26);
                }
                if (arrowRB.position.x > 7.4f && arrowRB.position.x < 7.8f) // arrow.x >-6.33 and arrow.x < -6.3
                {
                    squares_numbers[11].text = "" + Random.Range(1, 26);
                }
            }
        }
    }   
}