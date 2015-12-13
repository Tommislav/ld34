using UnityEngine;
using System.Collections;

public class WaveBase : MonoBehaviour {

	public enum Dir {
		left,
		right
	};
	public Dir direction = Dir.right;
	public float startDelay;
	public int index;
	public Game.Bounds bounds;
	public bool isDead;
	public Vector2 startPos;


	virtual protected void Start() {
		GetComponent<Enemy>().SetInvincible(startDelay + 1);
	}

	protected void SetStartPos(float x, float y) {
		startPos = new Vector2(x,y);
		transform.position = startPos;
	}

	
	public GameObject Init(int index, Dir startDir, float startDelay) {
		this.index = index;
		this.direction = startDir;
		this.startDelay = startDelay;
		this.bounds = Game.Instance.bounds;
		return gameObject;
	}

	private void OnDied(bool didDie) {
		if (didDie) {
			isDead = true;
			LeanTween.cancel(gameObject);
		}
	}

	protected void OnMoveDone() {
		SendMessage("MoveCompleted");
	}

	protected void FacePlayerX() {
		float rot = (transform.position.x < bounds.centerX) ? -90 : 90;
		transform.Rotate(0, 0, rot);
	}

	protected void FacePlayerY() {
		float rot = (transform.position.y < bounds.centerY) ? 0 : 180;
		transform.Rotate(0, 0, rot);
	}
}
