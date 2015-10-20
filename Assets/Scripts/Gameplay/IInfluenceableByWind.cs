using UnityEngine;
using System.Collections;

public interface IInfluenceableByWind {

    Vector3 Position {
        get;
    }

	void influence(Vector3 _v3Direction, float _fForce);
}
