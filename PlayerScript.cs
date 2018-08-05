using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerScript : MonoBehaviour {
    CharacterController CHController;
    GameObject Cholder,MainC;
    float Horz, Vert;
    Vector3 targetRotation;
	// Use this for initialization
	void Start () {
		CHController = GetComponent<CharacterController>();
        Cholder = GameObject.Find("CameraHolder");
        MainC = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update () {

        Horz = Input.GetAxis("Horizontal");//gets A/D input or horizontal controller Input.
        Vert = Input.GetAxis("Vertical");//gets W/S input or Vertical controller Input.
        targetRotation = Cholder.transform.forward * Vert + Cholder.transform.right * Horz;//gets the player's rotation from input.
        if(targetRotation.magnitude > 0){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetRotation), 0.5f);//Rotates player based on calculated rotation.
        }
        CHController.Move(Cholder.transform.TransformDirection(new Vector3(Horz, 0, Vert)) * 0.1f);//moves the player based on where the camera is facing.
	}
}
