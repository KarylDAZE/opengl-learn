#version 330 core

in vec3 Normal;
in vec3 FragPos;

out vec4 FragColor;

struct Material{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};
uniform Material material;

struct Light{
    vec3 position;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
uniform Light light;

uniform vec3 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos=vec3(0);

void main()
{
    vec3 ambient=material.ambient*light.ambient*lightColor;
    
    vec3 norm=normalize(Normal);
    vec3 lightDir=normalize(lightPos-FragPos);
    vec3 diffuse=max(dot(norm,lightDir),0.)*material.diffuse*light.diffuse*lightColor;
    
    vec3 viewDir=normalize(viewPos-FragPos);
    vec3 reflectDir=reflect(-lightDir,norm);
    vec3 specular=material.specular*light.specular*pow(max(dot(viewDir,reflectDir),0.),material.shininess)*lightColor;
    
    vec3 result=ambient+diffuse+specular;
    FragColor=vec4(result,1.f);
}