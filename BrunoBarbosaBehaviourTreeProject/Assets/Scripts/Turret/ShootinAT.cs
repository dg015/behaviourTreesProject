using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class ShootinAT : ActionTask {

        public BBParameter<Transform> player;
        public GameObject bullet;
        public float TimeBetweenShots;
        public Transform barrel;
        public float TimeSinceLastShot;
        public int BurstCount;

        //animation
        public Animator Anim;

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
            Anim.SetBool("Shooting", true);
            StartCoroutine(shoot());
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            Debug.Log("running");
            if (TimeSinceLastShot >= TimeBetweenShots)
            {
                TimeSinceLastShot = 0;
                StartCoroutine(shoot());
            }
            else
            {
                TimeSinceLastShot += Time.deltaTime;
            }

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            Anim.SetBool("Shooting", false);
        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }

        private IEnumerator shoot()
        {

            for (int i = 0; i < BurstCount; i++)
            {

                fireBullet();
                yield return new WaitForSeconds(.1f);
            }
        }

        /*
        private void shoot()
        {

        }
        */
        private void fireBullet()
        {
            GameObject.Instantiate(bullet, barrel.position,barrel.rotation);
        }

    }
}