using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Respawn : MonoBehaviour
{
    private Vector3 respawnPosition;
    public GameObject player;
    public AudioClip respawn;
    private AudioSource audioSource;

    private bool audioPlaying = false; // Variável para controlar se o áudio está a dar

    void Start()
    {
        GameObject respawnPositionObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnPositionObject != null)
        {
            respawnPosition = respawnPositionObject.transform.position;
            PlayerController playerController = new PlayerController();
            playerController.location = "madeira";
        }
        else
        {
            Debug.LogError("Objeto com a tag 'posicao inicial' não encontrado!");
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifica se a ação "buttonWest" foi acionada no gamepad
        if (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("Respawn!");
            // Volta para o respawn
            player.transform.position = respawnPosition;
            // Toca o áudio se não estiver a tocar
            if (!audioPlaying)
            {
                PlayRespawnAudio();
            }
        }
    }

    // Método para reproduzir o áudio de respawn
    void PlayRespawnAudio()
    {
        audioSource.clip = respawn;
        audioSource.Play();
        audioPlaying = true; 
        StartCoroutine(WaitForAudio()); 
    }

    // Coroutine para esperar até que o áudio termine de tocar antes de redefinir a variável de controle
    IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length); // Espera até que o áudio termine de tocar
        audioPlaying = false; // Reseta a variável de controle quando o áudio termina de tocar
        audioSource.Stop();
    }
}
