using NodeCanvas.Framework;
using ParadoxNotion.Design;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class IsPlayerInRadiusCT : ConditionTask {

        public BBParameter<Transform> target;
        public BBParameter <float> range;
        public BBParameter<bool> isCharging;

        //Called once per frame while the condition is active.
        //Return whether the condition is success or failure.
		protected override bool OnCheck() {
            if (Vector3.Distance(agent.transform.position, target.value.position) < range.value && !isCharging.value)
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