using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	public BulletSpawner spawner;
	public float intervalMin = 1f;
	public float intervalMax = 2f;

	private float countDown;
	private bool isShooting;


	void Start() {
		NewInterval();
		isShooting = true;
	}
	
	void Update () {
		if (spawner == null) { return; }

		if (isShooting) {
			countDown -= Time.deltaTime;
			if (countDown <= 0 && spawner.CanFire()) {
				spawner.Fire();
				NewInterval();
			}
		}
	}

	private void OnDied(bool isDead) {
		if (isDead) {
			Destroy(this);
		}
	}

	private void NewInterval() {
		countDown = Random.Range(intervalMin, intervalMax);
	}
}
