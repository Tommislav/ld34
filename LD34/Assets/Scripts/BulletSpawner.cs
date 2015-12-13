using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

	public int bulletType;
	public float fireRate = 0.1f;
	public float speedOverride = -1f;
	public bool friendly;


	private float cooldown = 0f;
	
	
	void Update () {
		if (cooldown > 0f) {
			cooldown -= Time.deltaTime;
		}	
	}

	void OnDrawGizmos() {
#if UNITY_EDITOR
		Transform p = transform.FindChild("p");
		Debug.DrawLine(transform.position, p.transform.position, Color.blue);
#endif
	}


	public void Fire() {
		if (CanFire()) {
			cooldown = fireRate;

			GameObject prefab = Game.Instance.bulletRegistry[bulletType];
			GameObject bullet = GameObject.Instantiate(prefab);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			Bullet b = bullet.GetComponent<Bullet>();
			b.friendly = friendly;
            if (speedOverride > 0f) {
				b.speed = speedOverride;
			}
		}
		
	}

	public bool CanFire() {
		return cooldown <= 0f;
	}
}
