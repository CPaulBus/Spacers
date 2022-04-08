using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
	public Vector3 bulletOffset = new Vector3 (0, 0.5f, 0);
	public GameObject bulletPrefab;
	public float fireDelay = 0.5f;
	float cooldownTimer=0;
	int bulletLayer;

	void Start(){
		bulletLayer = gameObject.layer;
	}

	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		if (cooldownTimer<=0) {
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * new Vector3 (0, 0.5f, 0);
			GameObject bulletGO = (GameObject)Instantiate (bulletPrefab,transform.position+offset,transform.rotation);
			bulletGO.layer = bulletLayer;
		}
	}
}
