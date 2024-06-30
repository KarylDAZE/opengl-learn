#version 330 core

in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform float ambientStrength=.1;
uniform vec3 lightPos;
uniform vec3 viewPos=vec3(0);
uniform float specularStrength=.5;
uniform int shininess=32;

void main()
{
    vec3 result=vec3(0);
    vec3 ambient=ambientStrength*lightColor;
    
    vec3 norm=normalize(Normal);
    vec3 lightDir=normalize(lightPos-FragPos);
    vec3 diffuse=max(dot(norm,lightDir),0.)*lightColor;
    
    vec3 viewDir=normalize(viewPos-FragPos);
    vec3 reflectDir=reflect(-lightDir,norm);
    vec3 specular=specularStrength*pow(max(dot(viewDir,reflectDir),0.),shininess)*lightColor;
    
    result=(ambient+diffuse+specular)*objectColor;
    FragColor=vec4(result,1.f);
}