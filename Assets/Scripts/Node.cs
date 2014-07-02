using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public bool visited = false;
	public bool obstructed = false;
	public int X = -1;
	public int Y = -1;
	public Node[] _adjacent = new Node[8];
}