using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashDuration;
    public float freezeDuration;
    private float _startTime;
    [HideInInspector]
    public bool flashing;
    private Light2D _flash;

    private List<GameObject> _targets = new List<GameObject>();
    
    void Start()
    {
        _flash = GetComponent<Light2D>();
    }

    public void CameraFlash()
    {
        Debug.Log(_targets.Count);
        _startTime = Time.time;
        StartCoroutine(FlashCoroutine());
        foreach (var target in _targets)
        {
            target.GetComponent<BaseGhostAI>().RevealGhost(flashDuration);
            target.GetComponent<BaseGhostAI>().FreezeGhost(freezeDuration);
        }
    }

    private IEnumerator FlashCoroutine()
    {
        bool done = false;
        flashing = true;
        while(!done)
        {
            _flash.intensity = 3; 

            float perc;
            
            perc = Time.time - _startTime;
            perc = perc / flashDuration;
            if(perc > flashDuration)
            {
                done = true;
            }
            yield return null;
        }
        _flash.intensity = 0;
        flashing = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (_targets.Contains(other.gameObject)) { return; }
        _targets.Add(other.gameObject);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        _targets.Remove(other.gameObject);
    }
}


