using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEditor;

[CustomEditor(typeof(FFMPEGImagesToVideo))]
public class FFMPEGImagesToVideoEditor : Editor
{

	SerializedProperty workingDirectory;
	SerializedProperty inputName;
	SerializedProperty outputName;
	SerializedProperty overwrite;
	SerializedProperty startingFrameNumber;
	SerializedProperty framerate;
	SerializedProperty compression;
	SerializedProperty convertOnQuit;

	private void OnEnable() {

		workingDirectory = serializedObject.FindProperty("workingDirectory");
		inputName = serializedObject.FindProperty("inputName");
		outputName = serializedObject.FindProperty("outputName");
		overwrite = serializedObject.FindProperty("overwrite");
		startingFrameNumber = serializedObject.FindProperty("startingFrameNumber");
		framerate = serializedObject.FindProperty("framerate");
		compression = serializedObject.FindProperty("compression");
		convertOnQuit = serializedObject.FindProperty("convertOnQuit");
	}

	public override void OnInspectorGUI() {

		FFMPEGImagesToVideo script = target as FFMPEGImagesToVideo;

		serializedObject.Update();

		EditorGUILayout.BeginVertical("HelpBox");
		EditorGUILayout.LabelField("Directory Settings", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(workingDirectory);
		if (GUILayout.Button("Browse")) { workingDirectory.stringValue = EditorUtility.OpenFolderPanel("Images Folder", "", ""); }
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(inputName);
		EditorGUILayout.PropertyField(outputName);

		if(!overwrite.boolValue && File.Exists(workingDirectory.stringValue+"/"+outputName.stringValue))
			EditorGUILayout.HelpBox("A file with that name already exists.", MessageType.Warning);

		EditorGUILayout.PropertyField(overwrite);

		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical("HelpBox");

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Conversion Settings", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField(startingFrameNumber);
		EditorGUILayout.PropertyField(framerate);
		EditorGUILayout.IntSlider(compression, 0, 51);

		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(convertOnQuit);
		if (GUILayout.Button("Convert Now")) { script.FFMPEGConvertImagesToVideo(); }

		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();

	}

}
