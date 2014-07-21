using UnityEngine;
using System.Collections;

public class Controller_GUI : MonoBehaviour {

	private int _searchMethod = 0;
	private int _mapSelection = 0;
	private int _currentMap = 0;
	private bool _triggerReset = false;
	private bool _triggerSearch = false;
	private bool _triggerLoad = false;
	private string _message = "Press Search to Start the search.";
	private GUIStyle style = new GUIStyle();
	private GUIStyle styleMessages = new GUIStyle();
	private readonly string[] SEARCH_TOOLBAR = {"DFS", "BFS", "A*"};
	private readonly string[] MAP_TOOLBAR = {"Searchspace0", "Searchspace1", "Searchspace2", "Searchspace3"};
	private GameObject _game;

	// Use this for initialization
	void Start () {
		style.normal.textColor = Color.white;
		styleMessages.normal.textColor = Color.white;
		styleMessages.fontStyle = FontStyle.Bold;
		
		_game = GameObject.Find("Game");
	}
	
	public void SetMessage(string message) {
		_message = message;
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
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
		_mapSelection = GUILayout.Toolbar(_mapSelection, MAP_TOOLBAR);
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Messages:", styleMessages, GUILayout.Width(GUI.skin.label.CalcSize(new GUIContent("Messages:")).x));
		GUILayout.Space(10f);
		GUILayout.Label(_message, style);
		GUILayout.EndHorizontal ();
		
		GUILayout.EndArea ();
		
		if (GUI.changed) {
			if(_triggerReset) {
				_game.SendMessage("TriggerReset");
				_message = "Reseting Search.";
			}
			if(_triggerSearch) {
				_game.SendMessage("TriggerReset"); 
				_game.SendMessage("TriggerSearch", _searchMethod );
			}
			if(_mapSelection != _currentMap) {
				_currentMap = _mapSelection;
				_game.SendMessage("TriggerReset");
				_game.SendMessage("LoadMap", _mapSelection); 
			}
		}	
	}
}
