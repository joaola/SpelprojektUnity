var target : Transform;
var distance = 10.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -0;
var yMaxLimit = 80;


private var x = 0.0;
private var y = 0.0;

@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {
	
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}

function LateUpdate () {
	 var dist = Vector3.Distance(target.position, transform.position);
	var up = transform.TransformDirection(Vector3.up);
	Debug.DrawRay(transform.position, -up * 10, Color.green);
	/*if(Physics.Raycast(transform.position,-up,1) && distance>3 ){
		distance -= Time.deltaTime * 15;
	}
	else if (dist<10){
		distance += Time.deltaTime * 15;
	}*/



	if(Input.GetKey(KeyCode.T)){
		transform.rotation = target.transform.rotation ;
		transform.LookAt(target);
	}
	else{
	
    if (target) {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
 		
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * Vector3(0.0, 0.0, -distance) + target.position;
        
        transform.rotation = rotation;
        transform.position = position;
    }
    }
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}