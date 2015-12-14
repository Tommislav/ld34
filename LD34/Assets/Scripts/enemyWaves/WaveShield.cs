using UnityEngine;
using System.Collections;
using System;

public class WaveShield : WaveBase {

	protected override void Start() {
		base.Start();

		Game.Instance.OnBossDiedEvent += OnBossDie;

		float x = direction == Dir.left ? bounds.l3 : bounds.r3;
		SetStartPos(x, bounds.top + 1);
		FacePlayerX();

		LeanTween.moveY(gameObject, bounds.bottom, 10).setLoopPingPong(4).setOnComplete(m1).setDelay(startDelay);
		float wiggleX = x + UnityEngine.Random.Range(-0.1f, 0.3f);

		LeanTween.moveX(gameObject, wiggleX, 0.5f).setLoopPingPong(20).setDelay(startDelay);
	}

	private void OnBossDie() {

		if (isDead) {
			return;
		}

		LeanTween.cancel(gameObject);
		float toX = direction == Dir.left ? bounds.outLeft : bounds.outRight;
		float toY = UnityEngine.Random.Range(bounds.bottom, bounds.top);
        LeanTween.move(gameObject, new Vector2(toX, toY), 1.5f).setEase(LeanTweenType.easeInSine).setOnComplete(OnMoveDone);
	}

	private void m1() {
		LeanTween.moveY(gameObject, bounds.outTop, 2).setOnComplete(OnMoveDone);
	}

	void OnDestroy() {
		Game.Instance.OnBossDiedEvent -= OnBossDie;
	}
}
