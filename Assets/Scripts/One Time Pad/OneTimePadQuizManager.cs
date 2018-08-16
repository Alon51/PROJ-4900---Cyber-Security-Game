using UnityEngine;
using UnityEngine.UI;

public class OneTimePadQuizManager : MonoBehaviour {

    public static byte round = 1;

    // Answers given for questions
    //public InputField decryptedAnswer;
    public InputField encryptedAnswer;
    //public InputField bonusAnswer;

    // 3 rounds - decryption, encryption, bonus
    //public GameObject decryptedRound;
    public GameObject encryptedRound;
    public GameObject generalQuestion;
    //public GameObject bonusRound;

    // continue button
    public GameObject nextButton;

    public GameObject button_menue;// the button in the main scene that allow you to start over the game

    public GameObject squares_random; 
    public Text[] squaresT_random;

    public Text textToChange; // this is what make the difference between the questions 1 and 2

    private string encryptionAnswer; // here I will save the result for question 1 (encrypt a sentence)
    private byte[] otpTheBest = { 15, 20, 16, 20, 8, 5, 2, 5, 19, 20 }; // Saving the values of each letter
    private byte[] itsTheSame = {  9, 20, 19, 20, 8, 5, 19, 1, 13, 5 };
    private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public GameObject ECorrect;
    public GameObject EIncorrect;
    public GameObject GCorrect;
    public GameObject GIncorrect;

    public GameObject hint;
    public GameObject instructions;
    private bool trueFalse;

    public GameObject questions;

    // starts with decryption question
    void Start()
    {       
        encryptedRound.SetActive(true);
        //decryptedRound.SetActive(false);
        //bonusRound.SetActive(false);
        button_menue.SetActive(false);// no need for this button in the quiz
        squares_random.SetActive(true);
        generalQuestion.SetActive(false);
    }

    // proceed to next question
    public void NextSet()
    {
        switch (round)
        {
            case 1:

                //The sentence "otp the best" has 10 letters equivalent to the values: 15 20 16 20 8 5 2 5 19 20:
                for (int i = 0; i < 10; i++)    
                {
                    if (i == 3 || i == 6)
                        encryptionAnswer += " ";

                    //Check to see if I get 26 % 26 which will give 0 and 0 - 1 is array out of range
                    if(((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) == 0) 
                        encryptionAnswer += letters[25];// add the last letter which is Z
                    else
                        encryptionAnswer += letters[((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) - 1];
                }

                // removes potential capitalization mistakes
                if (encryptedAnswer.textComponent.text.ToLower() == encryptionAnswer.ToLower()) 
                {
                    Debug.Log("Entering encryption round");
                    
                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
                    
                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    //GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                    ECorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);
                    
                    // Feedback for incorrect answer
                    //GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                    EIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                // Next round
                round++;

                //Reseting all values:
                textToChange.text = "\"Its the same\"";

                for (int i = 0; i < 10; i++) // reset the random generator
                {
                    squaresT_random[i].text = "0";
                }

                encryptionAnswer = ""; // reseting the answer to check it with a different combination
                encryptedAnswer.text = ""; 

                break;

            case 2:
                
                //The sentence "otp the best" has 10 letters equivalent to the values: 15 20 16 20 8 5 2 5 19 20:
                for (int i = 0; i < 10; i++)
                {
                    if (i == 3 || i == 6)
                        encryptionAnswer += " ";

                    //Check to see if I get 26 % 26 which will give 0 and 0 - 1 is array out of range
                    if (((int.Parse(squaresT_random[i].text) + itsTheSame[i]) % 26) == 0)
                        encryptionAnswer += letters[25];// add the last letter which is Z
                    else
                        encryptionAnswer += letters[((int.Parse(squaresT_random[i].text) + itsTheSame[i]) % 26) - 1];
                }

                // removes potential capitalization mistakes
                if (encryptedAnswer.textComponent.text.ToLower() == encryptionAnswer.ToLower()) //"xvywx rsfshc")
                {
                    Debug.Log("Entering encryption round");

                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    //GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                    ECorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    //GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                    EIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                // Next round
                round++;
                encryptedRound.SetActive(false);
                squares_random.SetActive(false);
                nextButton.SetActive(false);
                instructions.SetActive(false);
                hint.SetActive(false);
                generalQuestion.SetActive(true); // continue to the yes no questions

                break;

            case 3:

                if(trueFalse) // what I get back from OTPChoiceManager is either true or false
                {
                    GCorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    GIncorrect.GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                questions.SetActive(false);

                // Next scene
                GameControllerV2.Instance.scn_one_time_pad.SetActive(false);
                GameControllerV2.Instance.DisplayDecision();
                Destroy(this);

                break;
            
            default:
                Debug.Log("Default Case");
                break;
        }
    }

    public void GenerateNumbers()
    {
        encryptionAnswer = ""; // reseting the variable for each generated cycle

        for (int i = 0; i < 10; i++) // length 10
        {
            squaresT_random[i].text = "" + Random.Range(1, 26);
        }
    }

    public void setTrueFalse(bool x)
    {
        trueFalse = x;
    }
}