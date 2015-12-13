using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveShip : MonoBehaviour {

	private struct PathPoint {
		public float delay;
		public float x;
		public float y;
		public float time;
		public PathPoint(float delay, float x, float y, float time) {
			this.delay = delay;
			this.x = x;
			this.y = y;
			this.time = time;
		}
	}

	private List<PathPoint> _points = new List<PathPoint>();
	private int _currentIndex;


	void Start() {
		NextPath();
	}

	void Update() {

	}



	public MoveShip AddToPath(float delay, float x, float y, float time) {
		
		_points.Add(new PathPoint(delay, x, y, time));
		return this;
	}

	public MoveShip FleeOnDone(float delay) {
		Vector3 p = transform.position;
		AddToPath(delay, p.x, p.y, 2f);
		return this;
	}

	private void NextPath() {
		PathPoint p = _points[_currentIndex];

		Debug.Log("MovePath "+ _currentIndex + " " + p.x + ", p.y " + p.y);
		
		_currentIndex++;
		LeanTween.moveX(gameObject, p.x, p.time).setDelay(p.delay).setEase(LeanTweenType.easeInCubic);
		LeanTween.moveY(gameObject, p.y, p.time).setDelay(p.delay).setEase(LeanTweenType.easeOutCubic).setOnComplete(OnPathComplete);
	}

	private void OnPathComplete() {
		if (_currentIndex < _points.Count) {
			NextPath();
		} else {
			SendMessage("MoveCompleted");
		}
	}

	private void OnDied(bool didDie) {
		if (didDie) {
			LeanTween.cancel(gameObject);
		}
	}

}
