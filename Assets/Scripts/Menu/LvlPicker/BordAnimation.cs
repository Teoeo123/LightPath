using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Material UnActive;
    public Material Active;
    public GameObject border;
    public ScenesManager.Scenes scene;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        border.GetComponent<SpriteRenderer>().material = Active;
    }


    private void OnMouseExit()
    {
        border.GetComponent<SpriteRenderer>().material = UnActive;
    }

    private void OnMouseDown()
    {
        ScenesManager.instance.LoadScene(scene);
    }

}

