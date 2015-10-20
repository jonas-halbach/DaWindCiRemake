using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wind : MimiBehaviour {

    public float Force 
    {
        get;
        set;
    }

    public Vector3 Position 
    {
        get;
        set;
    }

    public float Radius 
    {
        get;
        set;
    }

    public float LifeTime 
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            die();
        }
	}

    

    protected void die() 
    {

        GameObject.Destroy(this.gameObject);
    }
}
