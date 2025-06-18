using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void ExplodeCubes(ExplodableCube[] cubes, Vector3 explosionCenter)
    {
        if (cubes == null) 
            return;

        foreach (var cube in cubes)
        {
            if (cube == null) 
                continue;

            float distance = Vector3.Distance(cube.transform.position, explosionCenter);

            if (distance <= cube.ExplosionRadius)
            {
                float force = cube.ExplosionForce * (1 - distance / cube.ExplosionRadius);

                cube.CubeRigidbody.AddExplosionForce(
                    force,
                    explosionCenter,
                    cube.ExplosionRadius,
                    0,
                    ForceMode.Impulse
                );
            }
        }
    }
}