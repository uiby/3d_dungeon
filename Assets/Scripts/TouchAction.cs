using UnityEngine;
using System.Collections;

public class TouchAction : MonoBehaviour {
	void Start () {
	
	}
	
	void Update () {
		/*if (Input.touchCount > 0) {
			Touch _touch = Input.GetTouch(0);
			Vector2 deltaPos = _touch.deltaPosition;
			DebugLog.Show("x:"+deltaPos.x+"\ty:"+deltaPos.y);
			if (_touch.phase == TouchPhase.Ended) {
				Stop();
			}
			else Move(deltaPos);
		}*/
	}

	public void Move(Vector2 move) {
		this.GetComponent<Rigidbody>().velocity = new Vector3(move.x, 0, move.y);
	}
	public void Stop() {
		this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
	}
}
