using UnityEngine;
using System.Collections;

/**
 * Generic Node Class
 */
public class Node : MonoBehaviour {
	public Node parent;
	public Node[] children;
	public bool used = false;
}