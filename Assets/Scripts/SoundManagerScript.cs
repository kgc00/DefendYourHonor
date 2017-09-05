using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{

    public static SoundManagerScript instance = null;
	AudioSource myAudioSource;
	AudioSource newAudioSourceTyping = null;
	public AudioClip introTunes;
	public AudioClip mainTunes;
	public AudioClip typeSound;
	public AudioClip hisInsultSound;
	public AudioClip ourInsultSound;
	public bool finishedTyping = false;
	public bool onlyTypeOnce;
	public bool stopTypingOnce = true;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
			myAudioSource = this.gameObject.AddComponent<AudioSource> ();
			myAudioSource.clip = introTunes;
			myAudioSource.volume = .35f;
			myAudioSource.Play ();
			myAudioSource.loop = true;
			DontDestroyOnLoad (this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	void Update(){
		if(!finishedTyping && onlyTypeOnce){
			onlyTypeOnce = false;
			PlayOurTypingSound ();
			stopTypingOnce = true;
		}
	}

	public void SwitchTunes(){
		if (myAudioSource.clip == introTunes) {
			myAudioSource.clip = mainTunes;
			myAudioSource.Play ();
			myAudioSource.loop = true;
		} else {
			myAudioSource.clip = introTunes;
			myAudioSource.Play ();
			myAudioSource.loop = true;
		}
	}

	public void PlayHisInsultSound(){
		AudioSource newAudioSource = this.gameObject.AddComponent<AudioSource> ();
		newAudioSource.clip = hisInsultSound;
		newAudioSource.volume = .3f;
		newAudioSource.Play ();
		Destroy (newAudioSource, 2f);
	}

	public void PlayOurInsultSound(){
		AudioSource newAudioSourceUs = this.gameObject.AddComponent<AudioSource> ();
		newAudioSourceUs.clip = ourInsultSound;
		newAudioSourceUs.volume = 1f;
		newAudioSourceUs.Play ();
		Destroy (newAudioSourceUs, 2f);
	}

	public void PlayOurTypingSound(){
		if (newAudioSourceTyping == null) {
			newAudioSourceTyping = this.gameObject.AddComponent<AudioSource> ();
			newAudioSourceTyping.clip = typeSound;
			newAudioSourceTyping.volume = 1f;
			newAudioSourceTyping.time = 1.2f;
			newAudioSourceTyping.Play ();
		}
	}

	public void StopTyping(){
		Destroy (newAudioSourceTyping);
		newAudioSourceTyping = null;
	}

	public void PlayOurWinSound(){
		if (newAudioSourceTyping == null) {
			newAudioSourceTyping = this.gameObject.AddComponent<AudioSource> ();
			newAudioSourceTyping.clip = typeSound;
			newAudioSourceTyping.volume = 1f;
			newAudioSourceTyping.time = 27f;
			newAudioSourceTyping.Play ();
			StartCoroutine(StopWinTyping());
		} else if (newAudioSourceTyping != null){
			Destroy (newAudioSourceTyping);
			newAudioSourceTyping = null;
			PlayOurWinSound ();
		}
	}

	IEnumerator StopWinTyping()
	{
		yield return new WaitForSeconds(1f);
		Destroy (newAudioSourceTyping);
		newAudioSourceTyping = null;
	}
}

