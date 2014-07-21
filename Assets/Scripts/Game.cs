using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public int sx = 3;
	public int sz = 3;

	private GameObject[,] _tiles;
	
	private List<Node> _path = new List<Node>();
	private Node _start;
	private Node _goal;
	
	private bool _loading = true;
	private bool _canDestroy = false;
	
	private DFS depth = new DFS();
	private BFS breadth = new BFS();
	private AStar astar = new AStar();
	
	private string[] _maps = new string[4];

	// Use this for initialization
	void Start () {		
		for(int x = 0; x < 4; x++) {
			string path = "searchspace"+x.ToString();
			Debug.Log(path);
			TextAsset a = (TextAsset) Resources.Load (path, typeof(TextAsset));
			Debug.Log(a.text);
			
			_maps[x] = a.text;
		}
		
		LoadMap(0); //First Load
	}
	
	public void LoadMap(int map) {
		_loading = true;
		
		if(_canDestroy) {
			Debug.Log("Destroying Grid:");
			for(int x = 0; x < sx; x++) {
				for(int z = 0; z < sz; z++) {
					GameObject tile = _tiles[x,z];
					Destroy(tile);
				}
			}
		}
		
		string[] rows = _maps[map].Split('\n');
		string[] values = rows[0].Split(' ');
		
		sx = int.Parse(values[0]);
		sz = int.Parse(values[1]);
		
		Debug.Log("Generating Grid:");
		_tiles = new GameObject[sx,sz];
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				GameObject tile = Instantiate(Resources.Load("tile")) as GameObject;
				tile.name = "Tile_"+x+"_"+z;
				tile.transform.position = new Vector3( (float) x, 0, (float) z);
				
				Node n = tile.GetComponent("Node") as Node;
				n.Visited = false;
				n.G = 0;
				n.H = 0;
				n.F = 0;
				n.Path = false;
				
				_tiles[x,z] = tile;
			}
		}
		
		Debug.Log("Associating Grid:");
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {				
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				if( (x - 1) >= 0 && (z + 1) < sz ) n.adjacent.Add( _tiles[x-1,z+1].GetComponent("Node") as Node);	//1
				if( (z + 1) < sz ) n.adjacent.Add( _tiles[x,z+1].GetComponent("Node") as Node);						//2
				if( (x + 1) < sx && (z + 1) < sz ) n.adjacent.Add( _tiles[x+1,z+1].GetComponent("Node") as Node);	//3
				if( (x - 1) >= 0 ) n.adjacent.Add( _tiles[x-1,z].GetComponent("Node") as Node);						//4
				if( (x + 1) < sx) n.adjacent.Add( _tiles[x+1,z].GetComponent("Node") as Node);						//5
				if( (x - 1) >= 0 && (z - 1) >= 0 ) n.adjacent.Add( _tiles[x-1,z-1].GetComponent("Node") as Node);	//6
				if( (z - 1) >= 0 ) n.adjacent.Add( _tiles[x,z-1].GetComponent("Node") as Node);						//7
				if( (x + 1) < sx && (z - 1) >= 0 ) n.adjacent.Add( _tiles[x+1,z-1].GetComponent("Node") as Node);	//8
			}					
		}
		
		int a = 1;
		foreach(string line in rows)
		{
			values = line.Split(' ');
			Node _obs = null;
			
			switch(a) {
				case 1:	//Size, Skipping...
					break;
				case 2: //Start
					_start = _tiles[ int.Parse(values[0]), int.Parse(values[1])].GetComponent("Node") as Node;
					_start.Status = Node.START;
					break;
				case 3: //End
					_goal = _tiles[int.Parse(values[0]), int.Parse(values[1])].GetComponent("Node") as Node;
					_goal.Status = Node.END;	
					break;
				default://Obstruction
					_obs = _tiles[ int.Parse(values[0]), int.Parse(values[1])].GetComponent("Node") as Node;
					_obs.Status = Node.OBSTRUCTED;
					break;
			}
			
			a++;
		}
		
		Camera.main.transform.position = new Vector3( (float) (sx / 2), Camera.main.gameObject.transform.position.y,(float) -(sz / 10f));		
		_loading = false;
		_canDestroy = true;
	}
	
	// Update is called once per frame
	void Update () {	
		if(!_loading) {
			for(int x = 0; x < sx; x++) {
				for(int z = 0; z < sz; z++) {
					Node n = _tiles[x,z].GetComponent("Node") as Node;
																																								
					switch (n.Status) {
					case Node.CLEAR:
						if(n.Path) _tiles[x,z].renderer.material.color = Color.white;
						else if(n.Visited) _tiles[x,z].renderer.material.color = Color.Lerp(Color.white, Color.black, 0.5f);
						else _tiles[x,z].renderer.material.color = Color.Lerp(Color.white, Color.black, 0.6f);
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
	}
	
	public void Find(int _method = 0) {	
		bool _found = false;
		
		switch (_method) {
			case 0:	//DFS
				_found = depth.Find (_start);
				_path = depth.GetPath ();
				break;
			case 1:	//BFS
				_found = breadth.Find (_start);
				_path = breadth.GetPath ();
			break;
			case 2:	//A*
				_found = astar.Find(_start, _goal);
				_path = astar.GetPath ();
				break;
		}

		GameObject t = GameObject.Find("GUI");

		//Search for end-node.
		if (_found) {		
			t.SendMessage("SetMessage", "A path was found!");
			
			if(Debug.isDebugBuild) {
				string _out = "";
				
				foreach (Node n in _path) {
					_out += n.name + " ";
				}
				Debug.Log (_out);
			}
			
			
		} else {
			t.SendMessage("SetMessage", "A path could not be found!");
			
			if(Debug.isDebugBuild) {
				Debug.Log("Search: Path Could not be found!");
			}
		}
	}
	
	//Triggers the Search
	public void TriggerSearch(int method) {
		Find (method);
	}
	
	//Resets the grid to use the same map with a different search algorithm.
	public void TriggerReset() {
		_path = new List<Node>();
		
		depth = new DFS();
		breadth = new BFS();
		astar = new AStar();
				
		Debug.Log("Reseting Grid:");
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				n.Visited = false;
				n.parent = null;
				n.G = 0;
				n.H = 0;
				n.F = 0;
				n.Path = false;
			}
		}
	
	}
}
