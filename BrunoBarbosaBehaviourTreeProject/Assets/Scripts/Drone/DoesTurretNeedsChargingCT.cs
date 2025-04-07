using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

    public class DoesTurretNeedsChargingCT : ConditionTask {
        //energy
        public Blackboard Turret;
        private float TurretEnegergy;
        public float LowEnergy;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}


        private bool CheckTurretBattery()
        {

            TurretEnegergy = Turret.GetVariableValue<float>("Energy");
            if (TurretEnegergy >= LowEnergy)
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
        protected override bool OnCheck()
        {
            if (CheckTurretBattery())
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