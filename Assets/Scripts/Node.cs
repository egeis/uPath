using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public bool visited { get; set; }
	public bool obstructed { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
		
	private Node[] _adjacent = new Node[8];
	
	public bool addAdj(ref Node n, int index) {
		if(index < 8 && index > -1 && n != null) {
			_adjacent[index] = n;
			return true;
		}
		
		return false;
	}
	
	public Node getAdj() { return _adjacent; }
}