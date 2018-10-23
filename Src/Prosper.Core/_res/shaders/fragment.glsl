#version 400

// subroutine definition
subroutine vec4 getColor ();

in vec4 vColor;
in vec2 f_texCoords;

uniform sampler2D u_texture;

subroutine uniform getColor u_subroutine;

out vec4 outColor;

// subroutines
subroutine (getColor ) vec4 sr_byTexture()
{
    return texture(u_texture, f_texCoords);
} 
subroutine (getColor ) vec4 sr_byColor()
{
    return vColor;
}

void main()
{
     outColor = u_subroutine();
}
