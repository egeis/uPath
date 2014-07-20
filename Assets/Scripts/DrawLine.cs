using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {

	public const float Y = 0.2f;

	private LineRenderer line;
	private List<Node> _paths = new List<Node>();
	private bool _start = false;
	private int i = 1;
	private float _counter = 0;
	private float _dist = 0;
	public float Speed = 6.0f;
	private bool isDrawing = false;
	
	void Start() {
		line = GetComponent<LineRenderer>();
	}
	
	public void DrawPath(List<Node> _p) {
		if(_p.Count > 1) {
			_paths = _p;
			_start = true;
			
			line = GetComponent<LineRenderer>();
			line.SetVertexCount(_paths.Count);
			line.SetWidth(0.45f, 0.45f);
						
			Node n1 = _paths[i-1].GetComponent("Node") as Node;
			Node n2 = _paths[i].GetComponent("Node") as Node;
			
			Vector3 pos_a = n1.transform.position;
			Vector3 pos_b = n2.transform.position;
			
			pos_a.y = Y;
			pos_b.y = Y;
			
			line.SetPosition(i-1, pos_a);
			
			_dist = Vector3.Distance(pos_a, pos_b);
		}
	}
	
	void Draw() {
		_counter += 0.1f / Speed;
		
		float x = Mathf.Lerp(0, _dist, _counter);
		
		Node n1 = null;
		Node n2 = null;
	
		if(_paths.Count > 0 && _paths.Count > i) n1 = _paths[i-1].GetComponent("Node") as Node;
		if(_paths.Count > 0 && _paths.Count > i) n2 = _paths[i].GetComponent("Node") as Node;
	
		if(n1 != null && n2 != null) {
			Vector3 pos_a = n1.transform.position;
			Vector3 pos_b = n2.transform.position;
				
			pos_a.y = Y;
			pos_b.y = Y;
				
			Vector3 pos = x * Vector3.Normalize(pos_b - pos_a) + pos_a;
			
			for( int a = i; a < _paths.Count; a++) {
				line.SetPosition(a, pos);
			}
		}
	}
	
	//Deletes the current line.
	void TriggerClear() {
		bool _start = false;
		
		while(isDrawing) { /*Do Nothing*/ }
		
		line.SetVertexCount(0);
		int i = 1;
		float _counter = 0;
		float _dist = 0;
		_paths = new List<Node>();
	}
	
	// Update is called once per frame
	void Update () {
		isDrawing = true;
		if(_start) {
			if(_counter < _dist) {
				Draw ();
			} else if(i < _paths.Count-1) {
				_counter = 0;
				i++;
				
				Node n1 = _paths[i-1].GetComponent("Node") as Node;
				Node n2 = _paths[i].GetComponent("Node") as Node;
				
				Vector3 pos_a = n1.transform.position;
				Vector3 pos_b = n2.transform.position;
				
				pos_a.y = Y;
				pos_b.y = Y;
				
				_dist = Vector3.Distance(pos_a, pos_b);
				
				Draw();
			}
		}
		isDrawing = false;
	}
}
