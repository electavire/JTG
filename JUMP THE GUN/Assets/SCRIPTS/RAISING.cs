using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAISING : MonoBehaviour {
	float time = 0f;
	public float moveRateIncrease = 1f;
	float moveSpeed = 0f;
	public float YMidAnchor;
	public float adjustDistance = 2;
	public float horizontalAdjust = 0;
	public float verticalAdjust = 0;
	public float max = 4;
	public float cosStartPoint = 0;
	float sinAdjust = 0;
	// Use this for initialization
	void Start () {

		YMidAnchor = transform.position.y;
		transform.position += new Vector3 (0, verticalAdjust, horizontalAdjust);

	}

	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, moveSpeed*Time.deltaTime, 0);
		time += Time.deltaTime;
		if (transform.position.y < YMidAnchor + adjustDistance && moveSpeed <= max) {
			moveSpeed += moveRateIncrease*Time.deltaTime;
		}
		if (transform.position.y > YMidAnchor + adjustDistance && moveSpeed >= -max) {
			moveSpeed += -moveRateIncrease*Time.deltaTime;
		}

	}

	/*Vector3 oscillationAdjustVector () {
		//get the adjustment amount by getting the integral of the adjustment time of cos (antiderivative of cos is sin)
		//get negative or positive from position in unit circle

	}*/
}
