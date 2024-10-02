#version 330 core

layout(location=0)in vec3 aPos;
layout(location=1)in vec3 aNormal;
layout(location=2)in vec2 aTexCoord;

out vec3 Normal;
out vec3 FragPos;
out vec2 TexCoord;

layout(std140)uniform Matrices
{
    mat4 view;
    mat4 projection;
};

uniform mat4 model;

void main()
{
    gl_Position=projection*view*model*vec4(aPos,1);
    Normal=vec3(view*vec4(mat3(transpose(inverse(model)))*aNormal,0));
    FragPos=vec3(view*model*vec4(aPos,1.));
    TexCoord=aTexCoord;
}