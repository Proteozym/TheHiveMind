using System;
using UnityEngine;
using System.Collections;

namespace AntHill
{
	public interface InterfaceAI
	{
		void init(int maxMvnts, float maxT, AntMemory mem, Vector3 curPos);
		Vector3 getNextMovement();
		void handleCollission(Collider other);
		void reset();
		void supply();
		//void handleEvent(String EventDesc);
		bool isReturnedHome();
		void setIdle(bool idle);
		bool hasReachedNextPosition();
		void updateInfo(AntMemory mem);
		AntMemory getMemory();
		void updatePosition(Vector3 position);
		bool idle();
		bool isAtHomeBase();
		bool wantsToCommuicate();
	}

}

