using UnityEngine;
using System.Collections;

public class SpawnPointAnim : MonoBehaviour {

	private Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
	}

	public void teleportAnim(){
		anim.SetTrigger ("teleport");
	}

}
