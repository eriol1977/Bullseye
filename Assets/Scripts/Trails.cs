using UnityEngine;
using System.Collections;

public class Trails : MonoBehaviour
{
	public GameObject sleeperPrefab;

	public float sleeperDistance = 0.5f;

	void Start ()
	{
		GameObject sleeper;
		float z = 0.0f;
		while (z <= 100.0f) {
			sleeper = (GameObject)Instantiate (sleeperPrefab, new Vector3 (0, 0, z), Quaternion.identity);
			z += sleeperDistance;
		}
	}

}
