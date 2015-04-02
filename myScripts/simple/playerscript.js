#pragma strict
var movespeed:float;
var rotspeed:float;
var jumpspeed:float;


function Start () {

}

function Update () {

	if(Input.GetButton("forward")){
		transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
	}
	if(Input.GetButton("backward")){
		transform.Translate(-Vector3.forward * movespeed * Time.deltaTime);
	}
	if(Input.GetButton("right")){
		transform.Rotate(Vector3.up * rotspeed * Time.deltaTime);
	}
	if(Input.GetButton("right")){
		transform.Translate(Vector3.right * movespeed * Time.deltaTime);
	}
	if(Input.GetButton("left")){
		transform.Rotate(-Vector3.up * rotspeed * Time.deltaTime);
	}
	if(Input.GetButton("left")){
		transform.Translate(Vector3.left * movespeed * Time.deltaTime);
	}
	if(Input.GetButtonDown("Jump")){
		transform.GetComponent.<Rigidbody>().AddForce(Vector3.up * jumpspeed );
	}

}
