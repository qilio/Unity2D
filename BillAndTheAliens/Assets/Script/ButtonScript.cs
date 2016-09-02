using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : Button {

	public void rifleButton(){
		if(WeaponScript.rifleUnlocked == true){
			this.interactable = false;
		}
	}

	public void shotgunButton(){
		if(WeaponScript.shotgunUnlocked == true){
			this.interactable = false;
		}
	}
}
