#version 330 core

in vec2 posPass;

out vec4 FragColor;

const float aRatio = 1080.0f / 1920.0f;

float minRe = -1.0f;
float maxRe = 1.0f;

const float zoom = 100.0f;

vec2 mapPoint(const vec2 v)
{
       vec2 res;

       float sumRe = maxRe + minRe;
       
       res = v * (maxRe - minRe)/2.0f + (sumRe / 2.0f);
       res.y -= (sumRe) / 2.0f;

       res.y *= aRatio;

       res.y /= zoom;
       res.x /= zoom;

       return res;
}

void main()
{
	vec2 pos = posPass;
	vec2 focus = vec2(-0.1011f, 0.9563f);
	vec2 focus2 = vec2(-0.7453f, 0.1127f);

	pos = mapPoint(pos);

	pos += focus2;
	
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
		float logTwo = log(two);
		float nu = log( log_zn / logTwo ) / logTwo;

		i = i + 1 - nu;
	}
	
	float val1 = mix(0.0f, 1.0f, float(floor(i))/maxLoop);
	float val2 = mix(0.0f, 1.0f, float(floor(i) + 1.0f)/maxLoop);

	vec3 color1 = vec3(val2, 0.0f, 0.0f);
	vec3 color2 = vec3(0.0f,val1/2.0f, 0.3f);
	
	color = mix(color1, color2, i / maxLoop);
	//color.b = mix(0.0f, 1.0f, float(i)/maxLoop);

	FragColor = vec4(color, 1.0f);
}

