#version 330 core
layout(location=0)in vec3 aPos;
layout(location=1)in vec3 aColor;
out vec4 vertexColor;
out vec3 outPos;
uniform vec3 posOffset;
void main()
{
   gl_Position=vec4(aPos+posOffset,1);
   outPos=gl_Position.xyz;
   vertexColor=vec4(aColor,1);
}