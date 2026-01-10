using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce = 10f; // Adjust force as needed

    private void OnTriggerEnter(Collider other)
    {

        // other.GetComponent<testLaunchBox>().Launch();

        // // Check if the object is the player
        // if (other.CompareTag("Player"))
        // {
        //     // Get the player's CharacterController or Rigidbody
        //     var controller = other.GetComponent<CharacterController>();
        //     var playerMovement = other.GetComponent<PlayerStateMachine>(); // replace with your actual movement script

        //     // Get the direction the pad is facing
        //     Vector3 launchDirection = transform.up.normalized;

        //     if (controller != null && playerMovement != null)
        //     {
        //         // Optional: temporarily disable controller for physics interaction
        //         controller.enabled = false;

        //         // Apply the force manually via your movement script
        //         playerMovement.Launch();

        //         // Re-enable the controller after short delay (if needed)
        //         StartCoroutine(EnableControllerAfterDelay(controller, 0.2f));
        //     }
        // }
        // else if (other.GetComponent<testLaunchBox>() != null)
        // {
        //     Vector3 launchDirection = transform.up.normalized;
        //     other.GetComponent<testLaunchBox>().Launch(launchDirection * launchForce);

        // }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("JumpPad OnCollisionEnter(Collision collision)");
            other.gameObject.GetComponent<PlayerStateMachine>().Launch();
        }
        else if (other.gameObject.GetComponent<testLaunchBox>() != null)
        {
            other.gameObject.GetComponent<testLaunchBox>().Launch();

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("JumpPad OnCollisionEnter(Collision collision)");
            collision.gameObject.GetComponent<PlayerStateMachine>().Launch();
        }
        else if (collision.gameObject.GetComponent<testLaunchBox>() != null)
        {
            collision.gameObject.GetComponent<testLaunchBox>().Launch();

        }

    }
    private System.Collections.IEnumerator EnableControllerAfterDelay(CharacterController controller, float delay)
    {
        yield return new WaitForSeconds(delay);
        controller.enabled = true;
    }
}