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
