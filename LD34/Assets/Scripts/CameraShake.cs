using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private Vector2 _offset;
	private Vector2 _size;
	private float _countDown;
	private bool _running;


	void Start () {
		_offset = new Vector2();
	}
	
	void Update () {
		if (_running) {
			Vector3 p = transform.position;
			p.x -= _offset.x;
			p.y -= _offset.y;

			_countDown -= Time.deltaTime;

			if (_countDown > 0) {
				_offset.x = Random.Range(-_size.x, _size.x);
				_offset.y = Random.Range(-_size.y, _size.y);

				p.x += _offset.x;
				p.y += _offset.y;
			}
			else {
				_offset = Vector2.zero;
				_running = false;
			}
			
			transform.position = p;
		}
	}

	public void Shake(Vector2 intensity, float time) {
		_size = intensity;
		_countDown = time;
		_running = true;
	}
}
