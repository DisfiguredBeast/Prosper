#version 400

in vec3 v_position;
in vec3 v_color;
in vec2 v_texCoords;

out vec4 vColor;
out vec2 f_texCoords;

uniform mat4 u_modelview;

void main()
{
    gl_Position = u_modelview * vec4(v_position, 1.0);
    vColor = vec4(v_color, 1.0);
    f_texCoords = v_texCoords;
}
