#version 330 core
out vec4 FragColor;
in vec4 vertexColor;
in vec3 outPos;
//uniform vec4 ourColor;
void main()
{
    FragColor=vec4((outPos+vec3(1,1,1))/2,1);
}