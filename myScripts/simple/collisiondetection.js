function OnCollisionEnter(theCollision : Collision){
	
	if(theCollision.gameObject.name == "floor"){
		Debug.Log("hit the floor");
	}
	else if(theCollision.gameObject.name == "sword"){
		Debug.Log("hit the sword");
	}
}