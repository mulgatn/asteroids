using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelLibrary", menuName = "Asteroid/LevelLibrary")]
public class LevelLibrary : ScriptableObject
{
	public List<LevelInfo> library;

	[Serializable]
	public struct LevelInfo
	{
		public int asteroidNum;
		public List<int> asteroidScore; // Each index represents the size of the asteroid
	}
}
