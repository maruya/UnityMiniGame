using UnityEngine;
using System.Collections;

public class ObjectColor : MonoBehaviour {

    private GameObject obj;

	void Start () {

        obj = GameObject.Find(this.name);
        obj.renderer.material.color = Color.yellow;

	}
	
	void Update () {
	
	}

    // ToDo : 16進数をColorに変換
    Color TransformColor(uint val)
    {
        var inv = 1f / 255f;
        var c = Color.black;
        c.r = inv * ((val >> 24) & 0xFF);
        c.g = inv * ((val >> 16) & 0xFF);
        c.b = inv * ((val >> 8) & 0xFF);
        c.a = inv * (val & 0xFF);
        c.a = 1;
        return c;
    }
}
