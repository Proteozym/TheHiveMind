using UnityEngine;
using System.Collections;

namespace AntHill
{
	public class WorkerAnt : AntAI, InterfaceAI
	{

		public WorkerAnt ()
		{
		}

		public Vector3 getNextMovement(){		

			decideMovement ();
			return nextMovementTarget;
		}
		public void handleCollission(Collider other){
			return;
		}
		public void reset() {
			base.reset ();
		}
		public void supply() {
			currentMovements = 0;
			memory.supply ();
		}

		private void decideMovement() {
			if (isIdle) {
				nextMovementTarget = currentPosition;
				return;
			}
			if (!hasReachedNextPosition()) {
				return;
			}
			if(!hasReachedNextPosition()){
			}
			if (currentPosition == memory.foodtoCollect.foodObject.transform.position && !returnHome) {
				goHome();
			}

			
			if (returnHome) {
				findPathHome ();
				return;
			} 

			switch (memory.currentTask)
			{
			case 0:
				setIdle(true);
				break;
			case 3:
				collect();
				break;
			default:
				Debug.Log("Task not found!");
				break;
			}

		}
		private void collect(){
			currentMovements++;
			nextMovementTarget = memory.foodtoCollect.path.getMovement(currentMovements);
			memory.currentPath.addMovement (nextMovementTarget);
		}
	}
}

