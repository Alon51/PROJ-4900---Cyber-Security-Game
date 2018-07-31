using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneTimePadQuizManager : MonoBehaviour {

    public int round = 1;

    // Answers given for questions
    public InputField decryptedAnswer;
    public InputField encryptedAnswer;
    public InputField bonusAnswer;

    // 3 rounds - decryption, encryption, bonus
    public GameObject decryptedRound;
    public GameObject encryptedRound;
    public GameObject bonusRound;

    // continue button
    public GameObject nextButton;

    // starts with decryption question
    void Start()
    {
        decryptedRound.SetActive(true);
        encryptedRound.SetActive(false);
        bonusRound.SetActive(false);
    }

    // proceed to next question
    public void NextSet()
    {

        switch (round)
        {
            case 1:
                // removes potential capitalization mistakes
                if (decryptedAnswer.textComponent.text.ToLower() == "hello world")
                {

                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    GameObject.Find("DCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // Next round
                    round++;
                    decryptedRound.SetActive(false);
                    encryptedRound.SetActive(true);
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("DIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // Next round
                    round++;
                    decryptedRound.SetActive(false);
                    encryptedRound.SetActive(true);
                }
                break;
            case 2:
                // removes potential capitalization mistakes
                if (encryptedAnswer.textComponent.text.ToLower() == "xvywx rsfshc")
                {

                    Debug.Log("Entering encryption round");

                    // play beep sound
                    GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                    // increase NP
                    GameControllerV2.Instance.IncreaseNP(5);

                    // Feedback for correct answer
                    // Feedback for correct answer
                    GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // Next round
                    round++;
                    encryptedRound.SetActive(false);
                    bonusRound.SetActive(true);
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // Next round
                    round++;
                    decryptedRound.SetActive(false);
                    bonusRound.SetActive(true);
                }
                break;
            case 3:
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

                    // deactivate quiz, and display results
                    GameControllerV2.Instance.scn_caesar_cipher.SetActive(false);

                    GameControllerV2.Instance.DisplayDecision();

                    // don't need script after this
                    Destroy(this);
                }
                else
                {
                    // Decrease NP
                    GameControllerV2.Instance.DecreaseNP(5);

                    // Feedback for incorrect answer
                    GameObject.Find("BIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                    // Next scene
                    GameControllerV2.Instance.scn_caesar_cipher.SetActive(false);
                    GameControllerV2.Instance.DisplayDecision();
                    Destroy(this);
                }
                break;
            default:
                Debug.Log("Default Case");
                break;

        }
    }
}
