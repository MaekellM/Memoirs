using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerScript : MonoBehaviour {
    CharacterController CHController;
    GameObject Cholder,MainC;
    float Horz, Vert;
    float FallSpeed;
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
        
        if (CheckIfGrounded())
        {
            FallSpeed = 0;//Vertical speed
            if (Input.GetKeyDown(KeyCode.Space)) {//sets vertical speed when space is pressed
                FallSpeed = 3;
            }
        }
        else { 
            FallSpeed -= 0.2f;//decreases vertical speed while in air
        }

        CHController.Move(Cholder.transform.TransformDirection(new Vector3(Horz, FallSpeed, Vert)) * 0.1f);//moves the player based on where the camera is facing.
	}

    bool CheckIfGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);//draws a ray from the feet
        return Physics.Raycast(ray, 0.1f);//returns if ray collides
    }
}
