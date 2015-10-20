using UnityEngine;
using System.Collections;

public class Door : Switchable, ISwitchable
{

    [SerializeField]
    private bool m_bOpen = false;

	// Use this for initialization
	void Start () 
    {
        if (m_bOpen) 
        {
            switchState();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override void switchState() 
    {

        if (!m_bOpen)
        {
            transform.Rotate(0, 90, 0);
        }
        else
        {
            transform.Rotate(0, -90, 0);
        }

        m_bOpen = !m_bOpen;
    }


}
