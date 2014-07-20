using UnityEngine;
using System.Collections;

public class Controller_GUI : MonoBehaviour {

	private int _searchMethod = 0;
	private bool _triggerReset = false;
	private bool _triggerSearch = false;
	private string _message = "";
	private GUIStyle style = new GUIStyle();
	private readonly string[] SEARCH_TOOLBAR = {"DFS", "BFS", "A*"};

	// Use this for initialization
	void Start () {
		style.normal.textColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetMessage(string message) {
		_message = message;
	}
	
	//Rendering and Handling GUI Events several times per frame.
	void OnGUI() {
		GUILayout.BeginArea( new Rect(0,0, (Screen.width), (Screen.height/4) ) );
		GUILayout.BeginHorizontal ();
		_searchMethod = GUILayout.Toolbar(_searchMethod, SEARCH_TOOLBAR);
		_triggerReset = GUILayout.Button("Reset");
		_triggerSearch = GUILayout.Button("Search");
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label(_message, style);
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
		
		if (GUI.changed) {
			GameObject t = GameObject.Find("Game");
			if(_triggerReset) {
				t.SendMessage("TriggerReset");
				_message = "";
			}
			if(_triggerSearch) {
				t.SendMessage("TriggerReset"); 
				t.SendMessage("TriggerSearch", _searchMethod );
				_message = ""; 
			}
		}	
	}
}
