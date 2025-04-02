using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ChaseAT : ActionTask {
		//basic direction to player
		private Vector3 direction;
		public Transform player;
		// zig zagging
		private Vector3 NextLocation;
		public float offset;
		public float stepSize;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			Move();
		}

		private void Move()
		{
            direction = player.transform.position - agent.transform.position;
            Debug.DrawLine(agent.transform.position, agent.transform.position + direction);

            //zig zag

            Vector3 adaptedStepDistance = new Vector3(offset, 0);

            Vector2 baseTriangle = direction - (direction - adaptedStepDistance);
            Vector3 NextPoint = baseTriangle / 2 + new Vector2(offset, 0);


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
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}