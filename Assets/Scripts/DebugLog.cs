using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//デバッグログを表示
public class DebugLog : MonoBehaviour {
	public static Text log;
	void Awake() {
		log = this.GetComponent<Text>();
	}
	public static void Show(string centence) {
		log.text = centence;
	}
}
