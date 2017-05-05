using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MovieController : MonoBehaviour {
    [SerializeField] private bool autoplay = true;

	private AudioSource m_audioSource;
	private MovieTexture m_movieTexture;

	void Awake () {
		m_audioSource = GetComponent<AudioSource> ();
		m_movieTexture = GetComponent<RawImage> ().texture as MovieTexture;
	}	

	void OnEnable()
    {
        if (autoplay)StreamMovieOil(true);
    }

    public void ListenerCloseVideo()
    {
        m_movieTexture.Stop();
        m_audioSource.Stop();
    }

	public void StreamMovieOil(bool playMovie)
	{
		if (m_movieTexture != null && m_audioSource != null) {
			if (playMovie) {			
				m_movieTexture.Play ();
				m_audioSource.Play ();
			} else {			
				m_movieTexture.Stop ();
				m_audioSource.Stop ();
			}
		}
	}
}
