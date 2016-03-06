using UnityEngine;
using System.Collections;

public class Cart : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "TrailStop") {
			transform.parent.gameObject.GetComponent<MoveOnTrails> ().Bounce ();
		}
	}
}
