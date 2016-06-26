using UnityEngine;
using System.Collections;

//自機
//1.ジョイステックで移動
public class Player : Unit {
	private Joystick joystick;
	void Start () {
		joystick = GameObject.Find("MainCanvas/Joystick").GetComponent<Joystick>();
	  speed = 4;
	}
	
	void Update () {
	  if (Input.GetMouseButtonDown(1)) {
	  	StepUp();
	  }
	}


  //移動に使う
	void FixedUpdate() {

	}

	public void StepUp() {
		GameObject.Find("MainCanvas").GetComponent<MainCanvas>().IsJump = true;
		this.GetComponent<Animator>().SetTrigger("Jump");
		Vector3 power = this.transform.TransformDirection(new Vector3(0, Mathf.Abs(joystick.Position.y) * 2, Mathf.Abs(joystick.Position.y)));
	  this.GetComponent<Rigidbody>().velocity =  power * 3;
	}
}
