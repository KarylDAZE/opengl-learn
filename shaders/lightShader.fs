#version 330 core

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoord;

out vec4 FragColor;

struct Material{
    //vec3 ambient;
    sampler2D diffuse;
    sampler2D specular;
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

uniform vec3 viewPos=vec3(0);

void main()
{
    vec3 ambient=vec3(texture(material.diffuse,TexCoord))*light.ambient;
    
    vec3 norm=normalize(Normal);
    vec3 lightDir=normalize(light.position-FragPos);
    vec3 diffuse=max(dot(norm,lightDir),0.)*vec3(texture(material.diffuse,TexCoord))*light.diffuse;
    
    vec3 viewDir=normalize(viewPos-FragPos);
    vec3 reflectDir=reflect(-lightDir,norm);
    vec3 specular=vec3(texture(material.specular,TexCoord))*light.specular*pow(max(dot(viewDir,reflectDir),0.),material.shininess);
    
    FragColor=vec4(ambient+diffuse+specular,1.f);
}