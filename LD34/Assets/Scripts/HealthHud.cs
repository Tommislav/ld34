using UnityEngine;
using System.Collections;
using System;

public class HealthHud : MonoBehaviour {

	public GameObject[] hearts;

	// Use this for initialization
	void Start () {
		Game.Instance.OnPlayerHealthChangedEvent += OnHealthChanged;
	}

	void OnDestroy() {
		Game.Instance.OnPlayerHealthChangedEvent -= OnHealthChanged;
	}

	private void OnHealthChanged() {
		int health = Game.Instance.health;
		for (int i=0; i<hearts.Length; i++) {
			hearts[i].SetActive(health > i);
		}
	}

	
}
