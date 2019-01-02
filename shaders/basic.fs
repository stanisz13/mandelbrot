#version 330 core

in vec2 pos;

out vec4 FragColor;

void main()
{
	int maxLoop = 1000;
	vec2 c = pos;
	vec2 z = vec2(0.0f, 0.0f);
	vec3 color = vec3(0.0f, 0.0f, 1.0f);

	for (int i = 0; i < maxLoop; ++i)
	{
		if (z.x*z.x + z.y*z.y > 4.0f)
		{
			color = vec3(0.0f, 0.0f, 0.0f);
			break;
		}

		float xtemp = z.x * z.x - z.y * z.y + c.x;
		z.y = 2 * z.x * z.y + c.y;
		z.x = xtemp; 

		
	}

	FragColor = vec4(color, 1.0f);
}

