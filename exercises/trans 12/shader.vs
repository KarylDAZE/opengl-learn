#version 330 core

layout(location=0)in vec3 aPos;
layout(location=1)in vec3 aColor;
layout(location=2)in vec2 aTexCoord;

out vec4 vertexColor;
out vec3 outPos;
uniform vec3 posOffset;
out vec2 TexCoord;

uniform mat4 transform;

void main()
{
   gl_Position=transform*vec4(aPos+posOffset,1);
   outPos=gl_Position.xyz;
   TexCoord=aTexCoord;
}