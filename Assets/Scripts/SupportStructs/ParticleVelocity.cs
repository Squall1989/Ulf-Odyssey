using UnityEngine;

public struct ParticleVisualParams
{
    public float speed;
    public float emission;

    public ParticleVisualParams(ParticleSystem particleSystem)
    {
        speed = particleSystem.main.simulationSpeed;
        emission = particleSystem.emission.rateOverTime.constant;
    }

    public void ChangeParticle(ParticleSystem particle, in ParticleVelocity velocity)
    {
        float plusSpeed = velocity.SpeedInterpolated / 5f;
        Vector2 particleDir = velocity.NormReverseDir;
        particle.transform.rotation = Quaternion.Euler(0, plusSpeed, 0);
    }
}

public struct ParticleVelocity
{
    public ParticleVelocity(Vector2 startPos, float interpolatePower)
    {
        lastPos = startPos;
        interpolSpeed = 0;
        interpolDir = Vector2.down;
        interpolPow = interpolatePower;
        interpolRest = 1f - interpolatePower;
    }

    private readonly float interpolPow;
    private readonly float interpolRest;
    private Vector2 lastPos;
    private float interpolSpeed;
    private Vector2 interpolDir;

    public float SpeedInterpolated => interpolSpeed;
    public Vector2 NormReverseDir => (-interpolDir).normalized;
    public Vector2 LastPos 
    { 
        get => lastPos; 
        set 
        {
            CalculateDeltas(value);
            lastPos = value;
        } 
    }

    private void CalculateDeltas(Vector2 newPos)
    {
        Vector2 dir = newPos - lastPos;
        float dist = dir.magnitude;
        float deltaSpeed = dist / Time.deltaTime;
        interpolSpeed = deltaSpeed * interpolPow + interpolSpeed * interpolRest;
        interpolDir = interpolDir * interpolRest + dir * interpolPow;
    }
}
