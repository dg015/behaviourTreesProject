using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class LockInAt : ActionTask {

		public BBParameter<Transform> target;
		public LineRenderer lineR;
        public LineRenderer lineL;
		public Transform barrelR;
        public Transform barrelL;
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
			if(Vector3.Distance(agent.transform.position,target.value.position) < range)
			{
				lockInPlayer();
            }
			else
			{
				lineR.enabled = false;
                lineL.enabled = false;
			}
		}


		private void lockInPlayer()
		{
			agent.transform.LookAt(target.value);
			lineR.enabled = true;
			lineL.enabled = true;

			lineR.SetPosition(0, barrelR.position);
			lineR.SetPosition(1, target.value.position);

            lineL.SetPosition(0, barrelL.position);
            lineL.SetPosition(1, target.value.position);

        }


	}
}