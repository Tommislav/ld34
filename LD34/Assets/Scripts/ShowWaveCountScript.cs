using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowWaveCountScript : MonoBehaviour {


	public bool showCurrent;

	void Start () {
		Text t = GetComponent<Text>();

		int current = WaveCount.currentWaveCount;

		if (showCurrent) {
			t.text = WaveCount.currentWaveCount + "";
		} else {
			t.text = WaveCount.maxWaveCount + "";
		}
	}
	
	
}
