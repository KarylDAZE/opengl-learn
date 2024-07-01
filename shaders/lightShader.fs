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
    vec3 direction;
    float cutOff;
    float outerCutOff;
    
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    
    float constant;
    float linear;
    float quadratic;
};

#define NR_POINT_LIGHTS 4
uniform Light dirLight,spotLight,pointLights[NR_POINT_LIGHTS];

uniform vec3 viewPos=vec3(0);

vec3 CalcDirLight(Light light,vec3 normal,vec3 viewDir);
vec3 CalcPointLight(Light light,vec3 normal,vec3 fragPos,vec3 viewDir);
vec3 CalcSpotLight(Light light,vec3 normal,vec3 fragPos,vec3 viewDir);

void main()
{
    vec3 norm=normalize(Normal);
    vec3 viewDir=normalize(viewPos-FragPos);
    vec3 result=vec3(0);
    result+=CalcDirLight(dirLight,norm,viewDir);
    for(int i=0;i<1;i++){
        result+=CalcPointLight(pointLights[i],norm,FragPos,viewDir);
    }
    result+=CalcSpotLight(spotLight,norm,FragPos,viewDir);
    FragColor=vec4(result,1);
}

vec3 CalcDirLight(Light light,vec3 normal,vec3 viewDir)
{
    vec3 lightDir=normalize(-light.direction);
    
    vec3 ambient=light.ambient*vec3(texture(material.diffuse,TexCoord));
    vec3 diffuse=light.diffuse*max(dot(normal,lightDir),0.)*vec3(texture(material.diffuse,TexCoord));
    vec3 reflectDir=reflect(-lightDir,normal);
    vec3 specular=light.specular*pow(max(dot(viewDir,reflectDir),0.),material.shininess)*vec3(texture(material.specular,TexCoord));
    return(ambient+diffuse+specular);
}

vec3 CalcPointLight(Light light,vec3 normal,vec3 fragPos,vec3 viewDir)
{
    vec3 lightDir=normalize(light.position-fragPos);
    float distance=length(light.position-fragPos);
    float attenuation=1./(light.constant+light.linear*distance+light.quadratic*(distance*distance));
    
    vec3 ambient=light.ambient*vec3(texture(material.diffuse,TexCoord));
    vec3 diffuse=light.diffuse*max(dot(normal,lightDir),0.)*vec3(texture(material.diffuse,TexCoord));
    vec3 reflectDir=reflect(-lightDir,normal);
    vec3 specular=light.specular*pow(max(dot(viewDir,reflectDir),0.),material.shininess)*vec3(texture(material.specular,TexCoord));
    ambient*=attenuation;
    diffuse*=attenuation;
    specular*=attenuation;
    return(ambient+diffuse+specular);
}

vec3 CalcSpotLight(Light light,vec3 normal,vec3 fragPos,vec3 viewDir)
{
    vec3 lightDir=normalize(light.position-FragPos);
    float cosTheta=dot(lightDir,normalize(-light.direction));
    float deltaCosEpsilon=light.cutOff-light.outerCutOff;
    float distance=length(light.position-FragPos);
    float attenuation=1./(light.constant+light.linear*distance+light.quadratic*(distance*distance));
    float intensity=clamp((cosTheta-light.outerCutOff)/deltaCosEpsilon,0.,1.);
    
    vec3 ambient=vec3(texture(material.diffuse,TexCoord))*light.ambient;
    vec3 diffuse=max(dot(normal,lightDir),0.)*vec3(texture(material.diffuse,TexCoord))*light.diffuse;
    vec3 reflectDir=reflect(-lightDir,normal);
    vec3 specular=vec3(texture(material.specular,TexCoord))*light.specular*pow(max(dot(viewDir,reflectDir),0.),material.shininess);
    ambient*=attenuation;
    diffuse*=attenuation*intensity;
    specular*=attenuation*intensity;
    return(ambient+diffuse+specular);
}