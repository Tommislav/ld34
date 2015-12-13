using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	public struct Bounds {
		public float top;
		public float bottom;
		public float left;
		public float right;

		public float outTop;
		public float outBottom;
		public float outLeft;
		public float outRight;

		public float centerX;
		public float centerY;
		public float l1;
		public float l2;
		public float l3;
		public float r1;
		public float r2;
		public float r3;
		public float t1;
		public float t2;
		public float t3;
		public float b1;
		public float b2;
		public float b3;

		public bool Contains(Vector2 p) {
			return (p.x >= left && p.x <= right && p.y <= top && p.y >= bottom);
		}

		public bool Contains(Vector3 p) {
			return Contains(new Vector2(p.x, p.y));
		}
	}

	
	public delegate void GameEvent();


	public static Game Instance { get; private set; }



	public event GameEvent OnPlayerDamageEvent;
	public event GameEvent OnDisassemble;


	public float gravity = -0.02f;
	public Bounds bounds;
	

	
	public GameObject[] bulletRegistry;
	public GameObject[] enemyRegistry;
	public int playerLayer { get { return LayerMask.NameToLayer("player"); } }
	public int enemyLayer { get { return LayerMask.NameToLayer("enemies"); } }
	public Transform player { get { return _player.transform; } }

	private Player _player;
	private Camera _bgCamera;
	private Color _bgColor;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		Transform tl = transform.FindChild("BoundsTL");
		Transform br = transform.FindChild("BoundsBR");

		// Setup bound values

		
		bounds = new Bounds();
		bounds.top = tl.position.y;
		bounds.left = tl.position.x;
		bounds.bottom = br.position.y;
		bounds.right = br.position.x;

		bounds.centerX = 0;
		bounds.centerY = 0;

		
		float outMargin = 2f;
		float margin = 1f;

		float height = bounds.top - bounds.bottom;
		float ySplit = (height - margin - margin) / 7f;

		float margin1 = 1f;
		float margin2 = 2f;
		float margin3 = 3f;



		bounds.outLeft = bounds.left - outMargin;
		bounds.outRight = bounds.right + outMargin;
		bounds.outTop = bounds.top + outMargin;
		bounds.outBottom = bounds.bottom - outMargin;

		bounds.l1 = bounds.left + margin1;
		bounds.l2 = bounds.left + margin2;
		bounds.l3 = bounds.left + margin3;

		bounds.r1 = bounds.right - margin1;
		bounds.r2 = bounds.right - margin2;
		bounds.r3 = bounds.right - margin3;

		bounds.t1 = bounds.top - margin - (ySplit * 1);
		bounds.t2 = bounds.top - margin - (ySplit * 2);
		bounds.t3 = bounds.top - margin - (ySplit * 3);

		bounds.b3 = bounds.top - margin - (ySplit * 5);
		bounds.b2 = bounds.top - margin - (ySplit * 6);
		bounds.b1 = bounds.top - margin - (ySplit * 7);
	}


	void Start () {
		_bgCamera = GameObject.Find("BgCamera").GetComponent<Camera>();
		_bgColor = _bgCamera.backgroundColor;
    }
	
	void Update () {
	}


	public void Disassemble() {
		if (OnDisassemble != null) {
			OnDisassemble();
		}
	}


	public void OnPlayerDamage() {
		_bgCamera.backgroundColor = new Color(1, 0, 0);
		StartCoroutine(ResetBgColor());

		Camera.main.gameObject.GetComponent<CameraShake>().Shake(new Vector2(0.15f,0.15f), 0.2f);
	}

	private IEnumerator ResetBgColor() {
		yield return new WaitForSeconds(0.1f);
		_bgCamera.backgroundColor = _bgColor;
	}
}
