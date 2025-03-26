using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ScanAT : ActionTask {
		public float Radius;
		public LayerMask mask;
		public BBParameter<Transform> target;

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
			detect();
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		public void detect()
		{

            Collider[] detections = Physics.OverlapSphere(agent.transform.position, Radius, mask);
			if(detections.Length > 0 )
			{
				Debug.Log("detected");
				LockAtTarget();
                target.value = detections[0].transform;
                EndAction(true);
                
            }


        }
		private void LockAtTarget()
		{
			agent.transform.LookAt(target.value);
		}
		
	}
}