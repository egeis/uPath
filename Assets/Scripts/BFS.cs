﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS : MonoBehaviour {
	private List<Node> _path = new List<Node>();
	private List<Node> _order = new List<Node>();
	
	public bool Find(Node _start) {
	
		return false;
	}
	
	public List<Node> GetPath() { return _path; }
}