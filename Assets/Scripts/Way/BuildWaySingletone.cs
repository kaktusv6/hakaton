using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWaySingletone : MonoBehaviour {

	private static BuildWay _buildWay;

    public static BuildWay buildWay {
        get {
            if(_buildWay == null)
            {
                _buildWay = GameObject.Find("BuildWay").GetComponent("BuildWay") as BuildWay;
            }
            return _buildWay;
        }
    }

    public static void run(Node node)
    {
        buildWay.run(node);
    }
}
