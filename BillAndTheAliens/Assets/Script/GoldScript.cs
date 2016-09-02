using UnityEngine;
using System.Collections;

public class GoldScript : MonoBehaviour {

	public AudioClip bling;

	private int goldVal = 1;
	private GameObject player;
	private AudioSource source;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{		
			player.GetComponent<UserInput> ().gainMoney (goldVal);
			source.PlayOneShot(bling, 1f);
			Destroy (this.gameObject);
		}
	}
}
