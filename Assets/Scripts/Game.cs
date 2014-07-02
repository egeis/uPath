using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public int sx = 3;
	public int sy = 3;

	// Use this for initialization
	void Start () {
		Debug.Log("Generating Grid:");
		for(int x = 0; x < sx; x++) {
			for(int y = 0; y < sy; y++) {
				GameObject tile = Instantiate(Resources.Load("Prefabs/tile")) as GameObject;
				tile.transform.localScale = new Vector3(1.0f,0.2f,1.0f);
				tile.transform.position = new Vector3( (float) x * 1.1f,0, (float) y * 1.1f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
