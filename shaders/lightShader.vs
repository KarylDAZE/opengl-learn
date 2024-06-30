#version 330 core

layout(location=0)in vec3 aPos;
layout(location=1)in vec3 aNormal;

out vec3 Normal;
out vec3 FragPos;

uniform mat4 model,view,projection;

void main()
{
    gl_Position=projection*view*model*vec4(aPos,1);
    Normal=vec3(view*vec4(mat3(transpose(inverse(model)))*aNormal,0));
    FragPos=vec3(view*model*vec4(aPos,1.));
}