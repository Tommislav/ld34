using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public delegate void EnemyEvent(Enemy e);
	public event EnemyEvent OnEnemyKilled;


	public enum State {
		Enemy,
		DeadAttachable,
		Friendly,
		Disassembled
	}

	public State currentState = State.Enemy;

	public int hp = 1;



	private float rotationSpeed = 4f;
	private float velX = 0f;
	private float velY = 0f;
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

		if (currentState == State.Disassembled) {
			Vector3 p = transform.position;
			velX *= 0.99f;
			if (velY > 2f) {
				velY -= 0.25f;
			}
			
			p.x += velX;
			p.y += velY;
			transform.position = p;
		}

		if (currentState == State.DeadAttachable || currentState == State.Disassembled) {
			if (transform.position.y < Game.Instance.bounds.outBottom) {
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
			Game.Instance.OnPlayerDamage();
		}


	}

	private void SetState(State s) {

		if (s == currentState) { return; }

		currentState = s;

		if (s == State.DeadAttachable) {
			SendMessage("SetColor", new Color(0.5f, 0.5f, 0.5f, 1));
			SendMessage("OnDied", true, SendMessageOptions.DontRequireReceiver);

			gameObject.layer = 0;

			float extra = (Game.Instance.bounds.top - transform.position.y) / 2f;

			float playerX = Game.Instance.player.position.x;
			float targetX = playerX < transform.position.x ? playerX - extra : playerX + extra;
			float targetY = Game.Instance.bounds.bottom - 2f;
			float time = Mathf.Abs(transform.position.x - targetX) * 0.3f;
			float t2 = time / 2f;
			float fallDelay = Mathf.Abs(transform.position.y + extra - targetY) * 0.3f;

			addX = 0.002f;
			if (targetX < 0) { addX = -addX; }

			LeanTween.moveY(gameObject, transform.position.y + extra, t2).setEase(LeanTweenType.easeOutSine);
			LeanTween.moveY(gameObject, targetY, fallDelay).setEase(LeanTweenType.easeInSine).setDelay(t2);
			
			
			gameObject.AddComponent<AttachToPlayer>();

			if(OnEnemyKilled != null) {
				OnEnemyKilled(this);
			}

		}
		else if (s == State.Friendly) {
			LeanTween.cancel(gameObject);

			transform.SetParent(Game.Instance.player, true);
			gameObject.layer = Game.Instance.playerLayer;
			SendMessage("SetColor", new Color(0f, 178f/255f, 156f/255f, 1));
			SendMessage("OnDied", false, SendMessageOptions.DontRequireReceiver);
			SetFriendly(true);

			Game.Instance.OnDisassemble += OnDisassemble;
		}
		else if (s == State.Disassembled) {
			Game.Instance.OnDisassemble -= OnDisassemble;

			SendMessage("SetColor", new Color(0.5f, 0.5f, 0.5f, 1));
			SendMessage("OnDied", true, SendMessageOptions.DontRequireReceiver);
			gameObject.layer = 0;

			Transform t = Game.Instance.player;
			Vector3 p0 = transform.position;
			Vector3 p1 = t.position;
			float angle = Mathf.Atan2(p0.y - p1.y, p0.x - p1.x);
			velX = Mathf.Cos(angle) * 0.15f;
			velY = Mathf.Sin(angle) * 0.15f;

			transform.SetParent(Game.Instance.player.parent, true); // unparent from player

		}
	}

	private void OnDisassemble() {
		if (currentState == State.Friendly) {
			SetState(State.Disassembled);
		}
	}

	private void OnAttachedToPlayer() {
		SetState(State.Friendly);
	}

	private void MoveCompleted() {
		Destroy(gameObject);
	}

	private void SetFriendly(bool isFriend) {
		BulletSpawner[] spawners = GetComponentsInChildren<BulletSpawner>();
		for (int i=0; i<spawners.Length; i++) {
			spawners[i].friendly = isFriend;
		}
	}

	private void OnDestroy() {
		Game.Instance.OnDisassemble -= OnDisassemble;
		if (OnEnemyKilled != null) {
			OnEnemyKilled(this);
		}
	}
}
