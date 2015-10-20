using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInput : MimiBehaviour {

    [SerializeField]
    private GameObject m_goLinearWindPrototype;

    public float m_fWindLayerHeight = 10;

    private List<Vector2> m_listMousePositions;

    private GameManager m_gameManager;

    public enum MouseButton {
        Left = 0,
        Right = 1,
        Middle = 2
    }

	// Use this for initialization
	void Start () {
        m_gameManager = GameManager.Instance;

        m_listMousePositions = new List<Vector2>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            m_listMousePositions.Add(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            evaluateMousePositions();
            m_listMousePositions.Clear();
        }
    }


    private void evaluateMousePositions() {

        Vector2 v2OldMousePosition = Vector2.zero;

        for (int i = 0; i < m_listMousePositions.Count; i++) {

            Vector2 v2CurrentMousePosition = m_listMousePositions[i];

            if(i > 0 ) {
                createLinearWind(v2OldMousePosition, v2CurrentMousePosition);
            }

            v2OldMousePosition = v2CurrentMousePosition;
        }

    }

    private void createLinearWind(Vector2 _v2WindStartPosition, Vector2 _v2WindEndPosition) {
                
                GameObject windObject = GameObject.Instantiate(m_goLinearWindPrototype);

                LineRenderer linearWindtrail = windObject.GetComponent<LineRenderer>();
                Vector3 v3TrailStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(_v2WindStartPosition.x, _v2WindStartPosition.y, m_fWindLayerHeight));
                linearWindtrail.SetPosition(0, v3TrailStartPosition);
                Vector3 v3TrailEndPosition = Camera.main.ScreenToWorldPoint(new Vector3(_v2WindEndPosition.x, _v2WindEndPosition.y, m_fWindLayerHeight));
                linearWindtrail.SetPosition(1, v3TrailEndPosition);

                LinearWind linearWind = windObject.GetComponent<LinearWind>();
                linearWind.Position = v3TrailStartPosition;
                linearWind.Direction = v3TrailEndPosition - v3TrailStartPosition;
                linearWind.Force = 100;
                linearWind.LifeTime = 100;
                linearWind.Radius = 100;
                
                m_gameManager.addLinearWind(linearWind);
    }

}
