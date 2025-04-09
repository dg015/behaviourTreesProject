using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class RetreatAT : ActionTask {

		public Transform turretChargeLocation;
		public BBParameter<NavMeshAgent> navAgent;
		public float retreatDistance;

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
			Retreat();
			Debug.Log("retreating");

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		private void Retreat()
		{
			navAgent.value.SetDestination(turretChargeLocation.position);
			if(Vector3.Distance(agent.transform.position, turretChargeLocation.position) < retreatDistance )
			{
				EndAction(true);
			}
		}
	}
}