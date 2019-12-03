using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var rotY = this.gameObject.transform.rotation.eulerAngles.y;
		rotation += 2.0f * Time.deltaTime;

		this.gameObject.transform.rotation = Quaternion.EulerAngles(0, rotation, 0);

	}

	private float rotation = 0;
}
