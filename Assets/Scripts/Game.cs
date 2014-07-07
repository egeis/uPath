using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public int sx = 3;
	public int sz = 3;

	private GameObject[,] _tiles;
	private List<Node> _order = new List<Node>();
	private Node _start;
	
	private DFS depth = new DFS();

	// Use this for initialization
	void Start () {
		Debug.Log("Generating Grid:");
		_tiles = new GameObject[sx,sz];
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				GameObject tile = Instantiate(Resources.Load("Prefabs/tile")) as GameObject;
				tile.name = "Tile_"+x+"_"+z;
				tile.transform.position = new Vector3( (float) x, 0, (float) z);
				
				Node n = tile.GetComponent("Node") as Node;
				n.Visited = false;
				n.Animated = false;
				
				_tiles[x,z] = tile;
			}
		}
		
		Debug.Log("Associating Grid:");
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {				
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				if( (x + 1) < sx) n.adjacent.Add( _tiles[x+1,z].GetComponent("Node") as Node);
				if( (z + 1) < sz ) n.adjacent.Add( _tiles[x,z+1].GetComponent("Node") as Node);
				if( (x - 1) >= 0 ) n.adjacent.Add( _tiles[x-1,z].GetComponent("Node") as Node);
				if( (z - 1) >= 0 ) n.adjacent.Add( _tiles[x,z-1].GetComponent("Node") as Node);
				if( (x - 1) >= 0 && (z - 1) >= 0 ) n.adjacent.Add( _tiles[x-1,z-1].GetComponent("Node") as Node);
				if( (x + 1) < sx && (z + 1) < sz ) n.adjacent.Add( _tiles[x+1,z+1].GetComponent("Node") as Node);
				if( (x + 1) < sx && (z - 1) >= 0 ) n.adjacent.Add( _tiles[x+1,z-1].GetComponent("Node") as Node);
				if( (x - 1) >= 0 && (z + 1) < sz ) n.adjacent.Add( _tiles[x-1,z+1].GetComponent("Node") as Node);	
			}					
		}
		
		//TODO: Load Maze.
		_start = _tiles[0,0].GetComponent("Node") as Node;
		_start.Status = Node.START;
		
		Node end = _tiles[0,2].GetComponent("Node") as Node;
		end.Status = Node.END;
		
		Node obs = _tiles[1,1].GetComponent("Node") as Node;
		obs.Status = Node.OBSTRUCTED;
		
		obs = _tiles[1,2].GetComponent("Node") as Node;
		obs.Status = Node.OBSTRUCTED;
		
		obs = _tiles[1,3].GetComponent("Node") as Node;
		obs.Status = Node.OBSTRUCTED;
		
		obs = _tiles[0,3].GetComponent("Node") as Node;
		obs.Status = Node.OBSTRUCTED;
		
		Camera.main.transform.position = new Vector3( (float) (sx / 2), Camera.main.gameObject.transform.position.y,(float) -(sz / 10f));
	
		Time.timeScale = 0.23f;
		
		Find(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (_order.Count > 0) {
			_order[0].Animated = true;
			_order.RemoveAt(0);
		}
		
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				
				switch (n.Status) {
				case Node.CLEAR:
					if(n.Animated == true ) {
						_tiles[x,z].renderer.material.color = Color.white;
					} else {
						if(n.Visited) _tiles[x,z].renderer.material.color = Color.Lerp(Color.white, Color.black, 0.6f);
						else _tiles[x,z].renderer.material.color = Color.cyan;
					}
					break;
				case Node.START:
					_tiles[x,z].renderer.material.color = Color.blue;
					break;	
				case Node.END:
					_tiles[x,z].renderer.material.color = Color.yellow;
					break;
				case Node.OBSTRUCTED:
					_tiles[x,z].renderer.material.color = Color.red;
					break;		
				}
			}
		}
	}
	
	public void Find(int _method = 0) {	
		bool _found = false;

		switch (_method) {
			case 0:	//DFS
				_found = depth.Find (_start);
				break;
			case 1:	//BFS

				break;
			case 2:	//A*

				break;
		}

		//Search for end-node.
		if (_found) {
			_order = depth.GetOrder();
		
			if(Debug.isDebugBuild) {
				string _paths = "";
				List<Node> path = depth.GetPath ();
				
				foreach (Node n in path) {
					_paths += n.name + " ";
				}
				Debug.Log (_paths);
			}
		} else {
			if(Debug.isDebugBuild) {
				Debug.Log("Search: Path Could not be found!");
			}
		}
	}
}
