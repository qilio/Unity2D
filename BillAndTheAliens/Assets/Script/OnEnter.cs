using UnityEngine;
using System.Collections;

public class OnEnter : MonoBehaviour {

	public AudioClip levelSong;

	public MovieTexture win;
	public MovieTexture lose;

	private static MovieTexture currVid;
	private static int count = 0;

	private AudioSource audioSource;

	void Awake(){
		if (count == 1) {
			currVid = lose;
		}
		if (count == 2) {
			currVid = win;
		}
		audioSource = GetComponent<AudioSource> ();
		StartCoroutine(playVid ());
	}

	public IEnumerator playVid(){
		if (currVid != null) {
			audioSource.clip = currVid.audioClip;
			audioSource.loop = false;
			audioSource.clip = currVid.audioClip;
			audioSource.Play();
			currVid.Play ();
			yield return new WaitForSeconds(currVid.audioClip.length);
			audioSource.clip = levelSong;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	void OnGUI(){
		if (currVid != null && currVid.isPlaying) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), currVid);
		}
	}

	public static void setCount(int num){
		count = num;
	}
}
