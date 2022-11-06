using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsObject : MonoBehaviour
{
    [SerializeField]
    private float angleRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(angleRotation, transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager._GAME_MANAGER.UpdatePoints();
            Destroy(gameObject, 0.3f);
        }
    }
}
