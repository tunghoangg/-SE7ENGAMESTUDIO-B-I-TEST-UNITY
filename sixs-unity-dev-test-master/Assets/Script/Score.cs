using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public ParticleSystem confettiExplosion;
    public GameObject camera;
    private CameraMovement cameraMovement;
    // Start is called before the first frame update
    void Start()
    {
        cameraMovement = camera.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal")) {
            cameraMovement.ChangeTargetTemporarily(transform, 2f);
            confettiExplosion.transform.position = transform.position;
            confettiExplosion.Play();
        }
    }
    public void ReloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
