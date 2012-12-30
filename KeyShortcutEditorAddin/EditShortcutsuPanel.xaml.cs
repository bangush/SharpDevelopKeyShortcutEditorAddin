﻿/*
 * Created by SharpDevelop.
 * User: Noam
 * Date: 28/12/2012
 * Time: 14:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using ICSharpCode.SharpDevelop.Gui;

namespace KeyShortcutEditorAddin
{
	/// <summary>
	/// Interaction logic for EditShortcutsuPanel.xaml
	/// </summary>
	public partial class EditShortcutsuPanel : OptionPanel
	{
		private EditKeyShortcutInAddinFiles _shortcutsEditor;
		private XmlSerializer _serialzer;
		
		public EditShortcutsuPanel()
		{
			InitializeComponent();
			_serialzer = new XmlSerializer(typeof(List<KeyShortcut>));
			_shortcutsEditor = new EditKeyShortcutInAddinFiles();
		}
		
		public void Export(object sender, RoutedEventArgs e)
		{
			string path = "";
			using (var file = new FileStream(path,FileMode.CreateNew))
			{
				_serialzer.Serialize(file,_shortcutsEditor.KeyShortcuts);
			}
		}

		public void Import(object sender, RoutedEventArgs e)
		{
			string path = "";
			using (var file = new FileStream(path,FileMode.Open))
			{
				var shortcuts = _serialzer.Deserialize(file) as List<KeyShortcut>;
				foreach (var s in shortcuts) {
					_shortcutsEditor.ChangeKeyShortcut(s.Label,s.Shortcut);
				}
			}
		}
	}
}