using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    Renderer renderer;
    private bool disolving;

    public float DisolveTime { get => renderer.sharedMaterial.GetFloat("Vector1_D1F593F7"); }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float temp = renderer.sharedMaterial.GetFloat("Vector1_3CFA44D2") + (renderer.sharedMaterial.GetFloat("Vector1_D1F593F7") * (disolving ? 1 : -1) * Time.deltaTime);
        if (temp > 1) { temp = 1; }
        if (temp < 0) { temp = 0; }

        renderer.sharedMaterial.SetFloat("Vector1_3CFA44D2", temp);
    }

    public IEnumerator Dissolve()
    {
        disolving = true;
        yield return new WaitForSeconds(renderer.sharedMaterial.GetFloat("Vector1_D1F593F7"));

        //Resolve
        disolving = false;
        yield return new WaitForSeconds(renderer.sharedMaterial.GetFloat("Vector1_D1F593F7"));
    }

}
