using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class ChaseAT : ActionTask {
		//basic direction to player
		private Vector3 direction;
		public Transform player;
		// zig zagging
		private Vector3 NextLocation;
		public float offset;
		public float stepSize;

		// movement 
		public BBParameter<NavMeshAgent> navAgent;

		//air control
		public float DefaultAirHeight = 3.61f;
		public float ascendSpeed = 2f;

		//attack
		public float attackReach = 1f;
        public float TaclkeDistance = 3f;
		public float escapeDistance = 15f;

        //material
        public Renderer render;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            render = agent.GetComponentInChildren<Renderer>();
            MovementControls.ChangeColour(render, Color.red);
            //run it once at the begining to reach its first point
            if (Vector3.Distance(player.position, agent.transform.position) > escapeDistance)
			{
                EndAction(false);
            }
			else
			{
                Move();
            }
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            //only run it once it gets to the new point
            MovementControls.AirControl(agent.transform, player, ascendSpeed);
            
			if (navAgent.value.remainingDistance < .5)
			{
				
                offset = offset * -1;
                Move();
            }
            else if (Vector3.Distance(player.position, agent.transform.position) < TaclkeDistance )
            {
				
                navAgent.value.SetDestination(player.position);
            }
            if (Vector3.Distance(player.position,agent.transform.position) < attackReach)
			{
                Debug.Log("attack");
                EndAction(true);
			}
			if(Vector3.Distance(player.position, agent.transform.position) > escapeDistance)
			{
				EndAction(false);
			}
		}



		private void Move()
		{
			// first get the direction
            direction = player.transform.position - agent.transform.position;
            Debug.DrawLine(agent.transform.position, agent.transform.position + direction);

			//zig zag
			//adapt the offset for a vector 3 so it can be used in 3D
			Vector3 adaptedOffset = agent.transform.right * offset; // problem 

			// base triangle = player location + (direciton * offset) /2
			Vector3 baseTriangle = agent.transform.position + direction.normalized * stepSize;

			//get the mid point, that means its the top point in the triangle, that the drone will travel to
			Vector3 midPoint = (agent.transform.position+ baseTriangle) / 2;

			Vector3 NextPoint = midPoint + adaptedOffset;
			NextLocation = NextPoint;

            navAgent.value.SetDestination(NextLocation);

            Debug.DrawLine(agent.transform.position, NextPoint, Color.red);
        }

		/*if it was 2d it would be
		 * offset is a vector 2 (0,Y)
		 * New location = direction + offset * stepSize
		 * Offset = -1* offset
		 * Repeat
		 * For 3D
		 * 
		*/
		//Called when the task is disabled.

	}
}