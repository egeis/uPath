/**
 * Psuedo-Code for DFS
 * 
 * Start up 
 * 	Set all nodes to not visited 
 * 	Set start node coordinates 
 * 	Set start node’s parent to null 
 * 	Set goal node coordinates 
 * 	Call Traverse(start node) 
 * 
 * Traverse (Node n) 
 * 	Mark node n as visited 
 * 	If node n is the goal node 
 * 		Print all nodes starting from n and traversing through all ancestors (parents) 
 * 		Halt entire process 
 * 	For each node t that is an 8-connected neighbor node of n 
 * 		If node t has not been visited and node t is not a wall node 
 * 			Set parent of node t to node n 
 * 			Call Traverse(node t) 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DFS {
	private List<Node> _path = new List<Node>();
	
	public bool Search(Node _start) {
		_start.Visited = true;
		
		if(_start.Status == Node.END) {
			_path.Add (_start);
			return true;
		}
		
		for( int i = 0; i < _start.adjacent.Count; i++ ) {
			if(	_start.adjacent[i].IsValid() ) {
				if(Search (_start.adjacent[i]) ) {
					_path.Insert(0, _start);
					return true;
				}
			}
		}
		
		return false;
	}
	
	public List<Node> GetPath() { return _path; }
}