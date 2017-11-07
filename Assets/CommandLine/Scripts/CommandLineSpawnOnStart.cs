using UnityEditor;
using UnityEngine;

public class CommandLineSpawnOnStart : MonoBehaviour {

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        PrefabUtility.InstantiatePrefab(prefab);
	}
}
