using UnityEngine;
using System.Collections;

interface IInfluenceableByWind {

    Vector3 Position {
        get;
        set;
    }

	void influence(Vector3 _v3Direction, float _fForce);
}
