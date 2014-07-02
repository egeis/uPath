using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public bool visited { get; set; }
	public bool obstructed { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	
	//private Node[] _adjacent = new Node[8];
}