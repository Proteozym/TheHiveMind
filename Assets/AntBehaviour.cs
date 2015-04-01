using UnityEngine;
using System.Collections;

namespace AntHill
{
	public class AntBehaviour : MonoBehaviour {

		public float speed;
		public int maxMovements;
		public float maxTrvl;
		public InterfaceAI ant;


		private Vector3 nextMovementTarget;
		private float startTime;


		public void init(string task, Vector3 antHill, AntMemory mem) {
			switch (mem.getType())
			{
			case "Searcher":
				ant = new SearcherAnt();
				break;
			case "Worker":
				ant = new WorkerAnt();
				break;
			default:
				Debug.Log("Type not found!");
				break;
			}
			ant.init (maxMovements, maxTrvl, mem, transform.position);
		}

		private void setAntType() {

		}

		
		// Use this for initialization
		void Start () {

		}
		
		// Update is called once per frame
		void Update () {
			ant.updatePosition (transform.position);

			if(ant.hasReachedNextPosition() || ant.idle()){
				ant.setIdle(false);
			}
			Vector3 newMovementTarget = ant.getNextMovement();

			if (newMovementTarget != nextMovementTarget) {
				startTime = Time.time;
				nextMovementTarget = newMovementTarget;
			}

			transform.position = Vector3.Lerp(transform.position, nextMovementTarget, (Time.time - startTime) * speed);
		}

		void OnTriggerEnter(Collider other) {
			ant.handleCollission(other);

		}

		public void resetAnt(){
			ant.reset ();
		}

		public bool isReturnedHome() {
			return ant.isReturnedHome ();
		}



	}
}
