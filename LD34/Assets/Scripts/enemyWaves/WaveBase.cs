﻿using UnityEngine;
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

	public bool extraPropSet;
	public float extraProp;
	public bool isBoss;


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

	public WaveBase SetExtraProp(float prop) {
		extraPropSet = true;
		extraProp = prop;
		return this;
	}

	public WaveBase SetIsBoss() {
		this.isBoss = true;
		return this;
	}

	private void OnDied(bool didDie) {
		if (didDie) {
			isDead = true;
			LeanTween.cancel(gameObject);

			if (isBoss) {
				Game.Instance.OnBossDidDie();
			}
		}
	}

	protected void OnMoveDone() {
		SendMessage("MoveCompleted");
	}

	protected void FacePlayerX() {
		float rot = (transform.position.x < bounds.centerX) ? 90 : -90;
		transform.rotation = Quaternion.Euler(0, 0, rot);
	}

	protected void FacePlayerY() {
		float rot = (transform.position.y < bounds.centerY) ? 180 : 0;
		transform.rotation = Quaternion.Euler(0, 0, rot);
	}
}
