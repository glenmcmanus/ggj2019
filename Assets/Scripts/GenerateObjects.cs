using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject medicinePrefab;
    public GameObject foodPrefab;
    public GameObject[] critterPrefabs;

    public float yPos = 0;
    public BoxCollider box;

    List<GameObject> trees;
    List<GameObject> rocks;
    List<GameObject> medicine;
    List<GameObject> food;
    List<GameObject> critters;

    public int maxTrees = 250;
    public int maxRocks = 250;
    public int maxMedicine = 250;
    public int maxFood = 250;
    public int maxCritters = 75;

    [Tooltip("Minimum distance between objects")]
    public float minDist = 1f;

    private void Awake()
    {
        box.enabled = false;
        enabled = false;
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        trees = new List<GameObject>();
        rocks = new List<GameObject>();
        medicine = new List<GameObject>();
        food = new List<GameObject>();
        critters = new List<GameObject>();

        for (int i = transform.childCount - 1; i >= 0; i-- )
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        int treeCounter = 0;
        int rockCounter = 0;
        int medicineCounter = 0;
        int foodCounter = 0;
        int critterCounter = 0;

        while (treeCounter < maxTrees || rockCounter < maxRocks || medicineCounter < maxMedicine || critterCounter < maxCritters)
        {
            Vector3 pos = RandomPos();
            treeCounter++;

            if (treeCounter < maxTrees && ValidPosition(pos))
            {
                GameObject t = Instantiate(treePrefab, pos, treePrefab.transform.rotation);
                t.transform.SetParent(transform);
                trees.Add(t);
            }

            pos = RandomPos();
            rockCounter++;

            if (rockCounter < maxRocks && ValidPosition(pos))
            {
                
                GameObject r = Instantiate(rockPrefab, pos, treePrefab.transform.rotation);
                r.transform.SetParent(transform);
                rocks.Add(r);
            }

            pos = RandomPos();
            medicineCounter++;

            if(medicineCounter < maxMedicine && ValidPosition(pos))
            {
                GameObject m = Instantiate(medicinePrefab, pos, medicinePrefab.transform.rotation);
                m.transform.SetParent(transform);
                medicine.Add(m);
            }

            pos = RandomPos();
            foodCounter++;

            if(foodCounter < maxFood && ValidPosition(pos))
            {
                GameObject f = Instantiate(foodPrefab, pos, foodPrefab.transform.rotation);
                f.transform.SetParent(transform);
                food.Add(f);
            }

            pos = RandomPos();
            critterCounter++;

            if(critterCounter < maxCritters && ValidPosition(pos))
            {
                int index = Random.Range(0, critterPrefabs.Length);

                GameObject c = Instantiate(critterPrefabs[index], pos, critterPrefabs[index].transform.rotation);
                c.transform.SetParent(transform);
                critters.Add(c);
            }
        }
    }

    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(Mathf.Floor(-box.size.x / 2), Mathf.Ceil(box.size.x / 2)) + transform.position.x,
                                      yPos,
                                      Random.Range(Mathf.Floor(-box.size.z / 2), Mathf.Ceil(box.size.z / 2)) + transform.position.z);
    }

    bool ValidPosition(Vector3 pos)
    {
        foreach (GameObject tree in trees)
        {
            if (Vector3.Distance(tree.transform.position, pos) < minDist)
            {
                return false;
            }
        }

        foreach(GameObject rock in rocks)
        {
            if (Vector3.Distance(rock.transform.position, pos) < minDist)
            {
                return false;
            }
        }

        foreach(GameObject m in medicine)
        {
            if (Vector3.Distance(m.transform.position, pos) < minDist)
            {
                return false;
            }
        }

        foreach(GameObject f in food)
        {
            if (Vector3.Distance(f.transform.position, pos) < minDist)
            {
                return false;
            }
        }

        foreach(GameObject c in critters)
        {
            if(Vector3.Distance(c.transform.position, pos) < minDist)
            {
                return false;
            }
        }

        return true;
    }
}
