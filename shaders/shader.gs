#version 330 core
layout(triangles)in;
layout(triangle_strip,max_vertices=3)out;

out vec3 Normal;
out vec3 FragPos;
out vec2 TexCoords;

in VS_OUT{
    vec3 position;
    vec3 normal;
    vec2 texCoords;
}gs_in[];

layout(std140)uniform Matrices
{
    mat4 view;
    mat4 projection;
};

uniform float time;

vec3 GetNormal()
{
    vec3 a=vec3(gl_in[0].gl_Position)-vec3(gl_in[1].gl_Position);
    vec3 b=vec3(gl_in[2].gl_Position)-vec3(gl_in[1].gl_Position);
    return normalize(cross(a,b));
}

vec4 explode(vec4 position,vec3 normal)
{
    float magnitude=.3;
    vec3 direction=normal*((sin(time)+1.)/2.)*magnitude;
    return projection*(position+vec4(direction,0.));
}

void main(){
    Normal=gs_in[0].normal;
    gl_Position=explode(vec4(gs_in[0].position,1),Normal);
    FragPos=gs_in[0].position;
    TexCoords=gs_in[0].texCoords;
    EmitVertex();
    Normal=gs_in[1].normal;
    gl_Position=explode(vec4(gs_in[1].position,1),Normal);
    FragPos=gs_in[1].position;
    TexCoords=gs_in[1].texCoords;
    EmitVertex();
    Normal=gs_in[2].normal;
    gl_Position=explode(vec4(gs_in[2].position,1),Normal);
    FragPos=gs_in[2].position;
    TexCoords=gs_in[2].texCoords;
    EmitVertex();
    EndPrimitive();
}