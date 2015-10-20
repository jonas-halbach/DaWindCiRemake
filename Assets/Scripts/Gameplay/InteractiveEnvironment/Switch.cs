using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Switch : MimiBehaviour, IInfluenceableByWind
{

    [SerializeField]
    private List<Switchable> influenceblesBySwitch = new List<Switchable>();

    [SerializeField]
    private Material m_SwitchOnMaterial;

    [SerializeField]
    private Material m_SwitchOffMaterial;

    [SerializeField]
    private bool m_bIsOn = false;

    [SerializeField]
    private Vector3 m_v3SwitchDirection;

    [SerializeField]
    private float m_fSwitchToleranceDeg = 30;

    private bool m_bWasOn = false;

	// Use this for initialization
	void Start () 
    {
        switchOn(m_bIsOn);
        GameManager.Instance.addObjectInfluenceableByWind(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_bIsOn != m_bWasOn)
        {
            switchOn(m_bIsOn);
        }

        setMaterial();
	}

    public void switchOn(bool _bSwitchOn) 
    {
        foreach (Switchable switchable in influenceblesBySwitch)
        {
            switchable.switchState();
        }

        m_bWasOn = _bSwitchOn;
        m_bIsOn = _bSwitchOn;
    }

    public Vector3 Position
    {
        get 
        {
            return this.m_transThis.position;
        }
    }

    public void influence(Vector3 _v3Direction, float _fForce)
    {
        if (Vector3.Angle(m_v3SwitchDirection, _v3Direction) < m_fSwitchToleranceDeg) {
            m_v3SwitchDirection = -m_v3SwitchDirection;
            switchOn(!m_bIsOn);
        }
        
    }

    private void setMaterial() {
        if (m_bIsOn)
        {
            this.GetComponent<MeshRenderer>().material = m_SwitchOnMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = m_SwitchOffMaterial;
        }
    }
}
