// Custom Diffuse BSDF Function
float3 CustomDiffuseBSDF(float3 albedo, float roughness, float3 normal, float3 lightDir)
{
    // Normalize inputs
    normal = normalize(normal);
    lightDir = normalize(lightDir);

    // Lambertian Diffuse Reflection
    float NdotL = max(dot(normal, lightDir), 0.0);
    float3 diffuse = albedo * NdotL / PI;

    // Simple roughness effect (can be expanded for more realistic models)
    float roughnessFactor = 1.0 - roughness; // This is a very basic way to use roughness
    diffuse *= roughnessFactor;

    return diffuse;
}
