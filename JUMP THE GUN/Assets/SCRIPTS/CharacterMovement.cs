using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {
	public float xSpeed = 6f;
	public Camera cam;
	SpriteRenderer Player;
	Rigidbody2D myRigidbody;
	Animator myAnimator;
	bool canJump = false;
	public float MoveBurst;
	float xVelocity;
	float yVelocity;
	float TotalVelocity;
	bool isTouching = false;
	bool isWall = false;
	public float ybump = 1.2f;
	public bool win = false;
	public float moveSpeed1;
	public float yThresholdOne;
	public GameObject Bullet1;
	public GameObject Bullet2;
	public GameObject Bullet3;
	public GameObject Bullet4;
	public GameObject Bullet5;
	public GameObject Bullet6;
	public GameObject Bullet7;
	public GameObject Bullet8;
	public GameObject Bullet9;
	public GameObject Bullet10;
	public GameObject Bone1;
	public GameObject Bone2;
	public GameObject Bone3;
	public GameObject ContinueButton;
	public int bulletNum = 3;
	public int groundEffectDelay = 6;
	int fuck = 0;
	public AudioClip BulletSound;
	public AudioClip Oof;
	List<GameObject> bulletList = new List<GameObject>();
	int boneNum = 3;
	public string NextLevel;
	bool damageable = false;


	//bool canJump = false;
	// Use this for initialization
	public Rigidbody2D Rb2d{
		get { return myRigidbody;}
	}
	void Start () {

		Player = GetComponent<SpriteRenderer>();
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator> ();
		for(int i = 0; i < bulletNum; i++){
			if (i == 0) {
				Instantiate (Bullet1);
				bulletList.Add(Bullet1);
			}
			if (i == 1) {
				Instantiate (Bullet2);
				bulletList.Add(Bullet2);
			}
			if (i == 2) {
				Instantiate (Bullet3);
				bulletList.Add(Bullet3);
			}
			if (i == 3) {
				Instantiate (Bullet4);
				bulletList.Add(Bullet4);
			}
			if (i == 4) {
				Instantiate (Bullet5);
				bulletList.Add(Bullet5);
			}
			if (i == 5) {
				Instantiate (Bullet6);
				bulletList.Add(Bullet6);
			}
			if (i == 6) {
				Instantiate (Bullet7);
				bulletList.Add(Bullet7);
			}
			if (i == 7) {
				Instantiate (Bullet8);
				bulletList.Add(Bullet8);
			}
			if (i == 8) {
				Instantiate (Bullet9);
				bulletList.Add(Bullet9);
			}
			if (i == 9) {
				Instantiate (Bullet10);
				bulletList.Add(Bullet10);
			}


		}
				ContinueButton.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("X Velocity: " + myRigidbody.velocity.x);
		if(myRigidbody.velocity.x > 10 || myRigidbody.velocity.x < -10){
			damageable = true;
			//Debug.Log ("BECOMING DAMAGEABLE");
		}else{
			damageable = false;
		}
		if (isTouching && groundEffectDelay > 5 && !isWall) {
			myAnimator.Play ("Idle");
		}	else if (!isTouching && !win && !damageable) {
			myAnimator.Play ("Air");
		} else if (!isTouching && !win && damageable){
			myAnimator.Play("Danger_Air");
		} else if (win) {
			myAnimator.Play ("win");
			ContinueButton.SetActive(true);
			/*else if(ContinueButton.transform.position.y > yThresholdTwo){
				ContinueButton.transform.position += new Vector3(0,moveSpeed2,0);
			}*/
		}
		/*if (Input.GetKey (KeyCode.RightArrow)) {
			myRigidbody.velocity = new Vector2 (xSpeed, myRigidbody.velocity.y);
			Player.flipX = false;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			myRigidbody.velocity = new Vector2 (-xSpeed, myRigidbody.velocity.y);
			Player.flipX = true;
		}
		if(Input.GetKeyDown (KeyCode.UpArrow) && canJump){
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 20);
			canJump = false;
		}*/
		if (Input.GetMouseButtonDown (0) && !win) {
			//TO DO.  Prevent any of this from happening if its in a UI Zone
			if (bulletNum > 0) {
				Destroy (bulletList [bulletNum-1]);
				//Debug.Log ("buletNum - 1: " + (bulletNum-1));
				fuck++;
				bulletNum--;
				//Debug.Log ("fuck");
				/*if (bulletNum == 4)
				if (bulletNum == 3) {
					Destroy (Bullet1);
					bulletNum--;
				}
				else if (bulletNum == 2) {
					Destroy (Bullet2);
					bulletNum--;
				}
				else if (bulletNum == 1) {
					Destroy (Bullet3);
					bulletNum--;*/

			  GetComponent<AudioSource>().PlayOneShot(BulletSound, 1);
				//Debug.Log ("LEFT CLICK");
				//BELOW converts transform to pixel location, to do the directional movement
				Vector3 TempPixelPos = cam.WorldToScreenPoint (transform.position);
				xVelocity = (Input.mousePosition.x - TempPixelPos.x);
				yVelocity = (Input.mousePosition.y - TempPixelPos.y);
				TotalVelocity = Mathf.Sqrt ((xVelocity * xVelocity) + (yVelocity * yVelocity));
				xVelocity = (xVelocity / TotalVelocity) * MoveBurst;
				yVelocity = ((yVelocity / TotalVelocity) * MoveBurst) * ybump;
				//Debug.Log("VELOCITY ABOUT TO CHANGE");
				//Add if in the air, set if on the ground
				if(isTouching){
					myRigidbody.velocity = new Vector2 (-xVelocity, -yVelocity);
				}
				else{
					myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x + -xVelocity, -yVelocity);
				}
				//prevents horizontal momentum from stopping itself on the ground
				groundEffectDelay = 0;
				/*if (!canJump) {
					myRigidbody.velocity += new Vector2 (-xVelocity, 0);
				}*/

			}
		}else {
			if (isTouching && groundEffectDelay > 5 ) {
				//Gradually get up to or down to 5f
				if(myRigidbody.velocity.x > 5.5f){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x -0.5f, myRigidbody.velocity.y);
				}
				else if(myRigidbody.velocity.x < 4.5f){
					myRigidbody.velocity = new Vector2(myRigidbody.velocity.x +0.5f, myRigidbody.velocity.y);
				}else{
					myRigidbody.velocity = new Vector2(5f, myRigidbody.velocity.y );
				}

			}

		}
		groundEffectDelay++;



	}
	void OnCollisionEnter2D(Collision2D collisioninfo){
		//To tell if the player is above the top of the object or not (Center position plus half of the rectangels height)
		Debug.Log("COLLIDING");
		Debug.Log("CURRENT TAG" + collisioninfo.gameObject.tag);
		/*if (collisioninfo.gameObject.tag == "Jumpable") {
			canJump = true;
		}*/
		float yscale = collisioninfo.transform.localScale.y;
		//Debug.Log("Current Object y Position: " + collisioninfo.transform.position.y);
		//Debug.Log("Current Object y Size: " +  yscale);
		//Debug.Log("Current Object y Size over 2: " + (yscale / 2f));
		//Debug.Log("Current Object y + height over 2: " + (collisioninfo.transform.position.y + (yscale / 2f)));
		if (collisioninfo.gameObject.tag == "FLOOR") {

			if(transform.position.y - ( transform.localScale.y / 2f) < collisioninfo.transform.position.y + (collisioninfo.transform.localScale.y / 2f)){
				Debug.Log("Not Above");
				if(damageable){
					GetComponent<AudioSource>().PlayOneShot(Oof, 1);
					Destroy(Bone1);
				}
			}
			if(transform.position.y - ( transform.localScale.y / 2f) > collisioninfo.transform.position.y + (collisioninfo.transform.localScale.y / 2f)){
				Debug.Log("Above");
				isTouching = true;
			}

		}
		if (collisioninfo.gameObject.tag == "WIN") {
			win = true;
			myAnimator.Play ("win");
		}
		if (collisioninfo.gameObject.tag == "WALL" && damageable) {
			isWall = true;

		}
		if (collisioninfo.gameObject.tag == "NEWBULLET") {
			Destroy(collisioninfo.gameObject);
		}
		//Debug.Log ("FART");
		//myRigidbody.velocity = new Vector2 (0, 0);
	}
	void OnCollisionExit2D(Collision2D collisioninfo){
		if(transform.position.y - ( transform.localScale.y / 2f) > collisioninfo.transform.position.y + (collisioninfo.transform.localScale.y / 2f)){
			Debug.Log("Above after leaving");
			isTouching = false;
		}

		isWall = false;
	}

}
