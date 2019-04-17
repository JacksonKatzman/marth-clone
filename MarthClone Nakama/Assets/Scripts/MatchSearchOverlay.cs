using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchSearchOverlay : MonoBehaviour
{
    [SerializeField] GameObject spinningImage;
    Vector3 rotateVec = new Vector3(0, 0, -1.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinningImage.transform.Rotate(rotateVec);
    }
}
