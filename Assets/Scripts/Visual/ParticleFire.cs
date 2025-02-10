using UnityEngine;

[ExecuteInEditMode]
public class ParticleFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_ParticleSystem;

    private ParticleVelocity particleVelocity;
    private ParticleVisualParams particleVisual;

    void Start()
    {
        particleVelocity = new(transform.position, .2f);
        particleVisual = new ParticleVisualParams(m_ParticleSystem);
    }

    void Update()
    {
        particleVelocity.LastPos = transform.position;

        for (int i = 0; i < transform.childCount; i++)
        {
            var particle = transform.GetChild(i).GetComponent<ParticleSystem>();

        }
    }
}