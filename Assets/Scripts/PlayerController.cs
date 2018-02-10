using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletPos;

	void Update () {

		// 判断是否是本地客户端操作		
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Cmdfire ();
		}

		moveController ();
	}

	// 重写父类方法
	// 这个方法只会在本地角色那里调用
	public override void OnStartLocalPlayer () {
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	// 移动旋转控制
	void moveController() {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		transform.Rotate (Vector3.up * h * 300 * Time.deltaTime);
		transform.Translate (Vector3.forward * v * 3 * Time.deltaTime);
	}

	// 子弹的生成应该在server端完成，然后把子弹同步到各个client
	[Command]//运行在server，响应在client
	void Cmdfire() {
		GameObject bullte = Instantiate (bulletPrefab, bulletPos.position, bulletPos.rotation);
		bullte.GetComponent<Rigidbody> ().velocity = bullte.transform.forward * 10f;
		Destroy (bullte, 2);

		NetworkServer.Spawn (bullte);
	}
}
