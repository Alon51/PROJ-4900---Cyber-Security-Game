using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    public Slider musicSlider;

	public void MusicSlider() {
        AudioListener.volume = musicSlider.value;
    }
}
