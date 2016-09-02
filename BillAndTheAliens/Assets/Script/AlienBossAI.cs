using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlienBossAI : MonoBehaviour
{

	public GameObject player;
	public float maxHealth;
	public float health;
	public Image healthHUD;
	public GameObject explosion;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		health = maxHealth;
	}

	void Update()
	{
		healthHUD.fillAmount = health / maxHealth;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("dead");
		if (other.gameObject.tag == "Player") 
		{		
			player.GetComponent<UserInput> ().ApplyDamage (1);
		}
	}

	public void ApplyDamage(float damage)
	{
		health -= damage;
		if (health <= 0) 
		{
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
			OnEnter.setCount (2);
			SceneManager.LoadScene (0, LoadSceneMode.Single);
		}

	}
}
