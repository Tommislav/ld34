using UnityEngine;
using System.Collections;
using System;

public class WaveZigZagTopDown : WaveBase {
	
	private float x1;
	private float x2;
	private float x3;

	protected override void Start() {
		base.Start();
	
		x1 = direction == Dir.left ? bounds.l1 : bounds.r1;
		x2 = direction == Dir.left ? bounds.l2 : bounds.r2;
		x3 = direction == Dir.left ? bounds.l3 : bounds.r3;

		SetStartPos(x3, bounds.outTop);
		FacePlayerX();

		if (!isDead) {
			LeanTween.moveX(gameObject, x1, 1).setDelay(startDelay).setEase(LeanTweenType.easeInCubic);
			LeanTween.moveY(gameObject, bounds.y1, 1).setDelay(startDelay).setEase(LeanTweenType.easeOutCubic).setOnComplete(m1);
		}
	}
	
	void Update () {
	
	}


	private void m1() {
		LeanTween.move(gameObject, new Vector2(x2, bounds.y2), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(m2);
	}

	private void m2() {
		LeanTween.move(gameObject, new Vector2(x3, bounds.y3), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(mcenter);
	}

	private void mcenter() {
		LeanTween.move(gameObject, new Vector2(x2, bounds.centerY), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(m3);
	}

	private void m3() {
		LeanTween.move(gameObject, new Vector2(x1, bounds.y4), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(m4);
	}

	private void m4() {
		LeanTween.move(gameObject, new Vector2(x2, bounds.y5), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(m5);
	}

	private void m5() {
		LeanTween.move(gameObject, new Vector2(x3, bounds.y6), 1).setDelay(1).setEase(LeanTweenType.easeInOutCubic).setOnComplete(m6);
	}

	private void m6() {
		float xOut = direction == Dir.left ? bounds.outLeft : bounds.outRight;
		
		LeanTween.moveX(gameObject, x1, 2).setDelay(1).setEase(LeanTweenType.easeInCubic);
		LeanTween.moveY(gameObject, bounds.outBottom, 2).setDelay(1).setEase(LeanTweenType.easeOutCubic).setOnComplete(OnMoveDone);
	}

}
