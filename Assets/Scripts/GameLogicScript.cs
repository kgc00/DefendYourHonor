using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour {

	public int insultsReceived = 0;
	public Slider slider;
	public GameObject insultGameObject;
	public GameObject insultTextTarget;
	public GameObject responsePanel;
	public GameObject dictionaryObject;
	public GameObject finisherPanelObject;
	public GameObject tutorialPanel;
	public GameObject oldGuy;
	public Sprite oldGuyHitSprite;
	public Sprite oldGuyNormalSprite;
	public Font useThisFont;
	int timesInstantiated = 0;
	int randomPositionX = 0;
	int randomPositionY = 0;
	string[] insultArray;
	string[] definitionArray;
	int insultUsed;
	bool startingInsult = true;
	bool finisherPanel = false;
	bool resetSprite = false;
	float timer = .75f;
	float typingTimer = .5f;


	void Start(){
		if(insultGameObject.activeInHierarchy==true){
			insultGameObject.SetActive (false);
		}
		FindObjectOfType<SoundManagerScript> ().SwitchTunes();
		definitionArray = new string[10];
		definitionArray [0] = "noun: nincompoop\n\na foolish or stupid person.!";
		definitionArray [1] = "noun: philanderer\n\na man who readily or frequently enters into casual sexual relationships with women; a womanizer.";
		definitionArray [2] = "adjective: gaseous\n\nrelating to or having the characteristics of a gas. \n\nnoun: fiend\n\nan evil spirit or demon.";
		definitionArray [3] = "noun: dolt\n\na stupid person.";
		definitionArray [4] = "adjective, derogatory: puritanical \n\npracticing or affecting strict religious or moral behavior.";
		definitionArray [5] = "adjective: vile\n\nextremely unpleasant.\n\nnoun: creature\n\nan animal, as distinct from a human being.";
		definitionArray [6] = "noun: boor\n\nan unrefined, ill-mannered person.";
		definitionArray [7] = "noun: fool\n\na person who acts unwisely or imprudently; a silly person.";
		definitionArray [8] = "noun: snake oil salesman\n\nsomeone who knowingly sells fraudulent goods or who is a fraud, quack, or charlatan.";
		definitionArray [9] = "noun: sophist\n\na person who reasons with clever but fallacious arguments.";
		insultArray = new string[10];
		insultArray [0] = "Nincompoop!";
		insultArray [1] = "Philanderer!";
		insultArray [2] = "Gaseous Fiend!";
		insultArray [3] = "Dolt!";
		insultArray [4] = "Puritanical!";
		insultArray [5] = "Vile Creature!";
		insultArray [6] = "Boor!";
		insultArray [7] = "Fool!";
		insultArray [8] = "Snake Oil Salesman!";
		insultArray [9] = "Sophist!";
		StartCoroutine(FirstInsult());
	}

	IEnumerator FirstInsult()
	{
		yield return new WaitForSeconds(.5f);
		InitiateInsult ();
	}

	public void InitiateInsult()
	{
		insultUsed = Random.Range (0, insultArray.Length);
		randomPositionX = Random.Range (-50, 50);
		randomPositionY = Random.Range (-75, 75);
		timesInstantiated += 1;
		GameObject instantiatedInsultObject = Instantiate(insultGameObject, 
			new Vector3(insultTextTarget.transform.position.x + (randomPositionX), insultTextTarget.transform.position.y + (randomPositionY), 
				insultTextTarget.transform.position.z + (10 * timesInstantiated)),
			Quaternion.identity, insultTextTarget.transform);
		instantiatedInsultObject.GetComponentInChildren<Text> ().text = insultArray[insultUsed];
		instantiatedInsultObject.GetComponentInChildren<Text> ().font = useThisFont;
		instantiatedInsultObject.GetComponentInChildren<Text> ().fontSize = 38;
		//insultArray [insultUsed].Remove (insultUsed);
		if (instantiatedInsultObject.activeInHierarchy == false) {
			instantiatedInsultObject.SetActive (true);
		}
		dictionaryObject.GetComponent<Text> ().text = definitionArray [insultUsed];
		dictionaryObject.GetComponent<Text> ().font = useThisFont;
		dictionaryObject.GetComponent<Text> ().fontSize = 24;
		FindObjectOfType<SoundManagerScript> ().PlayHisInsultSound();
		FillBar();
	}

	public static void Remove<T>(ref T[] arr, int index)
	{
		Debug.Log ("Some Logics ");	
	}

	// Update is called once per frame
	void Update () {
		if (!finisherPanel) {
			responsePanel.GetComponent<Text> ().text += Input.inputString;
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				responsePanel.GetComponent<Text> ().text = null;
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
				ResolveInsult (responsePanel.GetComponent<Text> ().text);
				responsePanel.GetComponent<Text> ().text = null;
			}
		} else {
			finisherPanelObject.GetComponent<Text> ().text += Input.inputString;
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				finisherPanelObject.GetComponent<Text> ().text = null;
			}
			if (Input.GetKeyDown (KeyCode.Return)) {
				ResolveInsult (finisherPanelObject.GetComponent<Text> ().text);
				finisherPanelObject.GetComponent<Text> ().text = null;
			}
		}

		if(resetSprite){
			if (timer <= 0f) {
				oldGuy.GetComponent<SpriteRenderer> ().sprite = oldGuyNormalSprite;
				resetSprite = false;
				InitiateInsult ();
			} else if (timer > 0f){
				timer -= Time.deltaTime;
			}
		}

		if (Input.anyKey && !Input.GetKey(KeyCode.Return)) {
			typingTimer = .5f;
			FindObjectOfType<SoundManagerScript> ().finishedTyping = false;
		} else {
			typingTimer -= Time.deltaTime;
			FindObjectOfType<SoundManagerScript> ().finishedTyping = false;
			if(typingTimer <= 0f){
				FindObjectOfType<SoundManagerScript> ().finishedTyping = true;
				FindObjectOfType<SoundManagerScript> ().onlyTypeOnce = true;
				if (FindObjectOfType<SoundManagerScript> ().stopTypingOnce == true){
					FindObjectOfType<SoundManagerScript> ().StopTyping();
					FindObjectOfType<SoundManagerScript> ().stopTypingOnce = false;
				}

			}
		}
	}

	public void FillBar(){
		if(!startingInsult){
			if (slider.value <= 5) {
				insultsReceived++;
				slider.value = slider.value + 1;
				if (slider.value == 5) {
					BarMaxed ();
				}
			} else {
				//Debug.Log ("Can't Increase Value" + slider.value);
				return;
			}
		} 
		startingInsult = false;
	}

	void ResolveInsult(string responseToInsult){
		int insultLength = responseToInsult.Length;
		//print (insultLength);
		if (!finisherPanel) {
			if (responseToInsult.Length >= 3 && responseToInsult.Contains ("a") || responseToInsult.Contains ("o")
				|| responseToInsult.Contains ("e") || responseToInsult.Contains ("u") || responseToInsult.Contains ("i") 
				|| responseToInsult.Contains ("A") || responseToInsult.Contains ("O")
				|| responseToInsult.Contains ("E") || responseToInsult.Contains ("U") || responseToInsult.Contains ("I")) {
				//Debug.Log (responseToInsult);	
				if(slider.value != 5){
//					InitiateInsult ();
					timer = .75f;
					oldGuy.GetComponent<SpriteRenderer> ().sprite = oldGuyHitSprite;
					resetSprite = true;
					FindObjectOfType<SoundManagerScript> ().PlayOurInsultSound();
				}
			}
		} else {
			if (responseToInsult.Length >= 3 && responseToInsult.Contains ("a") || responseToInsult.Contains ("o")
				|| responseToInsult.Contains ("e") || responseToInsult.Contains ("u") || responseToInsult.Contains ("i") 
				|| responseToInsult.Contains ("A") || responseToInsult.Contains ("O")
				|| responseToInsult.Contains ("E") || responseToInsult.Contains ("U") || responseToInsult.Contains ("I")) {
				//Debug.Log (responseToInsult);
				FindObjectOfType<SoundManagerScript> ().PlayOurInsultSound();
				FindObjectOfType<SoundManagerScript> ().PlayOurWinSound();
				Win ();
			}
		}

	}

	void BarMaxed(){
		//do logic
		//Debug.Log("BarMaxed");
		finisherPanel = true;
		if (!finisherPanelObject.activeInHierarchy){
			finisherPanelObject.gameObject.transform.parent.gameObject.SetActive (true);
			responsePanel.transform.parent.gameObject.SetActive (false);
			dictionaryObject.GetComponent<Text> ().text = "!!!!";
		}
	}

	void Win(){
		GetComponent<LevelManagerScript> ().LoadLevel ("EndScreen");
	}

	void TutorialLogic(){
		if (!tutorialPanel.activeInHierarchy) {
			tutorialPanel.SetActive (true);
		}
	}
}
