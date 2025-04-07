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

        //material
        public Renderer render;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

        protected override void OnExecute()
        {
            //get variables and set color blue for signfier
            render = agent.GetComponentInChildren<Renderer>();
            MovementControls.ChangeColour(render, Color.blue);
        }


        private void charge()
		{
            //check if the drone is close enough to charge
            if (Vector3.Distance(agent.transform.position, TurretTransform.position) < 2)
            {
                //set variable to true so that in the discharge script so that it doesnt remove battery while charing
                IsCharging = true;
                TurretEnergy += Time.deltaTime * energyChargeModifier;
            }
            //if it has enough energy set is charging as false and set energy variable to turret blackboard
            if(TurretEnergy >= MaxTurretEnergy )
            {
                IsCharging=false;
                Turret.SetVariableValue("IsBeingCharged", IsCharging);
                EndAction(true);
            }
        }


        //Called once per frame while the action is active.
        protected override void OnUpdate() {
            //get the energy variable from the the turret blackboard
            MaxTurretEnergy = Turret.GetVariableValue<float>("MaxEnergy");

            //set variables
            Turret.SetVariableValue("Energy", TurretEnergy);
            Turret.SetVariableValue("IsBeingCharged", IsCharging);

            //call functions
            navAgent.value.SetDestination(TurretChargeLocation.position);
            //get air control from static script
            MovementControls.AirControl(agent.transform, TurretChargeLocation, ascendSpeed);
            charge();
        }

		//Called when the task is disabled.
		protected override void OnStop() {
            //set charing as false a second time to make sure
            IsCharging = false;
            Turret.SetVariableValue("IsBeingCharged", IsCharging);
        }



    }
}