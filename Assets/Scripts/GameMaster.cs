using UnityEngine;

public class GameMaster : MonoBehaviour {

	// For inventory
	enum Hand { Empty, Item0, Item1, Item2, Item3, Item4, Item5, Item6, Delete };
	Hand hand = Hand.Empty;

	// White selection square
	public GameObject placeholder;

	// Level settings and objects array
	int levelWidth = 23;
	int levelHeight = 14;
	[HideInInspector] public GameObject[,] objects;

	void Awake() {
		// When starting initiate objects array
		objects = new GameObject[levelWidth, levelHeight];
	}

	private void LateUpdate() {
		// On right click deselect whatever is in hand
		if (Input.GetMouseButtonDown(1)) hand = Hand.Empty;

		// Get mouse position and find the closest sqare 
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(levelWidth-1, levelHeight, 0) / 2.0f;
		int x = Mathf.RoundToInt(mousePos.x);
		int y = Mathf.RoundToInt(mousePos.y);

		// If mouse isnt over anything selectable
		if (!(x >= 4 && x <= 18 && y >= 3 && y <= 11)) return;

		// Do whatever is needed for selected item or action
		placeholder.SetActive(false);
		switch (hand) {
			case Hand.Empty: // Draw the white selection square
				placeholder.SetActive(true);
				SetPlaceholderColor(255, 255, 255, 50);
				SetPlaceholderPosition(x, y);
				break;
			case Hand.Item0: // Draw placeholder item, on click actually place it and set hand as empty
				break;
			case Hand.Item1:
				break;
			case Hand.Item2:
				break;
			case Hand.Item3:
				break;
			case Hand.Item4:
				break;
			case Hand.Item5:
				break;
			case Hand.Item6:
				break;
			case Hand.Delete: // Draw the red selection square
				placeholder.SetActive(true);
				SetPlaceholderColor(255, 80, 80, 100);
				SetPlaceholderPosition(x, y);
				Delete(x, y); // On mouse button press delete whatever is there
				break;
			default:
				Debug.Log("Unknown case in 'GameMaster'");
				break;
		}
	}

	private void SetPlaceholderColor(float r, float g, float b, float a) {
		placeholder.GetComponent<SpriteRenderer>().color = new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
	}

	private void SetPlaceholderPosition(int x, int y) {
		placeholder.transform.position = new Vector2(x - levelWidth / 2, y - levelHeight / 2);
	}

	private void Delete(int x, int y) {
		// If there is nothing in objects array
		if (!objects[x, y]) return;
		// Some items should never be deleted
		string name = objects[x, y].name;
		if (name.Contains("Path") || name.Contains("Player")) return;
		// If check is passed, delete
		if (Input.GetMouseButtonDown(0)) Destroy(objects[x, y]);
	}

	public void SelectItem(int itemNum) {
		// If we select the same thing twice, deselect
		if (itemNum == (int)hand - 1) {
			hand = Hand.Empty;
			return;
		}
		// Assign selected
		hand = (Hand)(itemNum + 1);
	}
}
