using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class W3ShadowCaster : MonoBehaviour
{
    public int targetSize = 1024;
    public float shadowBias = 0.005f;
    public float cameraWidth = 10.0f;
    public float cameraHeight= 10.0f;

    private Camera cam;
    private RenderTexture depthTarget;

    Texture2D t2d;
    private void Start()
    {
//        t2d = Resources.Load< Texture2D >( "Tile_2" );
        Shader.SetGlobalTexture( "W3ShadowTex1" , depthTarget );
    }

    private void OnEnable()
    {
        UpdateResources();
    }
    
    private void OnValidate()
    {
        UpdateResources();
    }

    private void UpdateResources()
    {
        if( cam == null )
        {
            cam = GetComponent<Camera>();
            cam.depth = -1000;
        }

        if( depthTarget == null ||
            depthTarget.width != targetSize )
        {
            int sz = Mathf.Max( targetSize , 16 );
            depthTarget = new RenderTexture(sz, sz, 16, RenderTextureFormat.ARGB32 , RenderTextureReadWrite.Linear);
            depthTarget.wrapMode = TextureWrapMode.Clamp;
            depthTarget.filterMode = FilterMode.Bilinear;
            depthTarget.autoGenerateMips = false;
            depthTarget.useMipMap = false;
            cam.targetTexture = depthTarget;
        }
    }

    private void OnPostRender()
    {
        Shader.SetGlobalFloat( "W3ShadowCameraPosX" , transform.position.x );
        Shader.SetGlobalFloat( "W3ShadowCameraPosZ" , transform.position.z );
        Shader.SetGlobalFloat( "W3ShadowCameraWidth" , cameraWidth );
        Shader.SetGlobalFloat( "W3ShadowCameraHeight" , cameraHeight );



        //         var bias = new Matrix4x4() {
        //             m00 = 0.5f, m01 = 0,    m02 = 0,    m03 = 0.5f,
        //             m10 = 0,    m11 = 0.5f, m12 = 0,    m13 = 0.5f,
        //             m20 = 0,    m21 = 0,    m22 = 0.5f, m23 = 0.5f,
        //             m30 = 0,    m31 = 0,    m32 = 0,    m33 = 1,
        //         };
        //         
        //         Matrix4x4 view = cam.worldToCameraMatrix;
        //         Matrix4x4 proj = cam.projectionMatrix;
        //         Matrix4x4 mtx = bias * proj * view;
        //         
        //         Shader.SetGlobalMatrix("_ShadowMatrix", mtx);
        //         Shader.SetGlobalTexture("_ShadowTex", depthTarget);
        //         Shader.SetGlobalFloat("_ShadowBias", shadowBias);
    }
}
