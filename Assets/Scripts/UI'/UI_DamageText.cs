using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DamageText : MonoBehaviour
{
    [SerializeField] float LifeTime = 10f;
    [SerializeField] TextMeshProUGUI text;

    float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 0.5f, Space.World);

        if ((Time.time - startTime) > LifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void UpdatePositionAndValue(Vector3 pos, string val)
    {
        //Move to point of contact and forward a bit so that the text isn't obscured
        transform.position = pos;
        transform.Translate(transform.forward * -0.5f, Space.World);

        startTime = Time.time;
        text.alpha = 1;
        text.text = val;
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1f);
    }
}
