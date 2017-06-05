using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private Vector3 startPosition, initialVelocity, initialAngularVelocity;

    public float velocity;
    public bool launched;

	// Use this for initialization
	void Start () {
        Init();
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void Init ()
    {
        rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        initialVelocity = rigidbody.velocity;
        initialAngularVelocity = rigidbody.angularVelocity;
    }

    public void Launch (Vector3 velocity)
    {
        launched = true;
        audioSource = GetComponent<AudioSource>();
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;
        audioSource.Play();
    }

    public void Reset()
    {
        rigidbody.useGravity = false;
        launched = false;
        transform.position = startPosition;
        rigidbody.velocity = initialVelocity;
        rigidbody.angularVelocity = initialAngularVelocity;
    }
}
