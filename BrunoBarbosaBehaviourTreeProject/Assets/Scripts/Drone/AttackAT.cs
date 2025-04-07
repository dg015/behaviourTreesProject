using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class AttackAT : ActionTask {

		public GameObject player;
		private Rigidbody PlayerRB;
		private Transform PlayerTransform;

		//explosioin
		public float power = 16;
		public float radius = 10;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			PlayerRB = player.GetComponent<Rigidbody>();
            PlayerTransform = player.GetComponent<Transform>();


            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			Attack();
			EndAction(true);
		}
		

		private void Attack()
		{
			Debug.Log("attacked");
			PlayerRB.AddExplosionForce( power,agent.transform.position,radius, 5 );	
		}
	}
}