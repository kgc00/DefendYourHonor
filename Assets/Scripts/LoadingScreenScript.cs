using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenScript : MonoBehaviour {

	public int level = 0;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			if(level == 1){
				FindObjectOfType<LevelManagerScript> ().LoadLevel ("IntroScene");
			} else if (level == 2){
				FindObjectOfType<LevelManagerScript> ().LoadLevel ("MainScene");
			} else if (level == 3){
				FindObjectOfType<LevelManagerScript> ().LoadLevel ("IntroScene");
			}
		}
	}
}
