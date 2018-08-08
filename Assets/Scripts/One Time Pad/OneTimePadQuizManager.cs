using UnityEngine;
using UnityEngine.UI;

public class OneTimePadQuizManager : MonoBehaviour {

    public byte round = 1;

    // Answers given for questions
    public InputField decryptedAnswer;
    public InputField encryptedAnswer;
    //public InputField encryptedAnswer_2;
    public InputField bonusAnswer;

    // 3 rounds - decryption, encryption, bonus
    public GameObject decryptedRound;
    public GameObject encryptedRound;
    //public GameObject encryptedRound_2;
    public GameObject bonusRound;

    // continue button
    public GameObject nextButton;

    public GameObject button_menue;// the button in the main scene that allow you to start over the game

    public GameObject squares_random; // To move them as a group, located on top of the window - random
    public Text[] squaresT_random;

    public Text textToChange;

    private string encryptionAnswer; // here I will save the result for question 1 (encrypt a sentence)
    private byte[] otpTheBest = { 15, 20, 16, 20, 8, 5, 2, 5, 19, 20 }; // Saving the values of each letter
    private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // starts with decryption question
    void Start()
    {
        encryptedRound.SetActive(true);
        decryptedRound.SetActive(false);
        bonusRound.SetActive(false);
        button_menue.SetActive(false);// no need for this button in the quiz
        squares_random.SetActive(true);
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
                if (encryptedAnswer.textComponent.text.ToLower() == encryptionAnswer.ToLower()) //"xvywx rsfshc")
                {
                    Debug.Log("Entering encryption round");
                    
                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
                    
                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);
                    
                    // Feedback for incorrect answer
                    GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                // Next round
                round++;

                textToChange.text = "\"Its the same\"";

                for (int i = 0; i < 10; i++) // reset the random generator
                {
                    squaresT_random[i].text = "0";
                }

                encryptedAnswer.text = "";
                //encryptedRound_1.SetActive(false);
                //encryptedRound_2.SetActive(true);
                //decryptedRound.SetActive(true);

                break;

            case 2:
                
                //The sentence "otp the best" has 10 letters equivalent to the values: 15 20 16 20 8 5 2 5 19 20:
                for (int i = 0; i < 10; i++)
                {
                    if (i == 3 || i == 6)
                        encryptionAnswer += " ";

                    //Check to see if I get 26 % 26 which will give 0 and 0 - 1 is array out of range
                    if (((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) == 0)
                        encryptionAnswer += letters[25];// add the last letter which is Z
                    else
                        encryptionAnswer += letters[((int.Parse(squaresT_random[i].text) + otpTheBest[i]) % 26) - 1];
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
                    GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                // Next round
                round++;
                encryptedRound.SetActive(false);
                //encryptedRound_2.SetActive(true);
                //decryptedRound.SetActive(true);

                break;

            case 3:
                
                // removes potential capitalization mistakes
                if (decryptedAnswer.textComponent.text.ToLower() == "hello world")
                {

                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    GameObject.Find("DCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("DIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }

                // Next round
                round++;
                decryptedRound.SetActive(false);
                bonusRound.SetActive(true);

                break;
            case 4:
                
                // removes potential capitalization mistakes
                if (bonusAnswer.textComponent.text.ToLower() == "the boss is coming")
                {
                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(25);

                    // Feedback for correct answer
                    GameObject.Find("BCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // glitch screen
                    GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("BIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                }

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
            squaresT_random[i].text = "" + UnityEngine.Random.Range(1, 26);
        }
    }
}