using UnityEngine;
using System.Collections;

public class NextSceneScript : MonoBehaviour {

	public string nextScene;
	
	
	void Update () {
		if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X)) {
			Application.LoadLevel(nextScene);
		}
	}
}
