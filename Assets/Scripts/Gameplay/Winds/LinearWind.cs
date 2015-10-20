using UnityEngine;
using System.Collections;

public class LinearWind : Wind 
{

    public Vector3 Direction 
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

    private void die() {
        Debug.Log("Wind die!");
        GameManager.Instance.deleteLinearWind(this.GetComponent<LinearWind>());
        GameObject.Destroy(this.gameObject);
    }
}
