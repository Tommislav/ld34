using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	[HideInInspector]
	public Quaternion rotation = Quaternion.identity;
	public float speed = 0.05f;
	public bool friendly;
	private Vector3 move;

	void Start () {
		if (rotation != Quaternion.identity) {
			transform.rotation = rotation;
		}
		
		move = Vector3.Normalize(transform.right) * speed;
	}
	
	void Update () {
		transform.position += (move * Time.deltaTime);

		if (OutOfBounds()) {
			GameObject.Destroy(gameObject);
		}
	}

	private bool OutOfBounds() {
		Vector3 p = transform.position;
		Rect b = Game.Instance.bounds;
		return !b.Contains(p);
	}

	private void OnTriggerEnter2D(Collider2D coll) {
		
		int playerLayer = Game.Instance.playerLayer;
		int enemyLayer = Game.Instance.enemyLayer;
		int layer = friendly ? enemyLayer : playerLayer;

		if (coll.gameObject.layer == layer) {
			coll.gameObject.SendMessage("OnHitByBullet", this, SendMessageOptions.DontRequireReceiver);
			GameObject.Destroy(gameObject);
		}

		
	}
}
