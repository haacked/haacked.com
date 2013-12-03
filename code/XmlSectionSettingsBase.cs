using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Reflection;

using log4net;

namespace GU.Configuration
{
	/// <summary>
	/// This class serves as the base class for any class that is used to 
	/// house configuration settings using the XmlSerializationSectionHandler 
	/// written by Craig Andera (http://staff.develop.com/candera/weblog2/articleview.aspx/CLR%20Workings/The%20Last%20Configuration%20Section%20Handler%20I.xml) 
	/// and modified here.
	/// By inheriting this class, your class will gain the ability to reload itself 
	/// when the configuration file changes.
	/// </summary>
	public abstract class XmlSectionSettingsBase
	{
		private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		FileSystemWatcher _watcher;
		bool _isDataValid = true;
		string _rootName;

		/// <summary>
		/// Called by the XmlSerializationSectionHandler to deserialize a 
		/// settings object for the first time. If that object inherits from 
		/// XmlSectionSettingsBase, then we make it watch for changes to the 
		/// config file.
		/// </summary>
		/// <param name="section"></param>
		/// <returns></returns>
		public static object LoadSettings(XmlNode section)
		{
			object settings = DeserializeSection(section);

			XmlSectionSettingsBase xmlSettings = settings as XmlSectionSettingsBase;
			if(xmlSettings != null)
			{
				xmlSettings._rootName = section.Name;
				((XmlSectionSettingsBase)settings).WatchForConfigChanges();
			}
			return settings;
		}

		/// <summary>
		/// Method that checks for changes to the config file. If changes are 
		/// found, the settings are reloaded.  This should be called before 
		/// access to each public property.
		/// </summary>
		protected void UpdateChanges()
		{
			if(!_isDataValid)
				ReloadSettings();
		}

		/// <summary>
		/// Returns an object deserialized from an Xml Configuration Section.
		/// </summary>
		/// <param name="section">The configuration section containing the settings.</param>
		/// <param name="settingsTarget">The existing settings object to copy the new settings to.</param>
		/// <returns></returns>
		static object DeserializeSection(XmlNode section)
		{
			XPathNavigator navigator = section.CreateNavigator(); 

			string typename = (string)navigator.Evaluate("string(@type)");

			Type type = Type.GetType(typename);
			if(type == null)
				throw new ConfigurationException("The type '" + typename + "' is not a valid type. Double check the type parameter.");
			XmlSerializer serializer = new XmlSerializer(type); 

			return serializer.Deserialize(new XmlNodeReader(section));
		}

		/// <summary>
		/// Sets up a file system watcher to watch for changes to the config file.
		/// </summary>
		void WatchForConfigChanges()
		{
			FileInfo configFile = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			try
			{
				_watcher = new FileSystemWatcher(configFile.DirectoryName);
				_watcher.Filter = configFile.Name;
				_watcher.NotifyFilter = NotifyFilters.LastWrite;
				_watcher.Changed += new FileSystemEventHandler(OnConfigChanged);
				_watcher.EnableRaisingEvents = true;
			}
			catch(Exception ex)
			{
				Log.Error("Configuration problem.", ex);
				throw new ConfigurationException("An error occurred while attempting to watch for file system changes.", ex);
			}
		}

		/// <summary>
		/// Merely marks the fact that the configuration file has changed.
		/// </summary>
		void OnConfigChanged(object sender, FileSystemEventArgs e)
		{
			_isDataValid = false;
		}

		/// <summary>
		/// Reloads setting values from the configuration file.
		/// </summary>
		void ReloadSettings()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			XmlNodeList nodes = doc.GetElementsByTagName(_rootName);
			if(nodes.Count > 0)
			{
				//Note: newSettings should not watch for config changes.
				XmlSectionSettingsBase newSettings = DeserializeSection(nodes[0]) as XmlSectionSettingsBase;
				newSettings._isDataValid = true;
				CopySettings(newSettings);
			}
			else
				throw new System.Configuration.ConfigurationException("Configuration section " + _rootName + " not found.");
		}

		/// <summary>
		/// Updates the settings of this instance with the values 
		/// of the new settings by copying public property values.
		/// </summary>
		/// <param name="newSettings"></param>
		void CopySettings(object newSettings)
		{
			if(newSettings.GetType() != this.GetType())
				return;

			PropertyInfo[] properties = newSettings.GetType().GetProperties();

			foreach(PropertyInfo property in properties)
			{
				if(property.CanWrite && property.CanRead)
				{
					property.SetValue(this, property.GetValue(newSettings, null), null);
				}
			}
		}

	}
}
