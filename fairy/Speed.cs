using UnityEngine;
using System.Collections;

public class Speed : MonoBehaviour {
    private Animator animator; //animatorを入れる
    Vector3 prePosition;       //自分の位置

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        prePosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta_position = transform.position - prePosition;
        animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
        prePosition = transform.position;
    }
}
