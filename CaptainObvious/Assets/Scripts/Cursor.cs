using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public Player player;
	public Texture2D normalCursor;
	public Texture2D interactCursor;
	public Texture2D nonInteractCursor;
	private float distOfObjectInFrontOfPlayer = 2f;
	private float throwForce = 100f;
	private float punchForce = 2f;
	private bool overInteractable = false;
	private GameObject selectedInteractable;
	private bool holdsObject = false;

	// Update is called once per frame
	void Update () {
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		bool isNarrationHappening = Camera.main.audio.isPlaying;
		overInteractable = false;
		if (Physics.Raycast (ray, out hit, 10)) 
		{
			if (hit.collider.gameObject.tag.Equals("Interactable"))
			{
				overInteractable = true;
				selectedInteractable = hit.collider.gameObject;
			}
		}

		if (overInteractable && isNarrationHappening && !selectedInteractable.name.Equals("PunchBag"))
		{
			UnityEngine.Cursor.SetCursor(nonInteractCursor, Vector3.zero, CursorMode.Auto);
		}
		else if(overInteractable)
		{
			UnityEngine.Cursor.SetCursor(interactCursor, Vector3.zero, CursorMode.Auto);
		}
		else
		{
			UnityEngine.Cursor.SetCursor(normalCursor, Vector3.zero, CursorMode.Auto);
		}

		if (!isNarrationHappening)
			HandleMouseClick(ray, hit);

		if (Input.GetKeyUp(KeyCode.E))
			SkipNarration();
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


			if (overInteractable && selectedInteractable.GetComponent<Phone>() != null)
			{
				var phone = selectedInteractable.GetComponent<Phone>();
				phone.Interact(player);
				return;
			}

			//throw object
			if (holdsObject)
			{
				selectedInteractable.transform.parent = null;
				selectedInteractable.rigidbody.isKinematic = false;
				selectedInteractable.rigidbody.useGravity = true;
				selectedInteractable.rigidbody.AddForce(transform.forward * throwForce);
				holdsObject = false;
				return;
			}

			//handle door
			if (overInteractable && selectedInteractable.GetComponent<DoorAnimation>() != null)
			{
				selectedInteractable.GetComponent<DoorAnimation>().Open();
				return;
			}

			if (selectedInteractable.gameObject.name == "PhoneButtonTrigger" || selectedInteractable.gameObject.name == "PowerSocket" || selectedInteractable.gameObject.name == "AnsweringMashine")
				return;

			//pick object
			if (overInteractable)
			{
				selectedInteractable.rigidbody.isKinematic = true;
				selectedInteractable.rigidbody.useGravity = false;
				selectedInteractable.transform.parent = Camera.main.transform;
				selectedInteractable.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distOfObjectInFrontOfPlayer;
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

	void SkipNarration ()
	{
		Camera.main.audio.Stop();
	}
}
