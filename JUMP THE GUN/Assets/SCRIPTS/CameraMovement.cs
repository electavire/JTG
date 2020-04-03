using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public GameObject StartWall;
	public GameObject EndWall;
	public GameObject Player;
	public Camera ThisCamera;
	public int cameraToCharAdjust = 5;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 endWallPos = ThisCamera.WorldToViewportPoint(EndWall.transform.position);
		Vector3 startWallPos = ThisCamera.WorldToViewportPoint(StartWall.transform.position);
		//Debug.Log ("Start Wall X: " + startWallPos.x);
		//Debug.Log ("End Wall X: " + endWallPos.x);
		if(startWallPos.x < 0 && endWallPos.x > 1){
			transform.position = new Vector3 (Player.transform.position.x + cameraToCharAdjust, transform.position.y, transform.position.z);//(Player.transform.position.x, transform.position.y, transform.position.z);
		}
			//Debug.Log("FART");s
		else if(startWallPos.x > 0 && Player.GetComponent<Rigidbody2D>().velocity.x > 0){
			transform.position = new Vector3 (Player.transform.position.x + cameraToCharAdjust, transform.position.y, transform.position.z);
		}
		else if(endWallPos.x <1 && Player.GetComponent<Rigidbody2D>().velocity.x < 0){
			transform.position = new Vector3 (Player.transform.position.x + cameraToCharAdjust, transform.position.y, transform.position.z);
		}
		}
}
