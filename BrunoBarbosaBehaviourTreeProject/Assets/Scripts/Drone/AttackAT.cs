using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class AttackAT : ActionTask {

		public GameObject player;
		private Rigidbody PlayerRB;
		private Transform PlayerTransform;

		//explosioin
		public float power = 16;
		public float radius = 10;

		//Telegraphing
		public GameObject barrel;
		public Renderer BarrelRender;

		//timer 
		public float telegraphingTimer;
        public float TimerLimit;

		//navmesh
		public NavMeshAgent navAgent;

		//animation
		public Animator Anim;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			PlayerRB = player.GetComponent<Rigidbody>();
            PlayerTransform = player.GetComponent<Transform>();


            return null;
		}
		
		private void telegraphing()
		{
			Anim.SetTrigger("DroneHit");
			telegraphingTimer = Mathf.Clamp(telegraphingTimer, 0, TimerLimit);
			if(telegraphingTimer != TimerLimit)
			{
				BarrelRender.material.SetColor("_EmissionColor", Color.red);
				telegraphingTimer += Time.deltaTime;
			}
			else if(telegraphingTimer == TimerLimit)
			{
				Attack();
				EndAction(true);
			}

		}

		protected override void OnUpdate()
		{
			telegraphing();
		}

        private void Attack()
		{
			Debug.Log("attacked");
			PlayerRB.AddExplosionForce( power,agent.transform.position,radius, 5 );	
		}
	}
}