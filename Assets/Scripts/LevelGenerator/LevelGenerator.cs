using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public ColorToPrefab[] prefabs;
    public Texture2D level;

    private void Start() {
        GenerateLevel(level);
    }

    private void GenerateLevel(Texture2D image) {
        // Go trough the image
        for (int x = 0; x < image.width; x++) { 
            for(int y = 0; y < image.height; y++) {
                // Get the color
                Color color = image.GetPixel(x, y);
                // Find the apropriate prefab
                foreach (ColorToPrefab c in prefabs) {
                    // Compare colors
                    if (c.color.Equals(color)) {
                        Vector2 pos = new Vector2(x - image.width / 2, y - image.height / 2);
                        // Always put ground beneath, unless it is a path
                        if (!c.prefab.name.Contains("Path"))
                            Instantiate(prefabs[0].prefab, pos, Quaternion.identity, transform);
                        // Instantiate the object
                        FindObjectOfType<GameMaster>().objects[x,y] = Instantiate(c.prefab, pos, Quaternion.identity, transform);
                        continue;
                    }
                }
            }
        }
    }
}
