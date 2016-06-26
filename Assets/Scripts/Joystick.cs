using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ジョイスティック操作
public class Joystick : MonoBehaviour {
	public GameObject attachJoystickSprite, attachJoystickBackSprite;
	public Camera AttachGUICamera;
	public GameObject target;

	private Vector2 _position;
	public Vector2 Position {
		get{return _position;}
	}
	private bool isPush = false;
	public bool IsPush {
		set {isPush = value;}
	}

	private Vector2 initPos;
	private float _maxRadius;
	private const float MAX_RADIUS_RATE = 0.55f;

	//初期化
  void Awake() {
		attachJoystickSprite = GameObject.Find("MainCanvas/Joystick/JoystickBackSprite/JoystickSprite");
		attachJoystickBackSprite = GameObject.Find("MainCanvas/Joystick/JoystickBackSprite");
		initPos = attachJoystickSprite.transform.localPosition;
		_position = Vector2.zero;
		_maxRadius = 100 * MAX_RADIUS_RATE;//attachJoystickBackSprite.GetComponent<RectTransform>().sizeDelta.y * MAX_RADIUS_RATE;
	}

	//更新
	void Update () {
	  DisplayConfirmation();
	  Move();
	}

	private void DisplayConfirmation() {
		if (Input.GetMouseButtonDown(0)) {
			Vector2 tapPoint = Input.mousePosition;
			Vector2 joystickPos = attachJoystickSprite.transform.position;
			Vector2 margin = attachJoystickSprite.GetComponent<RectTransform>().sizeDelta;
			if (tapPoint.x >= joystickPos.x - margin.x/2 && tapPoint.x <= joystickPos.x + margin.x/2 &&
				  tapPoint.y >= joystickPos.y - margin.y/2 && tapPoint.y <= joystickPos.y + margin.y/2) {
				isPush = true;
				//Debug.Log(attachJoystickSprite.transform.localPosition+":"+Input.mousePosition + ":" + attachJoystickSprite.GetComponent<RectTransform>().sizeDelta);
			}
			/*Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
			if (aCollider2d) {
				Debug.Log(aCollider2d.transform.gameObject.name);
				if (aCollider2d.transform.gameObject.tag == "Joystick") isPush = true;
			}*/
		}
		else if (Input.GetMouseButtonUp(0)) {
			Debug.Log("OK");
			isPush = false;
			_position = Vector2.zero;
			attachJoystickSprite.transform.localPosition = initPos;
			target.GetComponent<Rigidbody>().velocity = new Vector3(0, 0 , 0);
		}
	}
	private void Move() {
		if (!isPush)  return;
		Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		attachJoystickSprite.transform.position = Input.mousePosition;

		//半径が制限を超えてる場合は制限内に抑える
		float radius = Vector3.Distance (Vector3.zero, attachJoystickSprite.transform.localPosition);

		//角度
		float radian = CalcRadian(Vector3.zero, attachJoystickSprite.transform.localPosition);		
		if(radius > _maxRadius){
			Vector3 setVec = Vector3.zero;
			setVec.x = _maxRadius * Mathf.Cos (radian);
			setVec.y = _maxRadius * Mathf.Sin (radian);

			attachJoystickSprite.transform.localPosition = setVec;
		}

		//-1〜1に正規化
		_position = new Vector2 (
			attachJoystickSprite.transform.localPosition.x / _maxRadius,
			attachJoystickSprite.transform.localPosition.y / _maxRadius
		);

		//ターゲットの移動
		target.GetComponent<Rigidbody>().velocity = new Vector3(_position.x, 0 , _position.y) * 4;
		target.transform.rotation = Quaternion.Euler(new Vector3(0,-radian * Mathf.Rad2Deg + 90,0));
	}

	//2点間の角度を求める
	private float CalcRadian(Vector3 from, Vector3 to) {
		float dx = to.x - from.x;
		float dy = to.y - from.y;
		float radian = Mathf.Atan2(dy, dx);
		Debug.Log(radian * Mathf.Rad2Deg);
		return radian;
	}
}
