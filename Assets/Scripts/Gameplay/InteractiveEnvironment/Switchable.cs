using UnityEngine;
using System.Collections;

public abstract class Switchable : MimiBehaviour, ISwitchable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void switchState();
}
