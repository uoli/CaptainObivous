using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public Texture2D normalCursor;
	public Texture2D interactCursor;

	// Update is called once per frame
	void Update () {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast (ray, out hit, 1000)) {
			if (hit.collider.gameObject.tag.Equals("Interactable"))
			    UnityEngine.Cursor.SetCursor(interactCursor, Vector3.zero, CursorMode.Auto);
			else
				UnityEngine.Cursor.SetCursor(normalCursor, Vector3.zero, CursorMode.Auto);
		}
	}
}
