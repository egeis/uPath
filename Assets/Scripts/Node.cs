using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public const int OBSTRUCTED = 1;
	public const int START = 2;
	public const int END = 3;
	public const int CLEAR = 0;
	public const int PATH = 4;
	
	public Node parent = null;
	public bool Visited = true;	
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

	private int _status = 0;
	
}