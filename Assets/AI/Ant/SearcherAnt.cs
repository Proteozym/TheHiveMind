using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AntHill
{
	public class SearcherAnt : AntAI, InterfaceAI
	{

		public Vector3 getNextMovement(){
			decideMovement ();
			return nextMovementTarget;
		}
		public void handleFood(Collider other) {
			foreach(Food food in memory.knownFood){
				if (food.foodObject == other.gameObject){
					if (food.path.metric <= currentMovements && !food.isEmpty){
						return;
					}
					break;
				}
			}
			
			currentMovements++;
			nextMovementTarget = other.transform.position;
			memory.currentPath.addMovement (nextMovementTarget);
			
			Food foundFood = new Food();
			foundFood.foodObject = other.gameObject;
			foundFood.path = memory.currentPath;
			foundFood.path = memory.currentPath;
			if(other.gameObject.GetComponent<foodHandler> ().foodPoints <= 0){
				foundFood.isEmpty = true;
			}
			memory.foundFood = foundFood;
			Debug.Log("found Food");
			
		}
		public void handleCollission(Collider other){
			if (other.tag == "Food" && isValid(other.transform.position)) {
				handleFood(other);
			}
		}

		public void reset() {;
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
			if ((memory.hasFoundFood() || maxMovements <= currentMovements) && !returnHome) {
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
			case 1:
				search();
				break;
			case 2:
				optimize();
				break;
			default:
				Debug.Log("Task not found!");
				break;
			}
		}
		
		private void search() {
			currentMovements++;
			do{
				nextMovementTarget = currentPosition + new Vector3 ((Random.value - 0.5f) * maxTrvl, 0.0f, (Random.value - 0.5f) * maxTrvl);	
			}while(!isValid (nextMovementTarget));
			memory.currentPath.addMovement (nextMovementTarget);			
		}
		private void optimize() {

			Vector3 lastCoord = memory.foodtoCollect.path.path [memory.foodtoCollect.path.path.Count];
			
			if(currentPosition == lastCoord){
				goHome();
				return;
			}

			Vector3 direction = currentPosition + lastCoord;
			Vector3 clampedDir = Vector3.ClampMagnitude(direction, maxTrvl);

			if (isValid (clampedDir)) {
				nextMovementTarget = clampedDir;
				return;
			}

			nextMovementTarget = clampedDir;

		}
	}
}

