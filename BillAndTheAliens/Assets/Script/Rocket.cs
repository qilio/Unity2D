using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour 

{
	public bool right;
	public bool left;
	public GameObject player;
	public GameObject explosion;
	public GameObject boss;

	private float health = 1;
	private Rigidbody2D rigidbody2D;
	private Vector2 target;
	private float speed = 2;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		rigidbody2D = GetComponent<Rigidbody2D> ();
		Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Destroy (this.gameObject, 15f);
	}

	void Update()
	{
		target = new Vector2 (player.transform.position.x, player.transform.position.y);
		if (player.transform.position.y + .3f > transform.position.y) {
			transform.position = Vector2.MoveTowards (transform.position, (target + new Vector2(0, .7f)), speed * Time.deltaTime);
		} else if (player.transform.position.y - .3f < transform.position.y) {
			transform.position = Vector2.MoveTowards (transform.position, (target - new Vector2(0, .7f)), speed * Time.deltaTime);
		} else {
			transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log ("dead");
		if (other.gameObject.tag == "Player") 
		{		
			player.GetComponent<UserInput> ().ApplyDamage (1);
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}

	public void ApplyDamage(float damage)
	{
		health -= damage;
		if (health <= 0) 
		{
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}

	}
}
