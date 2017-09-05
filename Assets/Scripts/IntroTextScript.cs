using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTextScript : MonoBehaviour {

	public Text text;
	public string[] introStory1;
	public string[] introStory2;
	string introText1 = "You live a peaceful life Amarillo, until one day...  \nDuring your daily routine: \n\n ";
	string introText2 = "you stumble upon a rude gentleman. \n";
	string introText3 = "\n\nThis is the story of that incident.\n(Defend yourself from this foul gentleman\'s profanity!)\n";
	string completeIntro;
	int randomStory;

	// Use this for initialization
	void Start () {
		randomStory = Random.Range (0, introStory1.Length);
		introStory1 = new string[3];
		introStory1[0] = "Inside of the general store, which you\'ve visited to purchase a bottle of milk and a sack of flour ";
		introStory1[1] = "During an afternoon gala, which has been most successful, ";
		introStory1[2] = "In the Sheriff\'s office, after inquiring about the local littering policy, ";

		introStory2 = new string[3];
		introStory2[0] = "\nOn hearing you\'ve purchased the last bottle of milk he becomes quite belligerent!  ";
		introStory2[1] = "\nOn hearing your opinion on the benefits of free range cattle raising techniques over similar, but less effective cattle raising methods, he becomes quite incensed.  ";
		introStory2[2] = "\nOn hearing your inquiry concerning littering he becomes irritated!  'Littering does not concern you!,' he screams.  \nQuite interesting.  ";

		completeIntro = introText1 + introStory1[randomStory] + introText2 + introStory2[randomStory] + introText3;

		text.text = completeIntro;
	}
}
