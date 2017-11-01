using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineOutputText : MonoBehaviour {

    public RectTransform output;
    float offset = 10;
    float initPoxY;

	void Start () {
        initPoxY = output.position.y;
	}

    private void Update()
    {
        ChangePosition();
    }

    private void ChangePosition()
    {
        if (initPoxY == output.position.y)
        {
            output.position += new Vector3(0, offset, 0);
        }
    }
}
