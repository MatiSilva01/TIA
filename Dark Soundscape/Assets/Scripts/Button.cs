using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Button : MonoBehaviour
{
    public UnityEvent buttonPressed;

    [SerializeField] float eventInterval = 0.4f;

    bool isOn = false;
    private Coroutine routine;
    private Animator anim;

private void Awake(){
            anim = GetComponent<Animator>();

}

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        Debug.Log("Button pressed!", gameObject);

        isOn = !isOn;
        anim.SetBool("isOn", isOn);

        if(isOn)
            routine = StartCoroutine("InvokeEvent");
        else
            StopCoroutine(routine);
    }   

    public IEnumerator InvokeEvent()
    {
        while(isOn){
            buttonPressed.Invoke();
            yield return new WaitForSeconds(eventInterval);
        }
    }
}
