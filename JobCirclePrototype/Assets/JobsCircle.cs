using UnityEngine;
using System.Collections.Generic;

public class JobsCircle : MonoBehaviour {

	public float rotationAmount = 7.0f;
	private List<GameObject> iconPositions;
	public List<GameObject> spawnedObjects;
	public Sprite[] unlockedJobs;

	private int currentJobIndex = 0;

    public int iconAmounts = 10;
    public float degreeOffset = 7;

	void Start()
	{
        for(int i = 0; i < iconAmounts; i++)
        {
            GameObject go = new GameObject("yo");
            float angle = 180 + (0.5f * iconAmounts * degreeOffset) - ((iconAmounts - i) * degreeOffset);
            print(angle);
            Vector2 newPos = new Vector2(transform.position.x + Mathf.Cos(Mathf.Deg2Rad * angle) * 3.5f, transform.position.y + Mathf.Sin(Mathf.Deg2Rad * angle) * 3.5f);
            go.transform.position = newPos;
        }
		//PopulateList();
	}

	void Update()
	{
		checkForRotation();
	}

	private void PopulateList() 
	{
		for (int i = 0; i < iconPositions.Count; i++) 
		{
            GameObject go = new GameObject("iconObject");
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = GetNextJobIcon();
            go.transform.position = iconPositions[i].transform.position;
            go.transform.parent = transform;
            spawnedObjects.Add(go);
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
		}
	}
}
