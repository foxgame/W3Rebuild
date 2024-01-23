#ifndef W3BASE_CGINC
#define W3BASE_CGINC



// max size 1024 * 1024 ( 256 grids * 4 pixels )
sampler2D_float W3FogTex0;
sampler2D_float W3ShadowTex0;
sampler2D W3ShadowTex1;

float W3ShadowCameraPosX;
float W3ShadowCameraPosZ;
float W3ShadowCameraWidth;
float W3ShadowCameraHeight;

#define W3FogTex( uv ) tex2D( W3FogTex0 , uv ).a
#define W3ShadowTex( uv ) tex2D( W3ShadowTex0 , uv ).a


float W3Width;
float W3Height;
float W3FogWidth;
float W3FogHeight;

// color of a day
float4 W3ColorDay0;


// terrain texture 1024 * 1024 ( terrain 256 * 256 * 14 ( base )+ 256 * 256 * 2 ( blight ) )
sampler2D W3TerrainTex0;




#endif
