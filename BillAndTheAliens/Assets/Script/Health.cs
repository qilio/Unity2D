using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float healthAmount;

	private GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{		
			player.GetComponent<UserInput> ().ApplyDamage (-healthAmount);

			Destroy (this.gameObject);
		}
	}
}
