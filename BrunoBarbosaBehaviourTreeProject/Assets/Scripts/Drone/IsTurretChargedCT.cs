using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class IsTurretChargedCT : ConditionTask {
		//energy
		public Blackboard Turret;
		public float TurretEnegergy;
		public float LowEnergy;
		//timer
		public float analyzeTime;
		private float RunTime;

		protected override void OnEnable() {
			RunTime = 0;
		}

		private void analyze()
		{
			if(RunTime>= analyzeTime)
			{
				CheckTurretBattery();
			}
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