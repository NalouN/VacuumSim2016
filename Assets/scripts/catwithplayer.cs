using UnityEngine;
using System.Collections;

public class catwithplayer : MonoBehaviour {

    [SerializeField]
    private float force = 1;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jeanmi");
        if (other.gameObject.tag == "Player") {
            Debug.Log("Robert");
            //Vector3 rowX = new Vector3(0.00000001f, 0.0000000001f, 0);
            //other.gameObject.GetComponent<Rigidbody>().AddForce(rowX);
            //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * -1);
            VacuumBotController.control = false;
            // Calculate Angle Between the collision point and the player
            if(other.GetComponent<VacuumBotController>().GetGround())
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.5f, other.transform.position.z);
            }
            Vector3 dir = other.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            other.GetComponent<Rigidbody>().AddForce(dir * force);
            Debug.Log(VacuumBotController.control);
/*            if (other.gameObject.tag == "Floor")
            {
                VacuumBotController.control = true;
            }*/
        }
    }

    void OnTriggerExit(Collider other)
    {
        VacuumBotController.control = true;
        Debug.Log(VacuumBotController.control);
    }

}
