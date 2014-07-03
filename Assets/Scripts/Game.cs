using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public int sx = 3;
	public int sz = 3;

	private GameObject[,] _tiles;
	private Node _start;

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
				n.Status = Node.CLEAR;
				n.Visited = false;
				
				_tiles[x,z] = tile;
			}
		}
		
		_start = _tiles[0,0].GetComponent("Node") as Node;
		_start.Status = Node.START;
		
		Node end = _tiles[sx-1,sz-1].GetComponent("Node") as Node;
		end.Status = Node.END;
		
		Node obs = _tiles[1,1].GetComponent("Node") as Node;
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
					_tiles[x,z].renderer.material.color = Color.white;
					break;
				case Node.START:
					_tiles[x,z].renderer.material.color = Color.gray;
					break;	
				case Node.END:
					_tiles[x,z].renderer.material.color = Color.green;
					break;
				case Node.OBSTRUCTED:
					_tiles[x,z].renderer.material.color = Color.red;
					break;		
				}
				if(n.Visited == true) _tiles[x,z].renderer.material.color = Color.blue;
			}
		}
	}
	
	public void Search() {
		for(int x = 0; x < sx; x++) {
			for(int z = 0; z < sz; z++) {
				Node n = _tiles[x,z].GetComponent("Node") as Node;
				n.Visited = false;
			}
		}
		
		//Node[] Result = DFS.Search(_tiles, _start);
		//if(Result.Equals(null)) Debug.Log("Failed fo find a path!");
	}
}
