using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
	public GameObject player;
	public GameManager gameManager;
	public Text killedCount;
	public Image health;
	public Text coinCount;
	public GameObject winnerText;
	public GameObject loseText;
	public GameObject pause;
	public GameObject controlls;
	public GameObject[] allEnemies;
	public int currLevel;
	public int nextLevel;
	public GameObject nxtLvl;
	public GameObject shop;
	public GameObject[] gunHud;
	public Text ammoCount;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}

	void Update()
	{
		killedCount.text = gameManager.currentKilledAliens + "/" + gameManager.aliensHaveToKill;
		coinCount.text = player.GetComponent<UserInput> ().getGold ().ToString();
		health.fillAmount = player.GetComponent<UserInput> ().curHealth/player.GetComponent<UserInput> ().maxHealth;

		if (gameManager.winner) 
		{
//			winnerText.SetActive (true);
			nxtLvl.SetActive (true);
			shop.SetActive (true);
//			waitWinner ();
//			winnerText.SetActive (false);
		}

		if (player.GetComponent<UserInput> ().dead) 
		{
			loseText.SetActive (true);
			nxtLvl.SetActive (false);
			shop.SetActive (false);
			allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < allEnemies.Length; i++) {
				Destroy (allEnemies [i]);
			}
		}
	}

	public void nextLvl()
	{
		SceneManager.LoadScene (nextLevel, LoadSceneMode.Single);
	}

	public void pauseMen(){
		if(!pause.activeSelf){
			pause.SetActive (true);
			return;
		}
		pause.SetActive (false);
	}

	IEnumerator waitWinner()
	{
		yield return new WaitForSeconds (5f);
	}

	public int getCurrlvl(){
		return currLevel;
	}

	public int getNextlvl(){
		return nextLevel;
	}

	public void setControllsActive(){
		pause.SetActive (false);
		controlls.SetActive (true);
		player.GetComponent<UserInput> ().setInControlls (true);
	}

	public void exitControlls(){
		controlls.SetActive (false);
		pause.SetActive (true);
		player.GetComponent<UserInput> ().setInControlls (false);
	}

	public void weaponSwitch(int num){
		if (num == 0) {
			gunHud [0].gameObject.SetActive (true);
			gunHud [1].gameObject.SetActive (false);
			gunHud [2].gameObject.SetActive (false);
			ammoCount.text = player.GetComponent<UserInput> ().getAmmo (num).ToString ();
		}else if (num == 1) {
			gunHud [0].gameObject.SetActive (false);
			gunHud [1].gameObject.SetActive (true);
			gunHud [2].gameObject.SetActive (false);
			ammoCount.text = player.GetComponent<UserInput> ().getAmmo (num).ToString ();
		}else if (num == 2) {
			gunHud [0].gameObject.SetActive (false);
			gunHud [1].gameObject.SetActive (false);
			gunHud [2].gameObject.SetActive (true);
			ammoCount.text = player.GetComponent<UserInput> ().getAmmo (num).ToString ();
		}
	}
}
