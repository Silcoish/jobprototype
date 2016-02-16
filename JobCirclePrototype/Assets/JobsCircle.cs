using UnityEngine;
using System.Collections.Generic;

public class JobsCircle : MonoBehaviour {

	private List<GameObject> iconPositions;
	public List<GameObject> spawnedObjects;
	public Sprite[] unlockedJobs;

	private int currentJobIndex = 0;

    public float radius = 3.5f;
    public int iconAmounts = 10;
    public float degreeOffset = 7;

    private float currentRotation = 0;

	void Start()
	{
        currentRotation = transform.rotation.z;

        iconPositions = new List<GameObject>();
        spawnedObjects = new List<GameObject>();

        PopulateList();
    }

	void Update()
	{
		checkForRotation();
	}

	private void PopulateList()
    {
        for (int i = 0; i < iconAmounts; i++)
        {
            GameObject go = new GameObject("spawnPos");
            float angle = 180 + (0.5f * iconAmounts * degreeOffset) - ((iconAmounts - i) * degreeOffset);
            print(angle);
            Vector2 newPos = new Vector2(transform.position.x + (Mathf.Cos(Mathf.Deg2Rad * angle) * radius), transform.position.y + (Mathf.Sin(Mathf.Deg2Rad * angle) * radius));
            go.transform.position = newPos;
            iconPositions.Add(go);

            GameObject go2 = new GameObject("spawnSprite");
            go2.transform.position = iconPositions[i].transform.position;
            go2.transform.parent = transform;
            SpriteRenderer sr = go2.AddComponent<SpriteRenderer>();
            sr.sprite = GetNextJobIcon();
            spawnedObjects.Add(go2);
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
			gameObject.transform.Rotate(new Vector3(0f, 0f, -degreeOffset));
            GameObject topObject = spawnedObjects[0];
            spawnedObjects.Remove(topObject);
            spawnedObjects.Add(topObject);

            topObject.transform.position = iconPositions[iconPositions.Count - 1].transform.position;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow)) {
			gameObject.transform.Rotate(new Vector3(0f, 0f, degreeOffset));
            GameObject endObject = spawnedObjects[spawnedObjects.Count - 1];
            spawnedObjects.Remove(endObject);
            spawnedObjects.Insert(0, endObject);

            endObject.transform.position = iconPositions[0].transform.position;
		}
	}
}
