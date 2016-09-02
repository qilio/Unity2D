using UnityEngine;
using System.Collections;

public class WeaponControl : MonoBehaviour {

	public AudioClip[] audioClip;
    public AudioSource source;
    public AudioClip gunChange;
	public WeaponScript[] weapons;
	public int weaponID = 0;

	public int SwitchWeaponUp(int rng, int dmg)
	{
		int wepIndex;
		weaponID++;
		if (weaponID > weapons.Length -1) 
		{
			weaponID = 0;
		}
		while (weapons [weaponID].getUnlocked(weapons [weaponID].gunName) == false) {
			weaponID++;
			if (weaponID > weapons.Length -1) 
			{
				weaponID = 0;
			}
		}
		source.PlayOneShot(gunChange, 1.0f);
		print ("Up " + weaponID);
		wepIndex = SwitchWeapon ();

		rng = weapons [weaponID].getRng(weapons [weaponID].gunName);
		dmg = weapons [weaponID].getDmg (weapons [weaponID].gunName);

		return wepIndex;
    }

	public int SwitchWeaponDown(int rng, int dmg)
	{
		int wepIndex;
		weaponID--;
		if (weaponID < 0) 
		{
			weaponID = weapons.Length -1;
		}
		while (weapons [weaponID].getUnlocked(weapons [weaponID].gunName) == false) {
			weaponID--;
			if (weaponID < 0) 
			{
				weaponID = weapons.Length -1;
			}
		}
        source.PlayOneShot(gunChange, 1.0f);
		print ("Down " + weaponID);
		SwitchWeapon ();
		wepIndex = SwitchWeapon ();

		rng = weapons [weaponID].getRng(weapons [weaponID].gunName);
		dmg = weapons [weaponID].getDmg (weapons [weaponID].gunName);

		return wepIndex;
    }

	public int SwitchWeapon()
	{
			
		Debug.Log (weaponID);

		if (weaponID == 0)
		{
			weapons [0].gameObject.SetActive (true);
			weapons [1].gameObject.SetActive (false);
			weapons [2].gameObject.SetActive (false);
			GetComponent<AudioSource> ().clip = audioClip [0];
			return 0;
		}
		if (weaponID == 1)
		{
			weapons [0].gameObject.SetActive (false);
			weapons [1].gameObject.SetActive (true);
			weapons [2].gameObject.SetActive (false);
			GetComponent<AudioSource> ().clip = audioClip [1];
			return 1;
		}
		if (weaponID == 2)
		{
			weapons [0].gameObject.SetActive (false);
			weapons [1].gameObject.SetActive (false);
			weapons [2].gameObject.SetActive (true);
			GetComponent<AudioSource> ().clip = audioClip [2];
			return 2;
		}
		return -1;
    }


}
