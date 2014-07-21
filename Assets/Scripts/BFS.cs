using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS {
	private List<Node> _path = new List<Node>();
	private List<Node> _order = new List<Node>();
	private Queue<Node> _queue = new Queue<Node>();
	
	public bool Find(Node _start) {
		_start.Visited = true;
		_order.Add (_start);
		_queue.Enqueue(_start);
				
		while(_queue.Count > 0) {
			Node n = _queue.Dequeue();
			
			if (n.Status == Node.END) {
				Node t = n;
				while(t != null) {
					_path.Add(t);
					t.Path = true;
					t = t.parent;
				}
				_path.Reverse();
				return true;
			}
			for (int i = 0; i < n.adjacent.Count; i++) {
				if (n.adjacent [i].IsValid ()) {
					n.adjacent[i].parent = n;
					n.adjacent[i].Visited = true;
					_order.Add (n.adjacent[i]);
					_queue.Enqueue(n.adjacent[i]);
				}
			}
			
		}
	
		return false;
	}
	
	public List<Node> GetPath() { return _path; }
	public List<Node> GetOrder() { return _order; }
}