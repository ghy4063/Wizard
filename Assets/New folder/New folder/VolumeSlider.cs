using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

	public Slider volumeSlider;

	public void VolumeController(){
		AudioListener.volume = volumeSlider.value;
	}
}
