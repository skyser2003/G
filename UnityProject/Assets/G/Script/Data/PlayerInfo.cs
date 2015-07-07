using UnityEngine;
using System.Collections;

[SerializeField]
public struct PlayerInfoStruct {
	public string name;
	public float damage;
	public int health;
	public float defence;
	public float weight;
	public float maxMoveSpeed;
	public float moveForce;
	public float moveFriction;
	public float jumpForce;
	public int jumpNumber;
	public float gravityFriction;
}

public class PlayerInfo : MonoBehaviour {

	private PlayerInfoStruct playerData;

	PlayerInfo(PlayerInfoStruct info) {
		this.playerData = info;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
