using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//画面の制御
public class MainCanvas : MonoBehaviour {
	private bool isJump = false;
	public bool IsJump {
		set {isJump = value;}
		get{return isJump;}
	}
}
