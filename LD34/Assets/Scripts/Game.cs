using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	
	public delegate void GameEvent();


	public static Game Instance { get; private set; }



	public event GameEvent OnPlayerDamageEvent;


	public float gravity = -0.02f;
	public Rect bounds;

	
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
		bounds = new Rect(tl.position.x, br.position.y, (br.position.x - tl.position.x), (tl.position.y - br.position.y));

	}


	void Start () {
		_bgCamera = GameObject.Find("BgCamera").GetComponent<Camera>();
		_bgColor = _bgCamera.backgroundColor;
    }
	
	void Update () {
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
