using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInput : MimiBehaviour 
{

    [SerializeField]
    private GameObject m_goLinearWindPrototype;
    
    [SerializeField]
    private float m_fWindLayerHeight = 10;

    [SerializeField]
    private float m_fForceMultiplicatior = 1;

    [SerializeField]
    private float m_fRadiusMultiplicator = 1;

    [SerializeField]
    private float m_fLifeTimeMultiplicator = 1;

    private List<Vector2> m_listMousePositions;

    private GameManager m_gameManager;

    private float m_fWindCreationTime;

    public enum MouseButton {
        Left = 0,
        Right = 1,
        Middle = 2
    }

	// Use this for initialization
	void Start () 
    {
        m_gameManager = GameManager.Instance;

        m_listMousePositions = new List<Vector2>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void FixedUpdate() 
    {

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            m_listMousePositions.Add(Input.mousePosition);
            m_fWindCreationTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            evaluateMousePositions();
            m_listMousePositions.Clear();
            m_fWindCreationTime = 0;
        }
    }


    private void evaluateMousePositions() {

        int iListSize = m_listMousePositions.Count;

        if (iListSize > 1)
        {
            Vector2 v2OldMousePosition = m_listMousePositions[0];
            Vector2 v2CurrentMousePosition = m_listMousePositions[iListSize - 1];

            createLinearWind(v2OldMousePosition, v2CurrentMousePosition);
        }
    }

    private void createLinearWind(Vector2 _v2WindStartPosition, Vector2 _v2WindEndPosition) 
    {
                
                GameObject windObject = GameObject.Instantiate(m_goLinearWindPrototype);

                LineRenderer linearWindtrail = windObject.GetComponent<LineRenderer>();
                Vector3 v3TrailStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(_v2WindStartPosition.x, _v2WindStartPosition.y, m_fWindLayerHeight));
                linearWindtrail.SetPosition(0, v3TrailStartPosition);
                Vector3 v3TrailEndPosition = Camera.main.ScreenToWorldPoint(new Vector3(_v2WindEndPosition.x, _v2WindEndPosition.y, m_fWindLayerHeight));
                linearWindtrail.SetPosition(1, v3TrailEndPosition);

                float fMagnitudeLengthRate = Vector2.Distance(_v2WindStartPosition, _v2WindEndPosition) / m_fWindCreationTime;
                float fForce = fMagnitudeLengthRate * m_fForceMultiplicatior;
                float fLifeTime = fMagnitudeLengthRate * m_fLifeTimeMultiplicator;
                float fRadius = fMagnitudeLengthRate * m_fRadiusMultiplicator;
                

                LinearWind linearWind = windObject.GetComponent<LinearWind>();
                linearWind.Position = v3TrailStartPosition;
                linearWind.Direction = v3TrailEndPosition - v3TrailStartPosition;
                linearWind.Force = fForce;
                linearWind.LifeTime = fLifeTime;
                linearWind.Radius = fRadius;
                
                m_gameManager.addLinearWind(linearWind);
    }

}
