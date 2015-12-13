using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {


	protected NavMeshAgent		agent;
	protected Animator			animator;

	protected Locomotion locomotion;
	bool done =false;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.avoidancePriority = Random.Range (0, 100);

		animator = GetComponent<Animator>();
		locomotion = new Locomotion(animator);

		SetDestination ();
	}

	protected void SetDestination()
	{
		done = false;
		float dist = Random.Range(5,60);
		Vector3 randomDirection = Random.insideUnitSphere * dist;
		randomDirection.y = this.transform.position.y;
		randomDirection += this.transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, dist,1);
		
		agent.destination = hit.position;

	}

	protected void SetupAgentLocomotion()
	{
	
		if (AgentDone() && !done)
		{
			done = true;
			locomotion.Do(0, 0);
			Invoke("SetDestination",Random.Range(0f,4f));

		}
		else
		{
			float speed = agent.desiredVelocity.magnitude;

			Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;

			float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;

			locomotion.Do(speed, angle);
		}
	}

    void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
		transform.rotation = animator.rootRotation;
    }

	protected bool AgentDone()
	{
		return !agent.pathPending && AgentStopping();
	}

	protected bool AgentStopping()
	{
		return agent.remainingDistance <= agent.stoppingDistance;
	}

	// Update is called once per frame
	void Update () 
	{


		SetupAgentLocomotion();
	}
}