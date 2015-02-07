using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public Player player;
	public Texture2D normalCursor;
	public Texture2D interactCursor;
	private float distOfObjectInFrontOfPlayer = 2f;
	private float throwForce = 100f;
	private float punchForce = 1f;
	private bool overInteractable = false;
	private GameObject selectedInteractable;
	private bool holdsObject = false;

	// Update is called once per frame
	void Update () {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		bool isOverInteractable = false;
		if (Physics.Raycast (ray, out hit, 10)) {
			if (hit.collider.gameObject.tag.Equals("Interactable"))
			{
				isOverInteractable = true;
			    UnityEngine.Cursor.SetCursor(interactCursor, Vector3.zero, CursorMode.Auto);
				overInteractable = true;
				selectedInteractable = hit.collider.gameObject;
			}
		}

		if (!isOverInteractable) 
		{
			UnityEngine.Cursor.SetCursor(normalCursor, Vector3.zero, CursorMode.Auto);
			overInteractable = false;
		}
		HandleMouseClick(ray, hit);
	}

	void HandleMouseClick(Ray ray, RaycastHit hit) 
	{
		if (Input.GetMouseButtonDown(0))
		{
			//handle punchbag
			if (overInteractable && selectedInteractable.name.Equals("PunchBag"))
			{
				selectedInteractable.rigidbody.AddRelativeForce(-Input.mousePosition * punchForce);
				//selectedInteractable.rigidbody.AddForceAtPosition(new Vector3(punchForce, 0, punchForce), Input.mousePosition);
				//selectedInteractable.rigidbody.AddForceAtPosition(ray.direction * punchForce, hit.point);
				//selectedInteractable.rigidbody.AddForce(ray.direction * punchForce);
				player.IncreaseRage();
				return;
			}

			//throw object
			if (holdsObject)
			{
				selectedInteractable.transform.parent = null;
				selectedInteractable.rigidbody.isKinematic = false;
				selectedInteractable.rigidbody.AddForce(transform.forward * throwForce);
				holdsObject = false;
				return;
			}

			//handle door
			if (overInteractable && selectedInteractable.GetComponent<DoorAnimation>() != null)
			{
				selectedInteractable.GetComponent<DoorAnimation>().enabled = true;
				return;
			}

			//pick object
			if (overInteractable)
			{
				selectedInteractable.rigidbody.isKinematic = true;
				selectedInteractable.transform.parent = Camera.main.transform;
				selectedInteractable.transform.position = transform.position + transform.forward * distOfObjectInFrontOfPlayer;
				holdsObject = true;
				return;
			}

			/*if (selectedInteractable.name.Equals("DoorKnob"))
			{
			    Debug.Log("You lose, Captain Obvious!");
				GameObject.FindGameObjectWithTag("Door").GetComponent<Animator>().enabled = true;
				Application.LoadLevelAdditiveAsync(1);
			}
			if (selectedInteractable.name.Equals("WindowKnob"))
				Debug.Log("So, you're not such an Average Joe after all!");*/

		}
	}
}
