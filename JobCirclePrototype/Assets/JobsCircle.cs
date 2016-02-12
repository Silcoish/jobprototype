using UnityEngine;
using System.Collections.Generic;

public class JobsCircle : MonoBehaviour {

	public float rotationAmount = 7.0f;
	public List<GameObject> iconPositions;
	public GameObject start, end;
	public Sprite[] unlockedJobs;

	private int currentJobIndex = 0;

	void Start()
	{
		PopulateList();
	}

	void Update()
	{
		checkForRotation ();
	}

	private void PopulateList() 
	{
		for (int i = 0; i < iconPositions.Count; i++) 
		{
			SpriteRenderer sr = iconPositions[i].AddComponent<SpriteRenderer>();
			sr.sprite = GetNextJobIcon();
		}
	}

	private Sprite GetNextJobIcon()
	{
		Sprite returnSprite = null;
		if (currentJobIndex < unlockedJobs.Length) {
			returnSprite = unlockedJobs [currentJobIndex];

		} else {
			Debug.LogError("Not enough jobs");
		}

		currentJobIndex++;
		if(currentJobIndex >= unlockedJobs.Length)
			currentJobIndex = 0;

		return returnSprite;
	}

	private void checkForRotation()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			gameObject.transform.Rotate(new Vector3(0f, 0f, -rotationAmount));

		}
		else if(Input.GetKeyDown(KeyCode.RightArrow)) {
			gameObject.transform.Rotate(new Vector3(0f, 0f, rotationAmount));
			GameObject temp = iconPositions[iconPositions.Count - 1];
			iconPositions.Remove(temp);
			iconPositions.Insert(0, temp);
			iconPositions[0].transform.position = start.transform.position;
		}
	}
}
