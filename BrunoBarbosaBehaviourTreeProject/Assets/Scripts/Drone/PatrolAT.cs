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
        public BBParameter <NavMeshAgent> navAgent;

        //catching the player
        public bool hasBeenFound;
        public float catchDistance = 10f;

        //flying
        public float DefaultAirHeight = 3.61f;
        public float ascendSpeed = 2f;

        //material
        public Renderer render;

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
            //get reference to variables and reset some others
            hasBeenFound = false;
            CurrentPatrolPoint = 0;
            render = agent.GetComponentInChildren<Renderer>();
            MovementControls.ChangeColour(render, Color.yellow);
            //EndAction(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            CheckForPlayer();
            //call function from static class 
            MovementControls.AirControl(agent.transform, patrolPoints[CurrentPatrolPoint], ascendSpeed);
            //if it has not being found go to the next point
            if (!hasBeenFound)
            {
                TravelToPoints();
            }
            FinishPatrol();
        }


        //set navmesh agent to next point
        public void TravelToPoints()
        {
            navAgent.value.SetDestination(patrolPoints[CurrentPatrolPoint].transform.position);
            if (Vector3.Distance(agent.transform.position, patrolPoints[CurrentPatrolPoint].transform.position) < arrivalDistance)
            {
                Debug.Log("Arrived");
                CurrentPatrolPoint += 1;
            }
        }

        public void FinishPatrol()
        {
            //check for the current point and the lenght of point arrays
            if (CurrentPatrolPoint >= patrolPoints.Length)
            {
                Debug.Log("finishedPatrol");
                EndAction(true);
            }
        }

        public void CheckForPlayer()
        {
            //check if they're close enough to the player, if so end script and se found true
            if(Vector3.Distance(agent.transform.position,Player.value.position)< catchDistance)
            {
                Vector3.Distance(agent.transform.position, Player.value.position);
                hasBeenFound = true;
                EndAction(false);
            }
        }
    }

}