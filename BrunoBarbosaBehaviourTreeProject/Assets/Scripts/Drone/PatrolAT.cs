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
        public float catchDistance = 10f;

        //flying
        public float DefaultAirHeight = 3.61f;
        public float ascendSpeed = 2f;

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
            hasBeenFound = false;
            CurrentPatrolPoint = 0;
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            CheckForPlayer();
            AirControl();
            if (!hasBeenFound)
            {
                TravelToPoints();
            }
            FinishPatrol();

            //Flying();
        }

        private void AirControl()
        {
            float speed = Mathf.Clamp(Vector3.Distance(agent.transform.position, patrolPoints[CurrentPatrolPoint].transform.position) / ascendSpeed, .2f, 1f);
            // if player is not being chased stay at default, if go down to player level
            float newY = Mathf.Lerp(agent.transform.position.y, patrolPoints[CurrentPatrolPoint].transform.position.y, Time.deltaTime * speed);
            agent.transform.position = new Vector3(agent.transform.position.x, newY, agent.transform.position.z);

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

        public void CheckForPlayer()
        {
            if(Vector3.Distance(agent.transform.position,Player.value.position)< catchDistance)
            {
                Vector3.Distance(agent.transform.position, Player.value.position);
                hasBeenFound = true;
                EndAction(false);
            }
        }
    }

}