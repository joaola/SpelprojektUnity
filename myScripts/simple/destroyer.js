var target : GameObject;
var myTimer : float = 5.0;

function Update () {
	
	if(myTimer > 0){
		myTimer -= Time.deltaTime;
	}
	
	if(myTimer <=0){
		Destroy(target);
	}
}
