using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class IsTurretChargedCT : ConditionTask {
		//energy
		public Blackboard Turret;
		public float TurretEnegergy;
		public float LowEnergy;
		//timer


		protected override void OnEnable() {

		}



		private bool CheckTurretBattery()
		{
			TurretEnegergy = Turret.GetVariableValue<float>("Energy");
			if(TurretEnegergy <= LowEnergy)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			Debug.Log("Im running");
			if(CheckTurretBattery())
			{
				Debug.Log("returned true");
				return true;
			}
			else
			{
				return false;
			}
			
		}
	}
}