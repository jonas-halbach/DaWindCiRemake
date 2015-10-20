using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Balloon>())
        {
            GameManager.Instance.gameWon();
        }
    }

}
