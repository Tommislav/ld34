using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	private bool isShooting = true;
	
	void Update () {

		if (isShooting) {
			Transform t = Game.Instance.player;
			Vector3 p0 = transform.position;
			Vector3 p1 = t.position;

			//float angle = Vector2.Angle(transform.position, t.position);
			float angle = Mathf.Atan2(p0.y - p1.y, p0.x - p1.x) / Mathf.PI * 180 - 90;

			transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
		}
	}

	private void OnDied(bool isDead) {
		if (isDead) {
			isShooting = false;
		}
	}
}
