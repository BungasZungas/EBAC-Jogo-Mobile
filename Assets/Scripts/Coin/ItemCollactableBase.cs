using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlesSystem;
    public float timeToHide = 3;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
    }

    protected virtual void Collect()
    {
        OnCollect();
        HideItens();
        PlayerController.Instance.Bounce();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }
    
    protected virtual void OnCollect() 
    {
        if (particlesSystem != null)
        {
            particlesSystem.transform.SetParent(null);
            particlesSystem.Play();
        }

        if (audioSource != null) audioSource.Play();
    }
}