using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public GameObject LookAtThis,CameraToHold;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        transform.LookAt(new Vector3(LookAtThis.transform.position.x, transform.position.y, LookAtThis.transform.position.z));//rotates the holder to face the player via Y axis.
		CameraToHold.transform.LookAt(LookAtThis.transform);//rotates the main camera to face the player.
		transform.position = Vector3.Lerp(transform.position,LookAtThis.transform.position + new Vector3(0,4,-11),0.02f);//moves the camera towards the player.
	}
}
