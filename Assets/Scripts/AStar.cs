using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	private List<Node> _path = new List<Node>();
	private List<Node> _order = new List<Node>();
	
	private Queue<Node> _open = new Queue<Node>();
	private Queue<Node> _close = new Queue<Node>();

	private int Distance(Node _start, Node _target) {
		int x = (int) Mathf.Abs( (float) (_start.X - _target.X));
		int z = (int) Mathf.Abs( (float) (_start.Z - _target.Z));
		int s = (x < z) ? x : z;
		return (s * 14) + ( (int) Mathf.Abs( (float) (x - z)) * 10);
		//return x + z;
	}

	public bool Find(Node _start, Node _target) {
		_start.Visited = true;
		_start.g = Distance (_start, _target);
		
		_open.Enqueue(_start);
		
		if(Debug.isDebugBuild) Debug.Log("Count: "+_open.Count);
		
		while(_open.Count > 0) {
			Node n = _open.Dequeue();
			_close.Enqueue(n);
			
			if (n.Status == Node.END) {
				Node t = n;
				
				while(t != null) {
					_path.Add(t);
					t = t.parent;
				}
								
				_path.Reverse();
				return true;
			}
			
			for (int i = 0; i < n.adjacent.Count; i++) {
				Node c = n.adjacent[i];
				
				if (c.IsValid ()) {
					c.parent = n;
					c.h = Distance(c, _target);
					
					int g = c.g;
					
					switch(i) {
					case 1:case 3:case 6:case 8:
						g += 14;
						break;
					case 2:case 4:case 5:case 7:
						g += 10;
						break;
					}
					
					if(_close.Contains(c)) continue;

					if(_open.Contains(c) ) {
						if(g < c.g ) {
							c.g = g;
						}
					} else {
						_open.Enqueue(c);
					}
					
				}
			}
		}
		
		return false;
	}
	
	public List<Node> GetPath() { return _path; }
	public List<Node> GetOrder() { return _order; }
}