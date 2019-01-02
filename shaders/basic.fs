#version 330 core

in vec2 posPass;

out vec4 FragColor;

void main()
{
	vec2 pos = posPass;
	pos = pos * 1.75f - 0.75f;
	pos.y += 0.75f;
	pos.y /= 1920.0f / 1080.0f;
	
	int maxLoop = 50;
	vec2 c = pos;
	vec2 z = vec2(0.0f, 0.0f);
	vec3 color = vec3(0.0f, 0.0f, 0.0f);
	float escapeRadius = 2.0f;
	float ERSQUARED = escapeRadius * escapeRadius;
	
	int i;
	
	for (i = 0; i < maxLoop && dot(z, z) <= ERSQUARED; ++i)
	{
		float xtemp = z.x * z.x - z.y * z.y + c.x;
		z.y = 2 * z.x * z.y + c.y;
		z.x = xtemp; 
	}

	color.b = mix(0.0f, 1.0f, float(i)/maxLoop);

	FragColor = vec4(color, 1.0f);
}

