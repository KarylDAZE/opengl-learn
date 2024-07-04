#version 330 core

out vec4 FragColor;
in vec2 TexCoord;
uniform sampler2D texture1;
uniform sampler2D texture2;
uniform float mixPara=.2;

void main()
{
    FragColor=texture(texture1,TexCoord);
}