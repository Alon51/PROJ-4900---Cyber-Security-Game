using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Text text;

    public bool correct;
    public bool disable;

    public GoogleAnalyticsV4 googleAnalytics;

    private void Start()
    {
        //googleAnalytics.StartSession();
    }

    public void message() {

        googleAnalytics.LogScreen("Main Menu");

        //googleAnalytics.LogEvent("asfdasf", "asdfasd", "safsf", 33847);
        if (correct) {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            text.text = "That's correct!";

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);
        } else {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            GameObject.Find("scn_quiz_password").GetComponent<SceneControllerPassword>().DecreaseLife();
            text.text = "That's incorrect!";
        }

        disable = true;
    }

}
