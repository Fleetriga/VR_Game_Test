using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image actualBar;
    [SerializeField] float updateSpeedSeconds = 0.1f;

    void Awake()
    {
        GetComponentInParent<NonPlayerCharacter>().HealthValueChanged += OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float healthPct)
    {
        StartCoroutine(TweenToPct(healthPct));
    }

    IEnumerator TweenToPct(float pct)
    {
        float preChangedPct = actualBar.fillAmount;
        float elapsed = 0f;

        while(elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            actualBar.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        actualBar.fillAmount = pct;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0,180,0));
    }
}
