using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionTable {
	private Dictionary<string, bool> hitDetectionTable = new Dictionary<string, bool>();

	public void Set(string key) {
		hitDetectionTable[key] = true;
	}

	public bool Get(string key) {
		if(hitDetectionTable.ContainsKey(key)) {
			return hitDetectionTable[key];
		}
		
		return false;
	}

	public void Remove(string key) {
		hitDetectionTable.Remove(key);
	}
}