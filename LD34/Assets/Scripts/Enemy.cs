using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public enum State {
		Enemy,
		DeadAttachable,
		Friendly,
		Dead
	}

	public State currentState = State.Enemy;

	public int hp = 1;



	private float rotationSpeed = 4f;
	private float velX = 0f;
	private float addX = 0f;



	void Start () {
	
	}
	
	void Update () {
		if (currentState == State.DeadAttachable) {
			transform.Rotate(0, 0, rotationSpeed);

			Vector3 p = transform.position;
			if (Mathf.Abs(velX) < 0.05f) {
				velX += addX;
			}
			
			transform.position = new Vector3(p.x + velX, p.y);
			
		}

		if (currentState == State.DeadAttachable || currentState == State.Dead) {
			if (transform.position.y < Game.Instance.bounds.yMin - 1f) {
				GameObject.Destroy(gameObject);
			}
		}
	}

	

	private void OnHitByBullet(Bullet b) {
		
		if (currentState == State.Enemy) {
			if (--hp <= 0) {
				SetState(State.DeadAttachable);
			}
			
		} else if (currentState == State.Friendly) {
			//TODO give damage to player
			Debug.Log("give damage to player!");
		}


	}

	private void SetState(State s) {

		if (s == currentState) { return; }

		currentState = s;

		if (s == State.DeadAttachable) {
			SendMessage("SetColor", new Color(0.5f, 0.5f, 0.5f, 1));
			SendMessage("OnDied", true, SendMessageOptions.DontRequireReceiver);

			gameObject.layer = 0;

			float extra = (Game.Instance.bounds.yMax - transform.position.y) / 2f;

			float playerX = Game.Instance.player.position.x;
			float targetX = playerX < transform.position.x ? playerX - extra : playerX + extra;
			float targetY = Game.Instance.bounds.yMin - 2;
			float time = Mathf.Abs(transform.position.x - targetX) * 0.3f;
			float t2 = time / 2f;
			float fallDelay = Mathf.Abs(transform.position.y + extra - targetY) * 0.3f;

			addX = 0.002f;
			if (targetX < 0) { addX = -addX; }

			LeanTween.moveY(gameObject, transform.position.y + extra, t2).setEase(LeanTweenType.easeOutSine);
			LeanTween.moveY(gameObject, targetY, fallDelay).setEase(LeanTweenType.easeInSine).setDelay(t2);
			
			
			gameObject.AddComponent<AttachToPlayer>();
		}
		else if (s == State.Friendly) {
			LeanTween.cancel(gameObject);

			transform.SetParent(Game.Instance.player, true);
			gameObject.layer = Game.Instance.playerLayer;
			SendMessage("SetColor", new Color(0f, 178f/255f, 156f/255f, 1));
			SendMessage("OnDied", false, SendMessageOptions.DontRequireReceiver);
			SetFriendly(true);
		}

	}

	private void OnAttachedToPlayer() {
		SetState(State.Friendly);
	}

	private void SetFriendly(bool isFriend) {
		BulletSpawner[] spawners = GetComponentsInChildren<BulletSpawner>();
		for (int i=0; i<spawners.Length; i++) {
			spawners[i].friendly = isFriend;
		}
	}
}
