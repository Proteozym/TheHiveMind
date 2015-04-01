using UnityEngine;
using System.Collections;
namespace AntHill
{
	public class AntMemory
	{
		public Food[] knownFood = new Food[0];
		public Food foodtoCollect = new Food();
		public Food foundFood = null;

		public Vector3 antHillPosition;
		public Path currentPath = new Path();
		public int currentTask = 0;
		public Vector3 startOptimizationPos;

		private string antType;

		public AntMemory (string type)
		{
			antType = type;
		}
		public bool hasFoundFood(){
			return foundFood != null;
		}
		public void rememberAntHillPos(Vector3 position) {
			antHillPosition = position;
			currentPath.addMovement (antHillPosition);
		}

		public void reset() {
			knownFood = new Food[0];
			foodtoCollect = new Food();
			foundFood = null;		
			currentPath = new Path();
			currentTask = 0;

			rememberAntHillPos (antHillPosition);
		}
		public void supply() {	
			currentPath = new Path();			
			rememberAntHillPos (antHillPosition);
		}

		public string getType(){
			return antType;
		}
		public void setTask(int task){
			currentTask = task;
		}
	}
}

