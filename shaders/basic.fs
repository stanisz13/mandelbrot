#version 330 core

in vec2 posPass;

out vec4 FragColor;

void main()
{
	vec2 pos = posPass;
	pos = pos * 1.75f - 0.75f;
	pos.y += 0.75f;
	pos.y /= 1920.0f / 1080.0f;
	
	int maxLoop = (1<<8);
	vec2 c = pos;
	vec2 z = vec2(0.0f, 0.0f);
	vec3 color = vec3(0.0f, 0.0f, 0.0f);
	float escapeRadius = 2.0f;
	float ERSQUARED = escapeRadius * escapeRadius;
	
	float i;
	
	for (i = 0; i < maxLoop && dot(z, z) <= ERSQUARED; ++i)
	{
		float xtemp = z.x * z.x - z.y * z.y + c.x;
		z.y = 2 * z.x * z.y + c.y;
		z.x = xtemp; 
	}

	if ( i < maxLoop )
     	{
		float diff = z.x*z.x + z.y*z.y; 
		float log_zn = log(diff) / 2.0f;
		const float two = 2.0f;
		float nu = log( log_zn / log(two) ) / log(two);

		i = i + 1 - nu;
	}
	
	float val1 = mix(0.0f, 1.0f, float(floor(i))/maxLoop);
	float val2 = mix(0.0f, 1.0f, float(floor(i) + 1.0f)/maxLoop);

	vec3 color1 = vec3(val2, 0.0f, 0.0f);
	vec3 color2 = vec3(0.0f,val1/2.0f,  0.0f);
	
	color = mix(color1, color2, i / maxLoop);
	//color.b = mix(0.0f, 1.0f, float(i)/maxLoop);

	FragColor = vec4(color, 1.0f);
}

