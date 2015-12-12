using UnityEngine;
using System.Collections;
using System;

public class Game : MonoBehaviour {

	public delegate void GameEvent();


	public static Game Instance { get; private set; }



	public event GameEvent OnPlayerDamageEvent;


	public float gravity = -0.02f;
	public Rect bounds;


	private Player _player;

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
	
	}
	
	void Update () {
	
		


	}
}
