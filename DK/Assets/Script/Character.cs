using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    GameObject Add;
    GameObject Delete;

	// Use this for initialization
	void Start () {
        Add = Instantiate(Resources.Load("AddCube"), transform.position, Quaternion.identity) as GameObject;
        Add.transform.localScale = Vector3.one * 1.02f;
        Delete = Instantiate(Resources.Load("DeleteCube"), transform.position, Quaternion.identity) as GameObject;
        Delete.transform.localScale = Vector3.one * 1.02f;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            Add.transform.GetComponent<MeshRenderer>().enabled = true;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 rawPosition = hit.point + hit.normal;
                Vector3 roundedPosition = new Vector3(Mathf.RoundToInt(rawPosition.x), Mathf.RoundToInt(rawPosition.y), Mathf.RoundToInt(rawPosition.z));
                Add.transform.position = roundedPosition - Vector3.one * 0.5f;
                //hit.transform.localScale = Vector3.one * 0.5f;
                //Debug.DrawLine(Vector3.zero, Vector3.one * 10);
                MathHelper.AddBlock(roundedPosition, Block.Dirt);
            }
        }
        else
        {
            Add.transform.GetComponent<MeshRenderer>().enabled = false;
        }

        if (Input.GetMouseButton(1))
        {
            Delete.transform.GetComponent<MeshRenderer>().enabled = true;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 rawPosition = hit.point - hit.normal;
                Vector3 roundedPosition = new Vector3(Mathf.RoundToInt(rawPosition.x), Mathf.RoundToInt(rawPosition.y), Mathf.RoundToInt(rawPosition.z));
                Delete.transform.position = roundedPosition - Vector3.one * 0.5f;
                MathHelper.AddBlock(roundedPosition, Block.Air);
            }
        }
        else
        {
            Delete.transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
