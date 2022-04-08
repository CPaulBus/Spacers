using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Damage : MonoBehaviour {
	public int health = 5;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	float invulnAnimTimer=0f;
	public static int enemyCnt = 0;

	private int playerPoints;
	public static int addpoints=0;

	SpriteRenderer spriteRend;
	Scene scene;

	public GameObject canvas;

	void OnGUI(){
		GUI.Label (new Rect(70.0f,23.0f,70,70),"Lives: " + health + "  Score: " + addpoints + "  Enemy Count: " + enemyCnt);
	}

	void Start(){
		correctLayer = gameObject.layer;
		spriteRend = GetComponent<SpriteRenderer> ();
		if (spriteRend == null) {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer> ();
			if (spriteRend == null) {
				Debug.LogError ("Object '" + gameObject.name + "' has no sprite renderer.");
			}
		}
		if (PlayerPrefs.GetInt ("Score") > 1 && !MainMenu.isNewGame) {
			playerPoints = PlayerPrefs.GetInt ("Score");
			MainMenu.isNewGame = false;
		} else {
			playerPoints = 0;
		}

		if (PlayerPrefs.GetInt ("Health") > 1) {
			health = PlayerPrefs.GetInt ("Health");
		} else {
			health = 5;
		}
		scene = SceneManager.GetActiveScene ();
		Time.timeScale = 1;
		enemyCnt = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		canvas.SetActive (false);
	}

	void OnTriggerEnter2D(){
			health--;
		if (invulnPeriod > 0) {
			invulnTimer = invulnPeriod;
			gameObject.layer = 10;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			PlayerPrefs.SetString ("LevelSave", Application.loadedLevelName);
			PlayerPrefs.SetInt ("Health", health);
			PlayerPrefs.SetInt ("Score", playerPoints);
			SceneManager.LoadScene ("MainMenu");
		}

		if (invulnTimer > 0) {
			invulnTimer -= Time.deltaTime;
			if (invulnTimer <= 0) {
				gameObject.layer = correctLayer;
				if (spriteRend != null) {
					spriteRend.enabled = true;
				}
			} 
			else {
				if (spriteRend != null) {
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}

		if (health <= 0)
			Die ();

		WinLose ();
	}

	void Die(){
		Destroy (gameObject);
	}

	void WinLose(){
		if (health == 0) {
			Application.LoadLevel ("GameOver");
		}
		if ((GameObject.FindGameObjectsWithTag ("Enemy")).Length == 0) {
			Time.timeScale = 0;
			canvas.SetActive (true);
			// check the current level
			if (scene.name == "Level1" && Input.GetKeyDown (KeyCode.Q)) {
				SceneManager.LoadScene("Level2");
			} 
			else if (scene.name== "Level2" && Input.GetKeyDown (KeyCode.Q)) {
				SceneManager.LoadScene("Level3");
			}
			else if (scene.name== "Level3" && Input.GetKeyDown (KeyCode.Q)) {
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}
