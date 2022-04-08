using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public static bool isNewGame = false;

	public void New(){
		SceneManager.LoadScene ("Level1");
		isNewGame = true;
		Damage.addpoints = 0;
	}

	public void Load(){
		string levelName = PlayerPrefs.GetString ("LevelSave");
		SceneManager.LoadScene (levelName);
	}

	public void Quit(){
		Application.Quit ();
	}
}
