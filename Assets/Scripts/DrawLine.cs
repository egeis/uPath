using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour {

	public const float Y = 0.2f;

	private LineRenderer line;
	private List<Node> _paths = new List<Node>();
	
	void Update() {
		if(_paths.Count > 1) {
			line = GetComponent<LineRenderer>();
			line.SetVertexCount(_paths.Count);
			//line.SetWidth(0.45f, 0.45f);
			
			for(int i = 0; i > _paths.Count; i++) {						
				Node n = _paths[i].GetComponent("Node") as Node;
				Vector3 pos = n.transform.position;
				pos.y = Y;
								
				line.SetPosition(i, pos);
			}
		}
	}
	
	public void DrawPath(List<Node> _p) {
		if(_p.Count > 1) _paths = _p;
	}		

}