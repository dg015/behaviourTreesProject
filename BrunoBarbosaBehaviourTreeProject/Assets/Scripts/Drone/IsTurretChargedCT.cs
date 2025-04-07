using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class IsTurretChargedCT : ConditionTask {
		//energy
		public Blackboard Turret;
		public float TurretEnergy;
		public float LowEnergy;

		private bool CheckTurretBattery()
		{
			//get turret energy from other blackboard
			TurretEnergy = Turret.GetVariableValue<float>("Energy");
			//check if it has enough energy
			if(TurretEnergy <= LowEnergy)
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
			
			if(CheckTurretBattery())
			{
				
				return true;
			}
			else
			{
				return false;
			}
			
		}
	}
}