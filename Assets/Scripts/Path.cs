using UnityEngine;


public class Path {

	public enum State { UNVISITED, VISITED, FINISH };

	public Vector3 position;
	public State state = State.UNVISITED;
}
