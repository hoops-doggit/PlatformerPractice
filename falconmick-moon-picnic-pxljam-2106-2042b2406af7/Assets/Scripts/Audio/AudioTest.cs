using UnityEngine;
using System.Collections;

public class AudioTest : MonoBehaviour {

	public GameObject audioManager;

	private SetLevels setLevels;

	// Use this for initialization
	void Start () {
		setLevels = audioManager.GetComponent<SetLevels> ();
		//setLevels.CreateFade ("Melody", -80.0f, 5.0f);
		//setLevels.CreateFade ("Tape", 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
//		if (Input.GetKeyDown ("y")) {
//			setLevels.CreateFade ("Decimator", 50.0f, 5.0f);
//		}
	}
}
