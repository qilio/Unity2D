using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

	public GameObject controlls;

	public void StartGame()
	{
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
	public void Exit()
	{
		Application.Quit ();
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;

	}

	public void MainMenu()
	{
		OnEnter.setCount (1);
		SceneManager.LoadScene (0, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public static void EnterShop(){
		
		SceneManager.LoadScene (6, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public static void ExitShop(int curlevel){
		SceneManager.LoadScene (curlevel, LoadSceneMode.Single);
		Time.timeScale = 1;
	}

	public void setControllsActive(){
		controlls.SetActive (true);
	}

	public void exitControlls(){
		controlls.SetActive (false);
	}
}
