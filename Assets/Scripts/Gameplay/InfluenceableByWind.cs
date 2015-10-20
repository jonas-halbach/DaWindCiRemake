using UnityEngine;
using System.Collections;

public abstract class InfluenceableByWind : MimiBehaviour, IInfluenceableByWind 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public Vector3 Position {
        get {
            return this.transform.position;
        }
    }

    public abstract void influence(Vector3 _v3Direction, float _fForce);
}
