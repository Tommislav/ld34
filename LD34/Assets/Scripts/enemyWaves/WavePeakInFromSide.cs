using UnityEngine;
using System.Collections;

public class WavePeakInFromSide : WaveBase {


	private float x0;
	private float x1;
	private float y0;
	private float peekWait;



	protected override void Start() {
		base.Start();

		x0 = direction == Dir.left ? bounds.outLeft : bounds.outRight;
		x1 = direction == Dir.left ? bounds.l1 : bounds.r1;

		float[] yCoords = { bounds.y1, bounds.y2, bounds.y3, bounds.centerY, bounds.y4, bounds.y5, bounds.y6 };
		y0 = yCoords[index];

		if (extraPropSet) {
			peekWait = extraProp;
		} else {
			peekWait = 3 + Random.Range(0, 4);
		}

		

		SetStartPos(x0, y0);
		FacePlayerX();

		LeanTween.moveX(gameObject, x1, 0.5f).setEase(LeanTweenType.easeOutSine).setDelay(startDelay).setOnComplete(m1);
	}

	private void m1() {
		LeanTween.moveX(gameObject, x0, 0.5f).setEase(LeanTweenType.easeOutSine).setDelay(peekWait).setOnComplete(OnMoveDone);
	}
	
}
