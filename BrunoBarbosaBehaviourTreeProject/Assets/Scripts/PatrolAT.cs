using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;


namespace NodeCanvas.Tasks.Actions {

	public class PatrolAT : ActionTask {

        //points
        public Transform[] patrolPoints;
        public BBParameter<Transform> Player;
        public float DetectionRadius;
        public int NumberOfPointsTraveled;
        public int CurrentPatrolPoint;
        //speed
        public float arrivalDistance;
        public NavMeshAgent navAgent;
        //catching the player
        public bool hasBeenFound;
        //flying
        public float DefaulFlyingHeight;
        public float DescendDistance;
        public float currentFlyingHeight;
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
            CurrentPatrolPoint = 0;
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            if (!hasBeenFound)
            {
                TravelToPoints();
            }
            FinishPatrol();

            //Flying();
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }


        public void TravelToPoints()
        {
            navAgent.SetDestination(patrolPoints[CurrentPatrolPoint].transform.position);
            if (Vector3.Distance(agent.transform.position, patrolPoints[CurrentPatrolPoint].transform.position) < arrivalDistance)
            {
                Debug.Log("Arrived");
                CurrentPatrolPoint += 1;
            }
        }

        public void FinishPatrol()
        {
            if (CurrentPatrolPoint >= patrolPoints.Length)
            {
                Debug.Log("finishedPatrol");
                EndAction(true);
            }
        }

        public void Flying()
        {
            if (Vector3.Distance(agent.transform.position, patrolPoints[CurrentPatrolPoint].transform.position) < DescendDistance)
            {
                currentFlyingHeight = patrolPoints[CurrentPatrolPoint].transform.position.y + 1;
            }
            else
            {
                currentFlyingHeight = DefaulFlyingHeight;
            }
            agent.transform.position = new Vector2(0,currentFlyingHeight);
            
        }
    }

}