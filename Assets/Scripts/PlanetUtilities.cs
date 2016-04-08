using UnityEngine;
using System.Collections;

public class PlanetUtilities {

	public static void Spheric2Cartesian(float theta, float radius, out float x, out float y)
	{
		x = -radius * Mathf.Sin(Mathf.Deg2Rad * theta);
		y = radius * Mathf.Cos(Mathf.Deg2Rad * theta);
	}

	public static float GetDisplacementAngle(float delta, float radius)
	{
		return Mathf.Rad2Deg * radius / delta;
	}
}
