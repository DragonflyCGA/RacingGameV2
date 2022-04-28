using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RacetrackCurveAnglesAttribute))]
public class RacetrackCurveAnglesPropertyDrawer : ButtonSliderPropertyDrawerBase
{
    private PresetValueButton[] XAngleButtons;
    private PresetValueButton[] YAngleButtons;
    private PresetValueButton[] ZAngleButtons;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LoadAssets();

        if (property.propertyType != SerializedPropertyType.Vector3)
        {
            EditorGUI.LabelField(position, label.text, "[RacetrackCurveAngles] must be applied to a Vector3");
            return;
        }

        var angles = property.vector3Value;

        float lineHeight = base.GetPropertyHeight(property, label);
        position.height = lineHeight;
        position.y += LineSpacing;

        bool rebuildCurve = false;

        EditorGUI.LabelField(position, "Turn (Y)");
        float newY = DrawAngleButtons(position, angles.y, YAngleButtons);
        if (angles.y != newY)
        {
            angles.y = newY;
            rebuildCurve = true;
        }
        position.y += ButtonHeight;
        angles.y = EditorGUI.Slider(position, new GUIContent(" "), angles.y, -180.0f, 180.0f);
        position.y += lineHeight + LineSpacing;

        EditorGUI.LabelField(position, "Gradient (X)");
        float newX = DrawAngleButtons(position, angles.x, XAngleButtons);
        if (angles.x != newX)
        {
            angles.x = newX;
            rebuildCurve = true;
        }
        position.y += ButtonHeight;
        angles.x = EditorGUI.Slider(position, new GUIContent(" "), angles.x, -180.0f, 180.0f);
        position.y += lineHeight + LineSpacing;

        EditorGUI.LabelField(position, "Bank (Z)");
        float newZ = DrawAngleButtons(position, angles.z, ZAngleButtons);
        if (angles.z != newZ)
        {
            angles.z = newZ;
            rebuildCurve = true;
        }
        position.y += ButtonHeight;
        angles.z = -EditorGUI.Slider(position, new GUIContent(" "), -angles.z, -90.0f, 90.0f);

        property.vector3Value = angles;

        if (rebuildCurve)
        {
            // Find racetrack
            Racetrack track = null;
            if (property.serializedObject.targetObject is RacetrackCurve)
            {
                var curve = (RacetrackCurve)property.serializedObject.targetObject;
                track = curve.Track;
            }
            else if (property.serializedObject.targetObject is Racetrack)
            {
                track = (Racetrack)property.serializedObject.targetObject;
            }

            // Flag track as needing update
            if (track != null)
                track.IsUpdateRequired = true;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int rowCount = 3; // property.serializedObject.targetObject is RacetrackCurve ? 3 : 2;
        return (base.GetPropertyHeight(property, label) + ButtonHeight + LineSpacing) * rowCount;
    }

    protected override void InternalLoadAssets()
    {
        YAngleButtons = new[] {
            new PresetValueButton("YAngN90", -90.0f),
            new PresetValueButton("YAngN45", -45.0f),
            new PresetValueButton("YAngN30", -30.0f),
            new PresetValueButton("YAng0", 0.0f),
            new PresetValueButton("YAng30", 30.0f),
            new PresetValueButton("YAng45", 45.0f),
            new PresetValueButton("YAng90", 90.0f)
        };

        XAngleButtons = new[]
        {
            new PresetValueButton("Grad45", -45.0f),
            new PresetValueButton("Grad30", -30.0f),
            new PresetValueButton("Grad15", -15.0f),
            new PresetValueButton("Grad0", 0.0f),
            new PresetValueButton("GradN15", 15.0f),
            new PresetValueButton("GradN30", 30.0f),
            new PresetValueButton("GradN45", 45.0f)
        };

        ZAngleButtons = new[]
        {
            new PresetValueButton("Grad45", 45.0f),
            new PresetValueButton("Grad30", 30.0f),
            new PresetValueButton("Grad15", 15.0f),
            new PresetValueButton("Grad0", 0.0f),
            new PresetValueButton("GradN15", -15.0f),
            new PresetValueButton("GradN30", -30.0f),
            new PresetValueButton("GradN45", -45.0f)
        };
    }
}
