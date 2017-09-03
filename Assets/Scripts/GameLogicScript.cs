using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour {

	public int insultsReceived = 0;
	public Slider slider;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
			FillBar();
		}
	}

	public void FillBar(){
		if (slider.value <= 5) {
			insultsReceived++;
			slider.value = slider.value + 1;
			Debug.Log (slider.value);
		} else {
			Debug.Log ("Can't Increase Value" + slider.value);
		}
	}
}
