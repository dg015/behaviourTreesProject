using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace NodeCanvas.Tasks.Actions {

	public class ChargeTurretAT : ActionTask {
		public NavMeshAgent navAgent;
		public Transform TurretTransform;

        public float TurretEnegergy;
        public Blackboard Turret;
		public float energyChargeModifier;
        //flying
        public float DefaultAirHeight = 3.61f;
        public float ascendSpeed = 2f;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			if(Vector3.Distance(agent.transform.position, TurretTransform.position) > 2)
			{
				

                    TurretEnegergy += Time.deltaTime * energyChargeModifier;
            }
		}

        //Called once per frame while the action is active.
		protected override void OnUpdate() {
            TurretEnegergy = Turret.GetVariableValue<float>("Energy");
            navAgent.SetDestination(TurretTransform.position);
			AirControl();
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        private void AirControl()
        {
            float speed = Mathf.Clamp(Vector3.Distance(agent.transform.position, TurretTransform.position) / ascendSpeed, .2f, 1f);
            // if player is not being chased stay at default, if go down to player level
            float newY = Mathf.Lerp(agent.transform.position.y, TurretTransform.position.y, Time.deltaTime * speed);
            agent.transform.position = new Vector3(agent.transform.position.x, newY, agent.transform.position.z);

        }

    }
}