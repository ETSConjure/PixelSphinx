using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public float rotationSpeed = 1.0f;
    public float rotationDirection = 1.0f;
    public bool RandomRotationSpeed = true;

    // Use this for initialization
    public void Start () {


        if (RandomRotationSpeed)
            rotationSpeed = 10 * UnityEngine.Random.Range(1.25f, 3f);

        rotationDirection = (Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f)) * 2 - 1);
    }

    // Update is called once per frame
    public void FixedUpdate () {

        this.transform.Rotate(new Vector3(0, 0, 1.0f), rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
