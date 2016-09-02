using UnityEngine;
using System.Collections;

public class WeaponScript: MonoBehaviour  {

	public static bool pistolUnlocked = true;
	public static bool rifleUnlocked = false;
	public static bool shotgunUnlocked = false;
	public static int rifleCost = 25;
	public static int shotgunCost = 40;
	public string gunName;
	public UserInput player;
	public static int pRange = 2;
	public static int pDmg = 1;
	public static int rRange = 3;
	public static int rDmg = 1;
	public static int sRange = 4;
	public static int sDmg = 1;

	public bool getUnlocked(string name){
		if (name == "pistol") {
			return pistolUnlocked;
		} else if (name == "rifle") {
			return rifleUnlocked;
		} else if (name == "shotgun") {
			return shotgunUnlocked;
		} else {
			return false;
		}
	}

	public void setUnlock(string name){
		if (name == "pistol") {
			return;
		} else if (name == "rifle") {
			rifleUnlocked = true;
		} else if (name == "shotgun") {
			shotgunUnlocked = true;
		} else {
			return;
		}
		print (pistolUnlocked + " " + rifleUnlocked + " " + shotgunUnlocked);
	}

	public int getCost(string name){
		 if (name == "rifle") {
			return rifleCost;
		} else if (name == "shotgun") {
			return shotgunCost;
		} else {
			return 0;
		}

	}

	public int getPrice(string gun){
		if (gun == "rifle") {
			return rifleCost;
		}
		if (gun == "shotgun") {
			return shotgunCost;
		}
		return 0;
	}

	public int getRng(string gun){
		if (gun == "pistol") {
			return pRange;
		}
		if (gun == "rifle") {
			return rRange;
		}
		if (gun == "shotgun") {
			return sRange;
		}
		return 0;
	}

	public int getDmg(string gun){
		if (gun == "pistol") {
			return pDmg;
		}
		if (gun == "rifle") {
			return rDmg;
		}
		if (gun == "shotgun") {
			return sDmg;
		}
		return 0;
	}

	public void incGunRange(string gun){
		if (gun == "pistol") {
			pRange += 1;
		}
		if (gun == "rifle") {
			rRange += 1;
		}
		if (gun == "shotgun") {
			sRange += 1;
		}
	}

	public void incGunDmg(string gun){
		if (gun == "pistol") {
			pDmg += 1;
		}
		if (gun == "rifle") {
			rDmg += 1;
		}
		if (gun == "shotgun") {
			sDmg += 1;
		}
	}
}
