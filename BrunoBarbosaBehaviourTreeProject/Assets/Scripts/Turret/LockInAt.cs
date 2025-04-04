using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class LockInAt : ActionTask {

		public BBParameter<Transform> target;
		public LineRenderer line1;
        public LineRenderer line2;
		public Transform barrel1;
        public Transform barrel2;
		public float range;

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
			lockInPlayer();

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
		
		private void lockInPlayer()
		{
            agent.transform.LookAt(target.value);
			line1.enabled = true;
            line2.enabled = true;

			line1.SetPosition(0, barrel1.position);
            line1.SetPosition(1, target.value.position);
        }
		
	}
}