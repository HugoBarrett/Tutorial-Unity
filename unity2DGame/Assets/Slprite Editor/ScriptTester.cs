using UnityEngine;

public class ScriptTester : MonoBehaviour
{
  
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // this is saying for as they are in contact with the thing that they contacted with then do this
        foreach (ContactPoint contact in collision.contacts)
        {
            // Draw a white ray in the scene view starting from the contact point
            // in the direction of the contact normal the surface's outward direction
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        // Check if the collision's relative velocity, speed difference between objects
        // is more then 2 units
        if (collision.relativeVelocity.magnitude > 2)
            // Play an audio clip 
            audioSource.Play();
    }


    public GameObject explosionPrefab;
    
    // Called when the object starts colliding with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Get the first contact point of the collision
        ContactPoint contact = collision.contacts[0];

        // Create a rotation that aligns the object's up vector with the contact normal
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

        // Get the position where the contact occurred
        Vector3 position = contact.point;

        // create an explosion effect at the contact point with the calculated rotation
        Instantiate(explosionPrefab, position, rotation);

        Destroy(gameObject);
    }
    // Called each frame during a collision
    void OnCollisionStay(Collision collisionInfo)
    {
        // For each contact point in the ongoing collision
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            // Draw a white ray in the scene view from the contact point scaled by 10 for visibility
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

    // Called when the collision ends (objects are no longer touching)
    void OnCollisionExit(Collision collisionInfo)
    {
        // Print a message indicating the collision has ended with the specific object
        print("No longer in contact with " + collisionInfo.transform.name);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
