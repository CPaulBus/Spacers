using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform player;
	float rotSpeed = 180f;
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			GameObject go = GameObject.Find ("Player");

			if (go != null) {
				player = go.transform;
			}
		}

		if (player == null) {
			return;
		}

		Vector3 dir = player.position - transform.position;
		dir.Normalize ();

		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
		transform.rotation=Quaternion.RotateTowards (transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}
}
