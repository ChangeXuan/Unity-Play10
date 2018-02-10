using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
	[SyncVar(hook="onChangeHealth")]
	public int currentHealth = maxHealth;
	public Slider healthSlider;

	public void takeDamage(int damage) {

		// 血量的处理只在服务器端执行
		if (!isServer) {
			return;
		}

		currentHealth -= damage;
		if (currentHealth <= 0) {
			currentHealth = 0;
			Debug.Log ("Dead");
		}
	}

	void onChangeHealth(int newHealth) {
		healthSlider.value = newHealth /(float) maxHealth;
	}
}
