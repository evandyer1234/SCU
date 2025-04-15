using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectsController : MonoBehaviour
{
    public Shader postEffectShader;
    Material postEffectMaterial;
    RenderTexture postRenderTexture;
    public Color screenTint;
    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (postEffectMaterial == null)
        {
            postEffectMaterial = new Material(postEffectShader);
        }
        if (postRenderTexture == null)
        {
            postRenderTexture = new RenderTexture(src.width, src.height, 0);
        }
        //postEffectMaterial.SetColor("_ScreenTint", screenTint);
        postEffectMaterial.SetTexture("_MainTex", src);
        Graphics.Blit(src, postRenderTexture, postEffectMaterial, 0);
        Shader.SetGlobalTexture("_GlobalRenderTexture", postRenderTexture);
        Graphics.Blit(src, dst);
    }
}
