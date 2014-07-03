using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public int sx = 3;
	public int sz = 3;

	private GameObject[,] _tiles;
	private Node _start;
	
	private DFS depth = new DFS();

	// Use this for initialization
	void Start () {
		Debug.Log("Generating Grid:");
		_tiles = new GameObject[sx,sz];
		ResetTiles();
		
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
	
		//Search Test
		Search();
	}
	
	// Update is called once per frame
	void Update () {
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				
				switch (n.Status) {
				case Node.CLEAR:
					if(n.Visited == true) _tiles[x,z].renderer.material.color = new Color32(100,100,255,100);
					else _tiles[x,z].renderer.material.color = Color.white;
					break;
				case Node.START:
					if(n.Visited == true) _tiles[x,z].renderer.material.color = new Color32(100,255,255,100);				
					else _tiles[x,z].renderer.material.color = Color.green;
					break;	
				case Node.END:
					if(n.Visited == true) _tiles[x,z].renderer.material.color = new Color32(200,200,200,100);				
					else _tiles[x,z].renderer.material.color = Color.gray;
					break;
				case Node.OBSTRUCTED:
					if(n.Visited == true) _tiles[x,z].renderer.material.color = Color.black;	//Error!!!				
					else _tiles[x,z].renderer.material.color = Color.red;
					break;		
				}
				
			}
		}
	}
	
	public void ResetTiles() {
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				GameObject tile = Instantiate(Resources.Load("Prefabs/tile")) as GameObject;
				tile.name = "Tile_"+x+"_"+z;
				tile.transform.position = new Vector3( (float) x, 0, (float) z);
				
				Node n = tile.GetComponent("Node") as Node;
				n.Status = Node.CLEAR;
				n.Visited = false;
				
				_tiles[x,z] = tile;
			}
		}
	}
	
	public void Search() {
		//Reset Visited to FALSE.
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				n.Visited = false;
			}
		}
		
		//Search for end-node.
		bool _found = depth.Search( _start );
		if(_found) {
			string _paths = "";
			List<Node> path = depth.GetPath();
			
			foreach(Node n in path) {
				_paths += n.name + " ";
			}
			
			Debug.Log(_paths);
		}
	}
}
