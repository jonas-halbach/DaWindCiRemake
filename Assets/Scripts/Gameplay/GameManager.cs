using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> 
{

    [SerializeField]
    private GameObject m_goBalloon;

    [SerializeField]
    private List<LinearWind> m_listExistingLinearWinds;

    [SerializeField]
    private List<IInfluenceableByWind> m_listInfluenceablesByWind = new List<IInfluenceableByWind>();

    [SerializeField]
    private Canvas m_goFinishedScreen;

    protected GameManager() { }

    void Awake() 
    {
        addBalloonToInfluenceableByWindList();
    }

	// Use this for initialization
	void Start () 
    {
        showLevelFinishedCanvas(false);
        initializeAllWinds(); 
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        influenceObjectsByWind();
	}

    private void initializeAllWinds() 
    {
        LinearWind[] arWinds = GameObject.FindObjectsOfType<LinearWind>();

        foreach (LinearWind wind in arWinds)
        {
            if (!m_listExistingLinearWinds.Contains(wind)) {
                m_listExistingLinearWinds.Add(wind);
            }
        }        
    }

    public void addLinearWind(LinearWind _wind) {
        m_listExistingLinearWinds.Add(_wind);
    }

    public void deleteLinearWind(LinearWind _wind) {
        m_listExistingLinearWinds.Remove(_wind);
    }

    private void addBalloonToInfluenceableByWindList() 
    {
    
        IInfluenceableByWind goInfluenceableByWind = m_goBalloon.GetComponent<IInfluenceableByWind>();
        
        if (!m_listInfluenceablesByWind.Contains(goInfluenceableByWind))
        {
            m_listInfluenceablesByWind.Add(goInfluenceableByWind);
        }
    }

    private void influenceObjectsByWind() 
    {
        foreach (IInfluenceableByWind influencableObject in m_listInfluenceablesByWind)
        {
            influenceSingleObjectByWind(influencableObject);
        }
    }

    private void influenceSingleObjectByWind(IInfluenceableByWind _influencableObject) 
    {
        if(m_listExistingLinearWinds.Count > 0)
        {
            Vector3 v3ObjectPosition = _influencableObject.Position;

            Vector3 v3ResultngWindDirection = new Vector3();

            float fResultingWindForce = 0f;

            foreach(LinearWind wind in m_listExistingLinearWinds) 
            {
                Ray ray = new Ray(wind.Position, wind.Direction);

                Vector3 v3NearestPointOnRay = MathHelper.v3GetNearestPointOnRay(ray, v3ObjectPosition);

                float fSmallestDistanceObjectWind = MathHelper.fGetSmallestDistanceToStraight(ray, v3NearestPointOnRay, v3ObjectPosition);

                if(fSmallestDistanceObjectWind < wind.Radius) {
                    fResultingWindForce += (wind.Radius / fSmallestDistanceObjectWind * wind.Force);
                    v3ResultngWindDirection += wind.Direction.normalized;
                }
            }

            if (fResultingWindForce > 0) {
                _influencableObject.influence(v3ResultngWindDirection.normalized, fResultingWindForce);
            }
        }
    }

    public void addObjectInfluenceableByWind(IInfluenceableByWind _influenceableObject) 
    {
        m_listInfluenceablesByWind.Add(_influenceableObject);
    }

    public void gameWon() {
        showLevelFinishedCanvas(true);
    }

    public void showLevelFinishedCanvas(bool _bShow) {
        m_goFinishedScreen.GetComponent<Canvas>().enabled = _bShow;
    }


}
