
var defaultspeed : float = 15;
var airspeed : float = 3.0;
var jumpSpeed : float = 8.0;
var gravity : float = 20.0;
var targetObject : GameObject;
var target : Transform;
var rotationspeed : int;
var stopsmooth : float = 5;
var smooth : float = 5;
var antiBumpFactor : float = .75;


private var speed:float;
private var myTransform : Transform;
private var distance : float;
private var moveDirection : Vector3 = Vector3.zero;

function Awake(){
	myTransform = transform;

}

 
function Update() {

	
	distance = Vector3.Distance(targetObject.transform.position, transform.position);
    var controller : CharacterController = GetComponent(CharacterController);
    
    
    //Debug.Log(speed);
    if (controller.isGrounded ) {
    	
        if(distance >0){
        	moveDirection = Vector3(0, -antiBumpFactor, 1);
        	if(speed <defaultspeed){
        		speed = Mathf.MoveTowards(speed, defaultspeed, smooth * Time.deltaTime);
        	}
        	rotate();
        	
        	
        	if(Input.GetKey(KeyCode.LeftShift)){
    		speed = Mathf.MoveTowards(speed, defaultspeed * 2, smooth * Time.deltaTime);

   			}
   			else if (!Input.GetKey(KeyCode.LeftShift) && speed>defaultspeed) {
    			speed = Mathf.MoveTowards(speed, defaultspeed, smooth * Time.deltaTime);
    		}
        }
        else{
        	moveDirection = Vector3(0, 0, 0);
        	speed = Mathf.MoveTowards(speed, 0.0, stopsmooth * Time.deltaTime);
        	
        }
        //moveDirection.y += fyRot;
        moveDirection = transform.TransformDirection(moveDirection);
        
        
        
        moveDirection *= speed;
 
        if (Input.GetButton ("Jump")) {
            moveDirection.y = jumpSpeed;

        }
    }
   
 	else if (!controller.isGrounded ) {
 	if(distance >0){
    moveDirection2 = Vector3 (0, 0, 1);
    rotate();
    }
    else{
    	moveDirection2 = Vector3 (0, 0, 0);
    }
    moveDirection2 = transform.TransformDirection(moveDirection2);
    moveDirection2.x *= airspeed;
    moveDirection2.z *= airspeed;
}
    // Apply gravity
    moveDirection.y -= gravity * Time.deltaTime;
 
    // Move the controller
    controller.Move(Vector3.Lerp(new Vector3(0,0,0) ,(moveDirection + moveDirection2 ) * Time.deltaTime, 100f));
}
function rotate(){
	myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationspeed *Time.deltaTime);
}
