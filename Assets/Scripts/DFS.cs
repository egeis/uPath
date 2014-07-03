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
	public bool Search(GameObject[,] _graph, Node _start) {
		
	}
}