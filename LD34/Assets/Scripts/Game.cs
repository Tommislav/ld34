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
		public float y1;
		public float y2;
		public float y3;
		public float y4;
		public float y5;
		public float y6;

		public bool Contains(Vector2 p) {
			return (p.x >= left && p.x <= right && p.y <= top && p.y >= bottom);
		}

		public bool Contains(Vector3 p) {
			return Contains(new Vector2(p.x, p.y));
		}
	}

	
	public delegate void GameEvent();


	public static Game Instance { get; private set; }



	public event GameEvent OnPlayerHealthChangedEvent;
	public event GameEvent OnDisassemble;
	public event GameEvent OnBossDiedEvent;


	public float gravity = -0.02f;
	public Bounds bounds;
	

	
	public GameObject[] bulletRegistry;
	public GameObject[] enemyRegistry;
	public int playerLayer { get { return LayerMask.NameToLayer("player"); } }
	public int enemyLayer { get { return LayerMask.NameToLayer("enemies"); } }
	public Transform player { get { return _player.transform; } }
	public Color playerColor { get { return _playerColor; } }

	public int health = 5;

	private int _currentWave;


	private Player _player;
	private Camera _bgCamera;
	private Color _bgColor;
	private Color _playerColor;
	

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		_playerColor = _player.GetComponent<Colorize>().color;

		Transform b = transform.FindChild("Bounds");

		Transform tl =b.FindChild("TL");
		Transform br = b.FindChild("BR");

		// Setup bound values
		bounds = new Bounds();
		bounds.top = tl.position.y;
		bounds.left = tl.position.x;
		bounds.bottom = br.position.y;
		bounds.right = br.position.x;

		bounds.centerX = 0;
		bounds.centerY = 0;
		
		float outMargin = 2f;
		

		bounds.outLeft = bounds.left - outMargin;
		bounds.outRight = bounds.right + outMargin;
		bounds.outTop = bounds.top + outMargin;
		bounds.outBottom = bounds.bottom - outMargin;
		
		bounds.l1 = b.FindChild("L1").position.x;
		bounds.l2 = b.FindChild("L2").position.x;
		bounds.l3 = b.FindChild("L3").position.x;

		bounds.r1 = b.FindChild("R1").position.x;
		bounds.r2 = b.FindChild("R2").position.x;
		bounds.r3 = b.FindChild("R3").position.x;

		bounds.y1 = b.FindChild("Y1").position.y;
		bounds.y2 = b.FindChild("Y2").position.y;
		bounds.y3 = b.FindChild("Y3").position.y;
		bounds.y4 = b.FindChild("Y4").position.y;
		bounds.y5 = b.FindChild("Y5").position.y;
		bounds.y6 = b.FindChild("Y6").position.y;
	}


	void Start () {
		_bgCamera = GameObject.Find("BgCamera").GetComponent<Camera>();
		_bgColor = _bgCamera.backgroundColor;
    }
	
	void Update () {
	}


	public void Disassemble() {
		
		if (OnDisassemble != null) {

			_bgCamera.backgroundColor = _playerColor;
			StartCoroutine(ResetBgColor());

			Camera.main.gameObject.GetComponent<CameraShake>().Shake(new Vector2(0.18f, 0.18f), 0.35f);

			OnDisassemble();
		}
	}


	public void SetCurrentWave(int wave) {
		_currentWave = wave + 1; // not zero based
	}

	public void AddHealth() {
		if (health < 5) {
			health += 1;
			if (OnPlayerHealthChangedEvent != null) {
				OnPlayerHealthChangedEvent();
			}
		}
	}

	public void OnPlayerDamage() {
		health--;

		if (health <= 0) {
			GameOver();
			return;
		}


		_bgCamera.backgroundColor = new Color(1, 0, 0);
		StartCoroutine(ResetBgColor());

		Camera.main.gameObject.GetComponent<CameraShake>().Shake(new Vector2(0.15f,0.15f), 0.3f);

		if (OnPlayerHealthChangedEvent != null) {
			OnPlayerHealthChangedEvent();
		}
	}

	public void OnBossDidDie() {
		if (OnBossDiedEvent != null) {
			OnBossDiedEvent();
		}
	}

	private void GameOver() {
		WaveCount.currentWaveCount = _currentWave;
		if (_currentWave > WaveCount.maxWaveCount) {
			WaveCount.maxWaveCount = _currentWave;
		}

		Application.LoadLevel("GameOver");
	}

	private IEnumerator ResetBgColor() {
		yield return new WaitForSeconds(0.2f);
		_bgCamera.backgroundColor = _bgColor;
	}
}
