#version 330 core

layout (location = 0) in vec2 aPos;

out vec2 pos;

void main()
{
	pos = aPos;
	pos.x *= 2.5f;
	gl_Position = vec4(pos, 0.0f, 1.0f);
}

