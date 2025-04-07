using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class DischargeAT : ActionTask {
		public BBParameter<float> TurretEnergy;
		public float maxEnergy= 50;
		public float energyDrainModifier = 1;
		public BBParameter<bool> isBeingCharged = false;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			drain();
		}

		private void drain()
		{
			if(TurretEnergy.value <maxEnergy && TurretEnergy.value >0 && !isBeingCharged.value)
			{
				TurretEnergy.value -= Time.deltaTime * energyDrainModifier;
			}
		}

	}
}