using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFace : MonoBehaviour {


	public GameObject player;

	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (player.transform);
	}
}
