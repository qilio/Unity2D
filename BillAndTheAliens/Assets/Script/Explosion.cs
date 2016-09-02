using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public float time = 0.25f;
	void Awake () 
	{
		Destroy (this.gameObject, time);
	}
}
