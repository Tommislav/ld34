using UnityEngine;
using System.Collections;

public class WaveOneLapAround : WaveBase {

	
	private Vector2 p1;
	private Vector2 p2;
	private Vector2 p3;
	private Vector2 p4;
	private float dur0;
	private float dur1;
	private bool checkX0;
	private bool checkX1;
	private bool checkX2;
	private bool checkX3;


	protected override void Start() {
		base.Start();

		Vector2 tl = new Vector2(bounds.l1, bounds.y1);
		Vector2 tr = new Vector2(bounds.r1, bounds.y1);
		Vector2 br = new Vector2(bounds.r1, bounds.y6);
		Vector2 bl = new Vector2(bounds.l1, bounds.y6);

		float durShort = 4;
		float durLong = 6;

		if (index == 0) {
			SetStartPos(bounds.outLeft, bounds.y1);
			p1 = tr;
			p2 = br;
			p3 = bl;
			p4 = new Vector2(bounds.l1, bounds.outTop);

			dur0 = durLong;
			dur1 = durShort;
			checkX0 = checkX2 = false;
			checkX1 = checkX3 = true;

		} else if (index == 1) {
			SetStartPos(bounds.r1, bounds.outTop);
			p1 = br;
			p2 = bl;
			p3 = tl;
			p4 = new Vector2(bounds.outRight, bounds.y1);

			dur0 = durShort;
			dur1 = durLong;
			checkX0 = checkX2 = true;
			checkX1 = checkX3 = false;
		}
		else if (index == 2) { 
            SetStartPos(bounds.outRight, bounds.y6);
			p1 = bl;
			p2 = tl;
			p3 = tr;
			p4 = new Vector2(bounds.r1, bounds.outBottom);

			dur0 = durLong;
			dur1 = durShort;
			checkX0 = checkX2 = false;
			checkX1 = checkX3 = true;

		} else if (index == 3) {
			SetStartPos(bounds.outBottom, bounds.l1);
			p1 = tl;
			p2 = tr;
			p3 = br;
			p4 = new Vector2(bounds.outLeft, bounds.y6);

			dur0 = durShort;
			dur1 = durLong;
			checkX0 = checkX2 = true;
			checkX1 = checkX3 = false;
		}

		LeanTween.move(gameObject, p1, dur0).setDelay(startDelay).setOnComplete(m1);
		if (checkX0) { FacePlayerX(); } else { FacePlayerY(); }
	}

	private void m1() {
		if (checkX1) { FacePlayerX(); } else { FacePlayerY(); }
		LeanTween.move(gameObject, p2, dur1).setOnComplete(m2);
	}
	private void m2() {
		if (checkX2) { FacePlayerX(); } else { FacePlayerY(); }
		LeanTween.move(gameObject, p3, dur0).setOnComplete(m3);
	}
	private void m3() {
		if (checkX3) { FacePlayerX(); } else { FacePlayerY(); }
		LeanTween.move(gameObject, p4, dur1).setOnComplete(OnMoveDone);
	}
	
}
