using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibracaoGamepad : MonoBehaviour
{
    public AudioClip soundPassos;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
                audioSource = GetComponent<AudioSource>(); 
                audioSource.clip = soundPassos;

    }

    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            VibrarGamepad();
        }
    }
    void OnTriggerExit(Collider other) //quando deixa de colidir, para de vibrar
    {
        if (other.tag == "Player")
        {
            PararVibracao();
        }
    }

    void VibrarGamepad() 
    {
        // Verifica se há um gamepad conectado
        if (Gamepad.current != null)
        {
            if (audioSource != null && soundPassos != null)
            {
                audioSource.Stop();
            }
            // Vibração
            //valor da esquerda - Velocidade do motor de baixa frequência (esquerda). Valor normalizado [0..1] com 1 indicando velocidade máxima e 0 indicando que o motor está desligado. Será automaticamente fixado no alcance.
            //Velocidade do motor de alta frequência (direita). Valor normalizado [0..1] com 1 indicando velocidade máxima e 0 indicando que o motor está desligado. Será automaticamente fixado no alcance.
            //a sensacao que me da é que o da direita vibra a direita e o da esquerda a esquerda
            Gamepad.current.SetMotorSpeeds(0.2f, 1.0f); 
            //StartCoroutine(DesligarVibracao());

        }
    }
    void PararVibracao()
    {
        if (Gamepad.current != null)
        {
            // Desliga a vibração
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
    IEnumerator DesligarVibracao()
        {
            yield return new WaitForSeconds(0.5f); // Aguarda 0.5 segundos
            Gamepad.current.SetMotorSpeeds(0f, 0f); // Desliga a vibração
        }


}