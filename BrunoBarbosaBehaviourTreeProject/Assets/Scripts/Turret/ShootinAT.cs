using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ShootinAT : ActionTask {

        public BBParameter<Transform> player;
        public GameObject bullet;
        public float TimeBetweenShots;
        public Transform barrel;
        private float TimeSinceLastShot;
        public int BurstCount;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            if(TimeSinceLastShot >= TimeBetweenShots)
            {
                shoot();
            }

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }

        private void shoot()
        {
            for(int i = 0; i < BurstCount; i++)
            {
                
            }
        }

        private void fireBullet()
        {
            GameObject.Instantiate(bullet, barrel);
        }

    }
}