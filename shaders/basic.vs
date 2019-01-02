#version 330 core

layout (location = 0) in vec2 aPos;

out vec2 posPass;

void main()
{
	posPass = aPos;
	gl_Position = vec4(posPass, 0.0f, 1.0f);
}

