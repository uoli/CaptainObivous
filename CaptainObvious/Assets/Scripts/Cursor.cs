using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public Texture2D normalCursor;
	public Texture2D interactCursor;
	private bool overInteractable = false;
	private GameObject selectedInteractable;

	// Update is called once per frame
	void Update () {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (ray, out hit, 1000)) {
			if (hit.collider.gameObject.tag.Equals("Interactable"))
			{
			    UnityEngine.Cursor.SetCursor(interactCursor, Vector3.zero, CursorMode.Auto);
				overInteractable = true;
				selectedInteractable = hit.collider.gameObject;

			}
			else
			{
				UnityEngine.Cursor.SetCursor(normalCursor, Vector3.zero, CursorMode.Auto);
				overInteractable = false;
			}
		}
		HandleMouseClick();
	}

	void HandleMouseClick() {
		if (Input.GetMouseButtonDown(0) && overInteractable)
		{
			if (selectedInteractable.name.Equals("DoorKnob"))
			    Debug.Log("You lose, Captain Obvious!");
			if (selectedInteractable.name.Equals("WindowKnob"))
				Debug.Log("So, you're not such an Average Joe after all!");
		}
	}
}
