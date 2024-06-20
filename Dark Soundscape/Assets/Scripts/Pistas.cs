using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistas : MonoBehaviour
{
   
    private AudioSource audioSource;
    public bool pistasActive = false;

    private List<AudioSource> audioEspacialSources = new List<AudioSource>();
    private Dictionary<AudioSource, float> originalVolumes = new Dictionary<AudioSource, float>();

    private float lastPistasTime = -Mathf.Infinity; // Inicializa como -Infinity para que não ative automaticamente
    private float cooldownDuration = 60f; // 1 minuto p as pistas voltarem a ativar
    public AudioClip errorSound;
    private AudioSource errorAudioSource;
    public AudioClip bipSound;
    private AudioSource bipAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        errorAudioSource = gameObject.AddComponent<AudioSource>();
        errorAudioSource.clip = errorSound;
        bipAudioSource = gameObject.AddComponent<AudioSource>();
        bipAudioSource.clip = bipSound;

        // Encontra todos os audios com a tag audioEspacial e armazena-os na lista
        GameObject[] audioEspacialObjects = GameObject.FindGameObjectsWithTag("audioEspacial");
        foreach (GameObject obj in audioEspacialObjects)
        {
            AudioSource audio = obj.GetComponent<AudioSource>();
            if (audio != null)
            {
                audioEspacialSources.Add(audio);
                if (!originalVolumes.ContainsKey(audio))
                {
                    originalVolumes[audio] = audio.volume; // Armazena o volume original
                }
            }
        }
    }

    // Update is called once per frame
     void Update()
    {
        // Verifica se a ação "buttonEast" foi acionada no gamepad
        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            if (Time.time - lastPistasTime >= cooldownDuration)
            {
                // Registra um log no console
                Debug.Log("Pistas ativas!");
                bipAudioSource.Play(); 
                pistasActive = true;
                lastPistasTime = Time.time; // Atualiza o tempo de último uso
                StartCoroutine(ChangePistas());
            }
            else
            {
                Debug.Log("Pistas ainda não disponiveis!.");
                errorAudioSource.Play(); 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (pistasActive)
        {
            if (other.tag == "Player")
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    Debug.Log("currentDirection: " + playerController.currentDirection);
                    //se a tag de collisao e igual a de rotacao
                    if (this.CompareTag(playerController.currentDirection))
                    {
                        //procura a caixa de som com essa direcao e toca
                        GameObject[] cubes = GameObject.FindGameObjectsWithTag(playerController.currentDirection);
                        foreach (GameObject cube in cubes)
                        {
                            AudioSource audio = cube.GetComponent<AudioSource>();
                            if (audio != null)
                            {
                                audio.Play();
                            }
                        }
                    }
                    if (this.CompareTag("Errado"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("Errado1");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        if (audio1 != null)
                        {
                            audio1.Play();
                        }
                    }

                    if (this.CompareTag("duplaOeste") && playerController.currentDirection == "Oeste")
                    {
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaOeste1");
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaOestee");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaNorte"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaNorte1");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaNorte2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaSul") && playerController.currentDirection == "Sul")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaSul1");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaSul2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("Errado4"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("Errado2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        if (audio1 != null)
                        {
                            audio1.Play();
                        }
                    }
                    if (this.CompareTag("duplaN") && playerController.currentDirection == "Oeste")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaN1");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaN2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaNorteV2") && playerController.currentDirection == "Norte")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaNorteV21");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaNorteV22");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaOesteV2") && playerController.currentDirection == "Oeste")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaOesteV21");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaOesteV22");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaOesteV3") && playerController.currentDirection == "Oeste")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaOesteV311");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaOesteV31");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaOesteV4"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaOesteV41");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaOesteV42");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("OesteV1"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("OesteV1");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        if (audio1 != null)
                        {
                            audio1.Play();
                        }
                    }
                   
                    if (this.CompareTag("duplaOesteV4"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaOesteV41");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaOesteV42");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("sul1")  && playerController.currentDirection == "Sul")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("sul3");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("sul2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }

                    if (this.CompareTag("Este1") && playerController.currentDirection == "Este")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("Este2");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("Este3");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaSulV4") && playerController.currentDirection == "Sul")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaSulV41");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaSulV42");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("duplaE"))
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("duplaE1");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("duplaE2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("2duplaE") && playerController.currentDirection == "Este")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("2duplaE2");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("2duplaE1");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                    if (this.CompareTag("do") && playerController.currentDirection == "Oeste")
                    {
                        GameObject[] cubes1 = GameObject.FindGameObjectsWithTag("do1");
                        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("do2");

                        AudioSource audio1 = cubes1[0].GetComponent<AudioSource>();
                        AudioSource audio2 = cubes2[0].GetComponent<AudioSource>();
                        if (audio1 != null && audio2 != null)
                        {
                            StartCoroutine(PlaySoundsWithDelay(audio1, audio2));
                        }
                    }
                }
            }
        }
    }

    IEnumerator PlaySoundsWithDelay(AudioSource audio1, AudioSource audio2)
    {
        audio1.Play();
        yield return new WaitForSeconds(0.5f); // Espera por 0.6 segundo
        audio2.Play();
    }
    IEnumerator ChangePistas()
    {     

        // Muda o volume dos audios espacializados para 0 e armazena os volumes originais
        foreach (AudioSource audio in audioEspacialSources)
        {
            if (originalVolumes.ContainsKey(audio))
            {
                audio.volume = 0;
            }
        }
        yield return new WaitForSeconds(9f); // pistas ativas por 9 segundos
        

        // Restaura o volume dos audios espacializados
        foreach (AudioSource audio in audioEspacialSources)
        {
            if (originalVolumes.ContainsKey(audio))
            {
                audio.volume = originalVolumes[audio]; // Restaura o volume original
            }
        }

        bipAudioSource.Play(); 
        pistasActive = false;
    }

}
