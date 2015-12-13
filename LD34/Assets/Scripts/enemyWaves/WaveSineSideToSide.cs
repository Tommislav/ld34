using UnityEngine;
using System.Collections;

public class WaveSineSideToSide : WaveBase {

	private float sineSpeed;
	private int sineLoops;


	protected override void Start() {
		base.Start();

		sineSpeed = index == 0 ? 1f : 0.5f;
		sineLoops = index == 0 ? 4 : 8;

		SetStartPos(bounds.outLeft, bounds.y1);
		FacePlayerY();

		LeanTween.moveX(gameObject, bounds.r1, 8).setOnComplete(m1).setDelay(startDelay);
		LeanTween.moveY(gameObject, bounds.y1 - 1, sineSpeed).setLoopPingPong(sineLoops).setEase(LeanTweenType.easeInOutSine).setDelay(startDelay);
	}

	private void m1() {
		LeanTween.moveY(gameObject, bounds.y2, 1).setOnComplete(m2);
	}

	private void m2() {
		LeanTween.moveX(gameObject, bounds.outLeft, 8).setOnComplete(m1).setOnComplete(OnMoveDone);
		LeanTween.moveY(gameObject, bounds.y2 - 1, sineSpeed).setLoopPingPong(sineLoops).setEase(LeanTweenType.easeInOutSine);
	}
	
}
