void GaussianBlur_float(UnityTexture2D Texture, float2 UV, float2 Blur, UnitySamplerState Sampler, out float3 Out_RGB, out float Out_Alpha)
{
    float4 col = float4(0.0, 0.0, 0.0, 0.0);
    float kernelSum = 0.0;

    int upperX = ((Blur.x - 1) / 2);
    int lowerX = -upperX;
    int upperY = ((Blur.y - 1) / 2);
    int lowerY = -upperY;

    for (int x = lowerX; x <= upperX; ++x)
    {
        for (int y = lowerY; y <= upperY; ++y)
        {
            kernelSum++;

            float2 offset = float2(_MainTex_TexelSize.x * x, _MainTex_TexelSize.y * y);
            col += Texture.Sample(Sampler, UV + offset);
        }
    }

    col /= kernelSum;
    Out_RGB = float3(col.r, col.g, col.b);
    Out_Alpha = col.a;
}