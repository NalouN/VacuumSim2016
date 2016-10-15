using UnityEngine;
using System.Collections.Generic;

public class pathcat : MonoBehaviour {

    public List<Transform> target;
	private int random = 3;
	private int random2 = 1;

    [SerializeField]
    private float distance = 2f;
    
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        VariablePosition();
	}

	void VariablePosition () {
		if (Vector3.Distance (transform.position, target[random].position) > distance) {
            agent.destination = target[random].position;
		}
		else{
            random2 = Random.Range(0, target.Count);
            if (random == random2 || (random == 0 && random2 == 0)) {
                random2++;
                if(random2 >= target.Count){
                    random2 = 1;
                }
            }
			random = random2;            
        }
    }

}