using AirplaneController;
using UnityEditor;
using UnityEngine; 

public static class IP_Airplane_Menus
{
    [MenuItem("Airplane Tools/Create new airplane")]
    public static void CreateAirplane()
    {
        GameObject current = Selection.activeGameObject;
        if (current)
        {
            var currController = current.AddComponent<IP_AirplaneController>();
            GameObject COG = new GameObject("COG");
            COG.transform.SetParent(current.transform);

            currController.COG = COG.transform;
        }
    }
}