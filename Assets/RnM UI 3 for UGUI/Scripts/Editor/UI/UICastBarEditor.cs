﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

namespace UnityEditor.UI
{
	[CanEditMultipleObjects, CustomEditor(typeof(UICastBar), true)]
	public class UICastBarEditor : Editor {
		
		private SerializedProperty m_Fill;
		private SerializedProperty m_TitleLabel;
		private SerializedProperty m_TimeLabel;
		private SerializedProperty m_DisplayTime;
		private SerializedProperty m_TimeFormat;
		private SerializedProperty m_UseSpellIcon;
		private SerializedProperty m_IconFrame;
		private SerializedProperty m_IconImage;
		private SerializedProperty m_NormalColors;
		private SerializedProperty m_OnInterruptColors;
		private SerializedProperty m_InTransition;
		private SerializedProperty m_InTransitionDuration;
		private SerializedProperty m_OutTransition;
		private SerializedProperty m_OutTransitionDuration;
		private SerializedProperty m_HideDelay;
		
		protected void OnEnable()
		{
			this.m_Fill = base.serializedObject.FindProperty("m_Fill");
			this.m_TitleLabel = base.serializedObject.FindProperty("m_TitleLabel");
			this.m_TimeLabel = base.serializedObject.FindProperty("m_TimeLabel");
			this.m_DisplayTime = base.serializedObject.FindProperty("m_DisplayTime");
			this.m_TimeFormat = base.serializedObject.FindProperty("m_TimeFormat");
			this.m_UseSpellIcon = base.serializedObject.FindProperty("m_UseSpellIcon");
			this.m_IconFrame = base.serializedObject.FindProperty("m_IconFrame");
			this.m_IconImage = base.serializedObject.FindProperty("m_IconImage");
			this.m_NormalColors = base.serializedObject.FindProperty("m_NormalColors");
			this.m_OnInterruptColors = base.serializedObject.FindProperty("m_OnInterruptColors");
			this.m_InTransition = base.serializedObject.FindProperty("m_InTransition");
			this.m_InTransitionDuration = base.serializedObject.FindProperty("m_InTransitionDuration");
			this.m_OutTransition = base.serializedObject.FindProperty("m_OutTransition");
			this.m_OutTransitionDuration = base.serializedObject.FindProperty("m_OutTransitionDuration");
			this.m_HideDelay = base.serializedObject.FindProperty("m_HideDelay");
		}
		
		public override void OnInspectorGUI()
		{
			base.serializedObject.Update();
			
			EditorGUILayout.Separator();
			
			EditorGUILayout.PropertyField(this.m_Fill, new GUIContent("Fill Image"));
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Spell Title", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_TitleLabel, new GUIContent("Text"));
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Time Text", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_TimeLabel, new GUIContent("Text"));
			EditorGUILayout.PropertyField(this.m_DisplayTime, new GUIContent("Display"));
			EditorGUILayout.PropertyField(this.m_TimeFormat, new GUIContent("Format"));
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Spell Icon", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_UseSpellIcon, new GUIContent("Use Icon"));
			if (this.m_UseSpellIcon.boolValue)
			{
				EditorGUILayout.PropertyField(this.m_IconFrame, new GUIContent("Frame"));
				EditorGUILayout.PropertyField(this.m_IconImage, new GUIContent("Image"));
			}
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_NormalColors, new GUIContent("Normal"), true);
			EditorGUILayout.PropertyField(this.m_OnInterruptColors, new GUIContent("On Interrupt"), true);
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Show Transition", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_InTransition, new GUIContent("Transition"));
			if (this.m_InTransition.enumValueIndex == 1)
				EditorGUILayout.PropertyField(this.m_InTransitionDuration, new GUIContent("Duration"));
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Hide Transition", EditorStyles.boldLabel);
			EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);
			EditorGUILayout.PropertyField(this.m_OutTransition, new GUIContent("Transition"));
			if (this.m_OutTransition.enumValueIndex == 1)
				EditorGUILayout.PropertyField(this.m_OutTransitionDuration, new GUIContent("Duration"));
			EditorGUILayout.PropertyField(this.m_HideDelay, new GUIContent("Hide Delay"));
			EditorGUI.indentLevel = (EditorGUI.indentLevel - 1);
			
			EditorGUILayout.Separator();
			
			if (GUILayout.Button("Test Interrupt", EditorStyles.miniButton))
			{
				(base.target as UICastBar).Interrupt();
			}
			
			EditorGUILayout.Separator();
			
			base.serializedObject.ApplyModifiedProperties();
		}
	}
}