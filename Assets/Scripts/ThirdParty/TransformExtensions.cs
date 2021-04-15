using UnityEngine;
using System.Collections;

public static class TransformExtensions {

	public static void PositionSetX (this Transform t, float x)
	{
		t.position = new Vector3 (x, t.position.y, t.position.z);
	}

	public static void PositionSetY (this Transform t, float y)
	{
		t.position = new Vector3 (t.position.x, y, t.position.z);
	}
	public static void PositionSetZ (this Transform t, float z)
	{
		t.position = new Vector3 (t.position.x, t.position.y, z);
	}

	public static void LocalPositionSetX (this Transform t, float x)
	{
		t.localPosition = new Vector3 (x, t.localPosition.y, t.localPosition.z);
	}

	public static void LocalPositionSetY (this Transform t, float y)
	{
		t.localPosition = new Vector3 (t.localPosition.x, y, t.localPosition.z);
	}
	public static void LocalPositionSetZ (this Transform t, float z)
	{
		t.localPosition = new Vector3 (t.localPosition.x, t.localPosition.y, z);
	}

}
