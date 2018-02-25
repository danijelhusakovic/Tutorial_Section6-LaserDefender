using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource audioSource;
	
	void Awake () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			audioSource = GetComponent<AudioSource>();
			//default music when the game starts. later it will change in OnLevelWasLoaded as intended
			audioSource.clip = startClip;
			audioSource.loop = true;
			audioSource.Play();
		}
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log("Loaded level #" + level);
		audioSource.Stop();
		if(level == 0){
			audioSource.clip = startClip;	
		} else if (level == 1){
			audioSource.clip = gameClip;
		} else if (level == 2) {
			audioSource.clip = endClip;
		}
		audioSource.loop = true;
		audioSource.Play();
	}
	
	void Start () {
		
	}
}
