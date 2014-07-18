using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour, IComparable<Node> {
	public const int OBSTRUCTED = 1;
	public const int START = 2;
	public const int END = 3;
	public const int CLEAR = 0;
	public const int PATH = 4;

	public int G = 0;
	public int H = 0;
	public int F = 0;

	public bool Visited = false;
	public bool Animated = false;
	public List<Node> adjacent = null; 	//Used for ALL searches.
	public Node parent = null;			//Used for BFS.
		
	public int Status {
		get { return _status; }
		set { if(value >= 0 && value < 5) _status = value; }
	} 
	
	public int X {
		get { return (int) gameObject.transform.position.x; }
	}
	
	public int Z {
		get { return (int) gameObject.transform.position.z; }
	}

	/**
	 *	Game Specific Validation
	 */	
	public bool IsValid() {
		return !Visited && (_status != Node.OBSTRUCTED); 
	}
	
	/**
	 *	Compare this F to another F
	 */
	public int CompareTo(Node n) {
		if(n == null) return 1;

		return this.H.CompareTo(n.H);
	}

	private int _status = 0;
}