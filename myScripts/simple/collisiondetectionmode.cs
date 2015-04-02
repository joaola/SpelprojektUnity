using UnityEngine;
using System.Collections;

public class collisiondetectionmode : MonoBehaviour {
    void Example() {
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
}