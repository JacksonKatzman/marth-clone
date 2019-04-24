using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField] Image redArrow;
    [SerializeField] Image redTarget;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.matchUI = this;
        }
    }

    //TODO: FIX RED ARROW TARGETTING!
    public void SetRedArrow(Vector3 start, Vector3 end)
    {
        //Debug.Log("Ending spot: " + end.ToString());
        if (!redArrow.gameObject.activeSelf)
        {
            redArrow.gameObject.SetActive(true);
        }
        start = myCamera.WorldToScreenPoint(start);
        start.z = 0.0f;
        Vector3 between = end - start;
        float mag = between.magnitude;
        Vector3 midPoint = (start + end) / 2;
        between.Normalize();
        float dot = Vector3.Dot(between, redArrow.transform.forward.normalized);
        float angle = Mathf.Acos(dot);
        angle = angle * Mathf.Rad2Deg;
        if(end.x < start.x)
        {
            angle *= -1;
        }
        Quaternion look = Quaternion.Euler(0, 0, angle);
        redArrow.GetComponent<RectTransform>().localPosition= midPoint- myCamera.WorldToScreenPoint(Vector3.zero);
        redArrow.GetComponent<RectTransform>().sizeDelta = new Vector2(30, mag);
        redArrow.transform.localRotation = look;
    }

    public void HideRedArrow()
    {
        redArrow.gameObject.SetActive(false);
    }

    public void SetRedTarget(Vector3 pos)
    {
        if(!redTarget.gameObject.activeSelf)
        {
            redTarget.gameObject.SetActive(true);
        }
        redTarget.GetComponent<RectTransform>().localPosition = pos - myCamera.WorldToScreenPoint(Vector3.zero);
    }

    public void HideRedTarget()
    {
        redTarget.gameObject.SetActive(false);
    }
}
