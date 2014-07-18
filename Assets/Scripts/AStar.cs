using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	private List<Node> _path = new List<Node>();
	private List<Node> _order = new List<Node>();
	
	//private Queue<Node> _open = new Queue<Node>();
	private List<Node> _open = new List<Node>();
	private Queue<Node> _close = new Queue<Node>();

	private int Distance(Node _start, Node _target) {
		int x = (int) Mathf.Abs( (float) (_start.X - _target.X));
		int z = (int) Mathf.Abs( (float) (_start.Z - _target.Z));
		int s = (x < z) ? x : z;
		return (s * 14) + ( (int) Mathf.Abs( (float) (x - z)) * 10);
	}

	public bool Find(Node _start, Node _target) {
		_start.Visited = true;
		_start.H = Distance (_start, _target);	
		_start.G = 0;
		_open.Add(_start);
						
		while(_open.Count > 0) {
			Node c = _open[0];
			_open.RemoveAt(0);
			c.Visited = true;

			//End Node Found
			if(c.Status == Node.END ) {		
				//Backtrack using Parent for path.
				while(c != null) {
					_path.Add(c);
					c = c.parent;
				}
				
				_path.Reverse();	//Reverse the Path from END->START to START->END 				
				return true;
			} 
			_close.Enqueue(c);
			
			for (int i = 0; i < c.adjacent.Count; i++) {
				Node n = c.adjacent[i];

				if (n.Status != Node.OBSTRUCTED) {		//If the node is NOT a wall or Null
					int h = Distance(n, _target);
					int g = n.G;
					
					switch(i) {
					case 1:case 3:case 6:case 8:
						g += 14;
						break;
					case 2:case 4:case 5:case 7:
						g += 10;
						break;
					}
					
					if(_close.Contains(n)) continue;

					if(_open.Contains(c) ) {
						if(g < n.G ) {
							n.parent = c;
							n.H = h;
							n.G = g;
							n.F = n.G + n.H;
						}
					} else {
						n.parent = c;
						n.H = h;
                        n.G = g;
						n.F = n.G + n.H;
						_open.Add(n);
					}
					
				}
			}
			
			_open.Sort();
		}
		
		return false;
	}
	
	public List<Node> GetPath() { return _path; }
	public List<Node> GetOrder() { return _order; }
}