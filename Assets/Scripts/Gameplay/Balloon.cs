using UnityEngine;
using System.Collections;

public class Balloon : MimiBehaviour, IInfluenceableByWind 
{

    private Rigidbody m_RigidBody;

    private float m_fBalloonMass;

    public Vector3 Position 
    {
        get 
        {
            return this.m_transThis.position;
        }
    }


	// Use this for initialization
	void Start () 
    {
        m_RigidBody = this.GetComponent<Rigidbody>();

        m_fBalloonMass = m_RigidBody.mass;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_RigidBody.velocity.magnitude > 0)
        {
            m_RigidBody.AddForce(-m_RigidBody.velocity * m_fBalloonMass, ForceMode.Impulse);
        }
	}

    void IInfluenceableByWind.influence(Vector3 _v3Direction, float _fForce) 
    {
        m_RigidBody.AddForce(_v3Direction * _fForce, ForceMode.Impulse);
    }
}
