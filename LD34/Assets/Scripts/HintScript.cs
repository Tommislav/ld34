using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HintScript : MonoBehaviour {

	private Text t;
	
	void Start () {
		t = GetComponent<Text>();
		//t.text = "";
		t.CrossFadeAlpha(0, 0, true);
		
	}
	
	public void ShowHint(string hint) {
		t.text = hint;
		t.CrossFadeAlpha(1, 1f, true);
		StartCoroutine(Hide());
	}

	private IEnumerator Hide() {
		yield return new WaitForSeconds(4);
		t.CrossFadeAlpha(0, 1f, true);
	}
}
