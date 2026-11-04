using CardboardXR.Demos;
using CardboardXR.Demos.LionCinema;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBootstrap
{
    public static class LionCinemaBuilder
    {
        const string Folder="Assets/_Project/Demos/LionCinema";
        const string ScenePath=Folder+"/LionCinema.unity";
        public static void Ensure()
        {
            if(!AssetDatabase.IsValidFolder(Folder)) AssetDatabase.CreateFolder("Assets/_Project/Demos","LionCinema");
            var catalog=AssetDatabase.LoadAssetAtPath<DemoCatalog>("Assets/_Project/Demos/DemoCatalog.asset");
            if(catalog==null)return;
            if(catalog.IndexOf("lion-cinema")<0){var so=new SerializedObject(catalog);var list=so.FindProperty("m_Demos");list.arraySize++;var e=list.GetArrayElementAtIndex(list.arraySize-1);e.FindPropertyRelative("Id").stringValue="lion-cinema";e.FindPropertyRelative("Title").stringValue="LION CINEMA VR";e.FindPropertyRelative("Description").stringValue="A seated IMAX-style virtual cinema prototype. YouTube playback integration is being developed.";e.FindPropertyRelative("Tagline").stringValue="Your virtual IMAX auditorium";e.FindPropertyRelative("Icon").stringValue="movie";e.FindPropertyRelative("Tint").colorValue=new Color(.48f,.08f,.12f);e.FindPropertyRelative("ScenePath").stringValue=ScenePath;var req=e.FindPropertyRelative("Requirements");req.FindPropertyRelative("SixDof").enumValueIndex=(int)Need.Off;req.FindPropertyRelative("Passthrough").enumValueIndex=(int)Need.Off;req.FindPropertyRelative("Hands").enumValueIndex=(int)Need.Off;req.FindPropertyRelative("ScanSurfaces").boolValue=false;req.FindPropertyRelative("KeepPlacedObjects").boolValue=false;so.ApplyModifiedPropertiesWithoutUndo();EditorUtility.SetDirty(catalog);}
            if(AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath)==null){var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);new GameObject("LION CINEMA Demo").AddComponent<LionCinemaDemo>();EditorSceneManager.SaveScene(scene,ScenePath);}
            EditorBuildSettingsScene[] old=EditorBuildSettings.scenes; if(System.Array.Exists(old,x=>x.path==ScenePath)==false){var arr=new EditorBuildSettingsScene[old.Length+1];old.CopyTo(arr,0);arr[old.Length]=new EditorBuildSettingsScene(ScenePath,true);EditorBuildSettings.scenes=arr;}
            AssetDatabase.SaveAssets();AssetDatabase.Refresh();Debug.Log("[LION CINEMA] Demo and scene prepared.");
        }
    }
}
