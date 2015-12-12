using UnityEngine;
using System.Collections;
using System;

public class Colorize : MonoBehaviour {

	public Color color;


	private SpriteRenderer _renderer;


	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
		SetColor(color);
	}

	public void SetColor(Color color) {
		_renderer.color = color;

		ParticleSystem[] psystems = GetComponentsInChildren<ParticleSystem>();
		for (int i=0; i<psystems.Length;i++) {
			Color oldCol = psystems[i].startColor;
			Color newCol = new Color(color.r, color.g, color.b, oldCol.a);
			psystems[i].startColor = newCol;
		}

	}
}
