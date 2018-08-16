using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

	Transform trash;

	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			if(trash != null) {
				if (trash.parent != null) {
					Destroy (trash.parent.gameObject);
				} 
				else {
					Destroy (trash.gameObject);
				}
			}
		}
	}

	void OnTriggerExit2D (){
		trash = null;
	}

	void OnTriggerEnter2D (Collider2D coll) {
		Debug.Log ("Bonk");
		Transform temp = coll.transform;
		if (temp.GetComponent<DragAndDrop> ().isDragged) {
			trash = temp;
		}
	}
}
