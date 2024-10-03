#version 330 core

layout(location=0)in vec3 aPos;
layout(location=1)in vec3 aNormal;
layout(location=2)in vec2 aTexCoord;

// out vec2 TexCoords;
out VS_OUT{
    vec3 position;
    vec3 normal;
    vec2 texCoords;
}vs_out;

layout(std140)uniform Matrices
{
    mat4 view;
    mat4 projection;
};

uniform mat4 model;

void main()
{
    // gl_Position=projection*view*model*vec4(aPos,1);
    vs_out.normal=vec3(view*vec4(mat3(transpose(inverse(model)))*aNormal,0));
    vs_out.position=vec3(view*model*vec4(aPos,1.));
    vs_out.texCoords=aTexCoord;
}