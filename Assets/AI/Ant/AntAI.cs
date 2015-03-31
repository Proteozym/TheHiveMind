using UnityEngine;
using System.Collections;

namespace AntHill
{
	public abstract class AntAI : Behaviour
	{
		public bool initCommunication = false;

		protected Vector3 nextMovementTarget = new Vector3();
		protected int currentMovements = 0;
		protected bool returnHome = false;
		protected Vector3 currentPosition;
		protected int maxMovements = 0;
		protected float maxTrvl;
		protected bool isIdle = true;
		protected AntMemory memory;

		public void init(int maxMvnts, float maxT, AntMemory mem, Vector3 curPos){
			maxMovements = maxMvnts;
			maxTrvl = maxT;
			updateInfo(mem);
			updatePosition (curPos);
			nextMovementTarget = currentPosition;
		}
		public bool isReturnedHome() {
			if (returnHome && currentMovements == 0) {
				return true;
			}
			return false;
		}
		public void setIdle(bool idle){
			isIdle = idle;
		}
		public void updatePosition(Vector3 position){
			currentPosition = position;
		}
		public bool hasReachedNextPosition(){
			return currentPosition == nextMovementTarget;
		}

		public void updateInfo(AntMemory mem){
			memory = mem;
		}
		public AntMemory getMemory(){
			return memory;
		}
		public bool idle(){
			return isIdle;
		}

		public bool isAtHomeBase(){
			return currentPosition == memory.antHillPosition;
		}
		public bool wantsToCommuicate(){
			return initCommunication;
		}
		protected void findPathHome(){
			if (nextMovementTarget == currentPosition) {
				nextMovementTarget = memory.currentPath.getMovement(currentMovements);
				currentMovements--;
			}
		}
		
		protected bool isValid(Vector3 position) {
			Vector3 direction = ( position - currentPosition).normalized;
			RaycastHit[] hits;
			hits = Physics.RaycastAll(currentPosition, direction, 100.0F);
			int i = 0;
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if(hit.transform.gameObject.tag == "Obstacle"){
					return false;
				}
				i++;
			}
			return true;
		}
		protected void goHome(){
			returnHome = true;
			initCommunication = true;
		}
		protected void reset() {
			returnHome = false;
			currentMovements = 0;
			memory.reset ();
			initCommunication = false;
		}

	}
}

