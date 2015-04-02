#pragma strict

var layermask : LayerMask;
var target: Transform;
var height : float;
var distance : float;

var x: float = 0;
var y: float = 0;
var yMin : float;
var yMax : float;

function LateUpdate () {
	x += 10* Input.GetAxis("Mouse X");
	y -= 10* Input.GetAxis("Mouse Y");
	if(y > yMax){
		y = yMax;
	}
	else if(y < yMin){
		y = yMin;
	}
	
	var rotation = Quaternion.Euler(y,x,0);
	var position = rotation * Vector3(0.0,height, -distance) + target.position;
	
	transform.rotation = rotation;
	transform.position = position;
	
	var hit : RaycastHit;
	if(Physics.Linecast(target.position,transform.position, hit, layermask)){
		var tempDistance = Vector3.Distance(target.position, hit.point);
		position = rotation * Vector3(0.0, height, -tempDistance) + target.position;
		transform.position = position;
	}
}