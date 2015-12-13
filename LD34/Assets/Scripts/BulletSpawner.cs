using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

	public int bulletType;

	public float reloadTimeMin = 0.25f;
	public float reloadTimeMax = 0.3f;
	
	public float speedOverride = -1f;


	public bool friendly;


	private float reloading = 0f;
	private Color _bulletColor;
	
	public void SetIsFriendly(bool isFriendly) {
		this.friendly = isFriendly;
		_bulletColor = isFriendly ? Game.Instance.playerColor : getParentColor();
	}

	
	private Color getParentColor() {
		return transform.parent.gameObject.GetComponent<Colorize>().color;
	}

	void Start() {
		SetIsFriendly(friendly);
	}

	void Update () {
		
		if (reloading > 0f) {
			reloading -= Time.deltaTime;
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
			reloading = Random.Range(reloadTimeMin, reloadTimeMax);

			GameObject prefab = Game.Instance.bulletRegistry[bulletType];
			GameObject bullet = GameObject.Instantiate(prefab);
			bullet.transform.position = transform.position;
			bullet.transform.rotation = transform.rotation;
			Bullet b = bullet.GetComponent<Bullet>();
			bullet.GetComponent<Colorize>().SetColor(_bulletColor);
			b.friendly = friendly;
            if (speedOverride > 0f) {
				b.speed = speedOverride;
			}
		}
		
	}

	public bool CanFire() {
		return reloading <= 0f;
	}
}
