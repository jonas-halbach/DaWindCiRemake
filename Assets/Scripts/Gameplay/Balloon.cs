using UnityEngine;
using System.Collections;

public class Balloon : MimiBehaviour, IInfluenceableByWind 
{

    public Vector3 Position 
    {
        get {
            return this.m_transThis.position;
        }
        set 
        {
            this.m_transThis.position = value;
        }
    }


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void IInfluenceableByWind.influence(Vector3 _v3Direction, float _fForce) 
    {
        this.GetComponent<Rigidbody>().AddForce(_v3Direction * _fForce);
    }
}
