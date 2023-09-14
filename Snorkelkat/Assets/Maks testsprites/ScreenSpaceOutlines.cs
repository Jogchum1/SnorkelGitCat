using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceOutlines : ScriptableRendererFeature
{

[System.Serializable]
private class viewSpaceNormalsTextureSettings {...}

private class ViewSpaceNormalsTexturePass :
    ScriptableRenderPass { 

    private FilteringSettings filteringSettings;

    private viewSpaceNormalsTextureSettings
        normalsTextureSettings;
    private readonly List<ShaderTagId> shaderTagIdList;
    private readonly RenderTargetHandle normals;
    private readonly Material normalsMaterial;
    
    public ViewSpaceNormalsTexturePass(RenderPassEvent renderPassEvent, layerMask outlinesLayerMask, ViewSpaceNormalsTextureSettings settings) {
    normalsMaterial = new Material(
        Shader.Find("Hidden/ViewSpaceNormalsShader"));
    shaderTagIdList = new List<ShaderTagId> {
    new shaderTagId("UniversalForward"),
    new shaderTagId("UniversalForwardOnly"),
    new shaderTagId("LightweightForward"),
    new shaderTagId("SRPDefaultUnlit"),
    }
        this.renderPassEvent = renderPassEvent;
        normals.Init("_SceneViewSpaceNormals");
        filteringSettings = new filteringSettings(
            RenderQueueRange.opaque, outlinesLayerMask);
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor) {
    RenderTextureDescriptor normalsTextureDescriptor = cameraTextureDescriptor;
    normalsTextureDescriptor.colorFormat = normalsTextureSettings.colorFormat;
    normalsTextureDescriptor.depthBufferBits = normalsTextureSettings.depthBufferBits;
        cmd.GetTemporaryRT(normals.id, cameraTextureDescriptor, FilterMode.Point);
        ConfigureTarget(normals.Identifier());
        ConfigureClear(ClearFlag.All, normalsTextureSettings.backgroundColor);
    }

    public override void Execute (ScriptableRendererContext context, ref RenderingData renderingData) {
    if (!normalsMaterial)
        return;

    CommandBuffer cmd = CommandBufferPool.Get();
    using (new ProfilingScope(cmd, new ProfilingSampler("SceneViewSpaceNormalsTextureCreation"))) {
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();
        DrawingSettings drawSettings = CreateDrawingSettings(
            shaderTagIdList, ref renderingData,
            renderingData.cameraData.defaultOpaqueSortFlags);
            drawSettings.overrideMaterial = normalsMaterial;
//            FilteringSettings.defaultValue;
        context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);
       }
       context.ExecuteCommandBuffer(cmd);
       CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd) {
        cmd.ReleaseTemporaryRT(normals.id);
    }

    }

private class ScreenSpaceOutlinePass :
    ScriptableRenderPass {

    private readonly Material screenSpaceoutlineMaterial;
    private RenderTargetIdentifier cameraColorTarget;
    private int temporaryBufferID = Shader.PropertyToID("_TemporaryBuffer");
    
    public ScreenSpaceOutlinePass(RenderPassEvent renderPassEvent) {
        this.renderPassEvent = renderPassEvent;
        screenSpaceoutlineMaterial = new Material(
            Shader.Find("Hidden/OutlineShader"));
    } 
    
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData){
        cameraColorTarget = renderingData.cameraData.renderer.cameraColorTarget;
    }

    public override void Execute(ScriptableRendererContext context, ref RenderingData renderingData){
        if (!screenSpaceoutlineMaterial)
            return;

        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, new ProfilingSampler("ScreenSpaceOutlines"))){
        Blit(cmd, cameraColorTarget, temporaryBuffer);
            Blit(cmd, temporaryBuffer, cameraColorTarget, screenSpaceoutlineMaterial);
        }
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    }

    [SerializeField] private RenderPassEvent renderPassEvent;

        public override void Create() {
            viewSpaceNormalsTexturePass =
            new ViewSpaceNormalsTexturePass(renderPassEvent);
            screenSpaceOutlinePass =
            new ScreenSpaceOutlinePass(renderPassEvent);
        }
        
        public override void AddRenderPasses(ScriptableRenderer 
            renderer, ref RenderingData renderingData) {
            renderer.EnqueuePass(ViewSpaceNormalsTexturePass);
            renderer.EnqueuePass(ScreenSpaceOutlinePass);
        }

    [SerializeField] private viewSpaceNormalsTextureSettings
        viewSpaceNormalsTextureSettings;

    [SerializeField] private LayerMask outlinesLayerMask;
}
