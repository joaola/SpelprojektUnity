using UnityEngine;
using System.Collections;

public class movetextures : MonoBehaviour {

	public float scrollSpeed = 1F;
	public float bumpdamping = 5F;
	public float maintexdamping = 5F;
    void LateUpdate() {
        float offset1 = Time.time * scrollSpeed / bumpdamping;
		float offset2 = Time.time * scrollSpeed / maintexdamping;
        GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(0, offset1));
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, offset2));
    }
}