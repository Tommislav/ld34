using UnityEngine;
using System.Collections;

public class AttachToPlayer : MonoBehaviour {

	
	


	private void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.layer == Game.Instance.playerLayer) {
			SendMessage("OnAttachedToPlayer");
			Destroy(this);
		}
	}
}