using UnityEngine.UI;
using UnityEngine;
using Lean.Touch;
using TMPro;

public class GameControls : MonoBehaviour
{

    int selectedUnit = 0;
    float[] timers;
    int[] availableUnits;

    public Camera cam;

    public float[] unitsCooldowns;
    public GameObject[] unitsPrefabs;

    public TextMeshProUGUI[] countersUI;
    public TextMeshProUGUI[] timersUI;
    public Image[] unitsUI;
    public Sprite normalUI;
    public Sprite highlightedUI;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (GameManager.GameIsPaused) return;

        for (int i = 0; i < timers.Length; i++)
        {
            timersUI[i].SetText(((int) timers[i]).ToString());
            if (timers[i] <= 0)
            {
                AddUnit(i);
                timers[i] = unitsCooldowns[i];
            }
            timers[i] -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                SpawnUnit(hit.point);
            }
        }
    }

    void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleTap;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerTap -= HandleTap;
    }


    void HandleTap(LeanFinger finger)
    {
        if (finger.StartedOverGui) return;

        Ray ray = cam.ScreenPointToRay(finger.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SpawnUnit(hit.point);
            if (hit.transform.tag == "Ground")
            {
            }
        }
    }

    void AddUnit(int i)
    {
        availableUnits[i]++;
        countersUI[i].SetText(availableUnits[i].ToString());
    }

    void SpawnUnit(Vector3 point)
    {
        if (availableUnits[selectedUnit] > 0)
        {
            availableUnits[selectedUnit]--;
            countersUI[selectedUnit].SetText(availableUnits[selectedUnit].ToString());
            GameObject prefab = unitsPrefabs[selectedUnit];
            Instantiate(prefab, point, prefab.transform.rotation);
        }
    }

    public void Reset()
    {
        timers = new float[unitsCooldowns.Length];
        availableUnits = new int[unitsCooldowns.Length];
        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] = unitsCooldowns[i];
            timersUI[i].SetText(((int)timers[i]).ToString());

            availableUnits[i] = 0;
            countersUI[i].SetText(availableUnits[i].ToString());
        }
    }

    public void SelectUnit(int i)
    {
        unitsUI[selectedUnit].sprite = normalUI;
        selectedUnit = i;
        unitsUI[selectedUnit].sprite = highlightedUI;
    }
}
