#pragma strict
#pragma implicit
#pragma downcast

var emitters : ParticleEmitter[];
var collisionDamping = 0.00;
		if(e.minSize > 0.02)
		{
		}
		
		
		if(timer > emitTimeOut || e.maxSize < minSize) e.emit = false;
	
	var hit : RaycastHit;
	if(Physics.Raycast(transform.position, velocity, hit, velocity.magnitude))
	{
		velocity = Vector3.Reflect(velocity, hit.normal) * collisionDamping;
	}
	