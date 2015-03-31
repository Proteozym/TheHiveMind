using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AntHill{
	public class AntHillAI : MonoBehaviour {

		public Rigidbody Ant;

		private int searcherCount = 0;
		private int workerCount = 0;
		private int thinkingtime = 100;

		private List<Food> knownFood = new List<Food>();
		// Use this for initialization
		void Start () {		
		}
		
		// Update is called once per frame
		void Update () {
			if (searcherCount <= 9 && thinkingtime == 100) {
				spawnSearcher();
				searcherCount++;
				thinkingtime = 0;
			}
			if (workerCount <= 49 && knownFood.Count > 0 && thinkingtime == 100) {
				Debug.Log("Worker");
				spawnWorker();
				workerCount++;
				thinkingtime = 0;
			}
			if (thinkingtime < 100) {
				thinkingtime++;
			}
		}

		void spawnSearcher() {
			AntMemory mem = new AntMemory ("Searcher");
			mem.rememberAntHillPos (transform.position);
			mem.knownFood = knownFood.ToArray ();

			Rigidbody searcherClone = (Rigidbody) Instantiate(Ant, transform.position, transform.rotation);
			searcherClone.GetComponent<AntBehaviour> ().init ("Searcher", transform.position, mem);
			instructAnt (searcherClone.GetComponent<AntBehaviour> ().ant);
		}

		void spawnWorker() {
			AntMemory mem = new AntMemory ("Worker");
			mem.rememberAntHillPos (transform.position);
			mem.foodtoCollect = knownFood [0];

			Rigidbody searcherClone = (Rigidbody) Instantiate(Ant, transform.position, transform.rotation);
			searcherClone.GetComponent<AntBehaviour> ().init ("Worker", transform.position, mem);
			instructAnt (searcherClone.GetComponent<AntBehaviour> ().ant);
		}

		void OnTriggerEnter(Collider other) {
			handleAntInBase (other);
		}

		void OnTriggerStay(Collider other) {
			handleAntInBase (other);
		}

		void handleAntInBase(Collider other) {
			InterfaceAI ant = other.GetComponent<AntBehaviour> ().ant;
			if (other.tag == "Ant" && ant.wantsToCommuicate ()) {
				communicate (ant);
			} else {
				ant.supply();
			}
		}

		void communicate(InterfaceAI ant){
			AntMemory mem = ant.getMemory();
			if (mem.hasFoundFood()) {
				updateFoodList(mem.foundFood);
			}

			instructAnt (ant);

		}

		void instructAnt(InterfaceAI ant){
			ant.setIdle (true);
			ant.reset();
			switch (ant.getMemory().getType())
			{
			case "Searcher":
				instructSearcherAnt(ant);
				break;
			case "Worker":
				instructWorkerAnt(ant);
				break;
			default:
				Debug.Log("Type not found!");
				break;
			}

		}

		void instructSearcherAnt(InterfaceAI ant){
			AntMemory mem = ant.getMemory ();
			mem.knownFood = knownFood.ToArray ();
			mem.setTask (Tasks.SEARCH);
			ant.updateInfo (mem);
		}

		void instructWorkerAnt(InterfaceAI ant){
			AntMemory mem = ant.getMemory ();
			mem.setTask (Tasks.COLLECT);
			mem.foodtoCollect = knownFood [0];
		}

		void updateFoodList(Food updateFood) {
			Food newFood = updateFood;
			foreach(Food food in knownFood){
				if (food.foodObject == updateFood.foodObject){
					newFood = food;
					if(newFood.path.metric > updateFood.path.metric){
						newFood = updateFood;
						Debug.Log("Update");
					}
					newFood.isEmpty = updateFood.isEmpty;
					knownFood.Remove(food);
					break;
				}
			}
			knownFood.Add (newFood);
		}
	}
}