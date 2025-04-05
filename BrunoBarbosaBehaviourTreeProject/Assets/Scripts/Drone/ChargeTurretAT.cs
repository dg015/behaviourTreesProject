using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace NodeCanvas.Tasks.Actions {

	public class ChargeTurretAT : ActionTask {

		//agent movement
		public BBParameter<NavMeshAgent> navAgent;
		public Transform TurretTransform;
		public Transform TurretChargeLocation;

		//turret energy
        public float TurretEnergy;
        public float MaxTurretEnergy;
        public Blackboard Turret;
		public float energyChargeModifier;
        public bool IsCharging;

        //flying
        public float DefaultAirHeight = 3.61f;
        public float ascendSpeed = 2f;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}


		private void charge()
		{
            if (Vector3.Distance(agent.transform.position, TurretTransform.position) < 2)
            {
                Debug.Log("chargin");
                IsCharging = true;
                //Turret.SetVariableValue("Energy", TurretEnergy);
                TurretEnergy += Time.deltaTime * energyChargeModifier;


            }
            if(TurretEnergy >= MaxTurretEnergy )
            {
                IsCharging=false;
                EndAction(true);
            }
        }

        protected override void OnExecute()
        {
            
            TurretEnergy = 0;

        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {
            //get variables
            //IsCharging = Turret.GetVariableValue<bool>("IsBeingCharged");
            //TurretEnergy = Turret.GetVariableValue<float>("Energy");
            MaxTurretEnergy = Turret.GetVariableValue<float>("MaxEnergy");

            //set variables
            Turret.SetVariableValue("Energy", TurretEnergy);
            Turret.SetVariableValue("IsBeingCharged", IsCharging);

            //call stuff
            navAgent.value.SetDestination(TurretChargeLocation.position);
			AirControl();
            charge();
        }

		//Called when the task is disabled.
		protected override void OnStop() {
            IsCharging = false;
            Turret.SetVariableValue("IsBeingCharged", IsCharging);
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        private void AirControl()
        {
            float speed = Mathf.Clamp(Vector3.Distance(agent.transform.position, TurretChargeLocation.position) / ascendSpeed, .2f, 1f);
            // if player is not being chased stay at default, if go down to player level
            float newY = Mathf.Lerp(agent.transform.position.y, TurretChargeLocation.position.y, Time.deltaTime * speed);
            agent.transform.position = new Vector3(agent.transform.position.x, newY, agent.transform.position.z);

        }

    }
}