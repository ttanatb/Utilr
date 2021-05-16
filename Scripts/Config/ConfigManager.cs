using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Utilr;

namespace Configr
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        #region Constants
        const string kSaveFileName = "playerconfig.dat";
        #endregion // Constants

        #region Serialized Fields
        [SerializeField]
        private PlayerConfig m_playerConfig = null;
        #endregion // Serialized Fields

        #region Getters & Setters
        public PlayerConfig PlayerConfig
        {
            get { return m_playerConfig; }
            private set { m_playerConfig = value; }
        }
        #endregion // Getters & Setters

        #region Unity Messages
        private void Awake()
        {
            if (PlayerConfig == null)
            {
                PlayerConfig = new PlayerConfig();
            }
        }

        private void OnEnable()
        {
            ReadSaveFile(kSaveFileName, out PlayerConfig temp);
            if (temp != null)
                m_playerConfig.Copy(temp);
        }

        private void OnDisable()
        {
            WriteSaveFile(kSaveFileName, PlayerConfig);
        }
        #endregion // Unity Messages

        #region Private Methods
        private void WriteSaveFile(string fileName, System.Object obj)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;
            try
            {
                stream = File.Exists(path) ? File.OpenWrite(path) : File.Create(path);
                formatter.Serialize(stream, obj);
            }
            catch (System.Exception e)
            {
                Debug.LogErrorFormat("Failed to write save file at {0}: {1}", path, e.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        private void ReadSaveFile<T>(string fileName, out T obj)
        {
            obj = default;
            string path = Path.Combine(Application.persistentDataPath, fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(path))
                return;

            FileStream stream = null;
            try
            {
                stream = File.OpenRead(path);
                obj = (T)formatter.Deserialize(stream);
            }
            catch (System.Exception e)
            {
                Debug.LogErrorFormat("Failed to read save file at {0}: {1}", path, e.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        private void DeleteSaveFile(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(path))
                return;

            try
            {
                File.Delete(path);
            }
            catch (System.Exception e)
            {
                Debug.LogErrorFormat("Failed to delete save file at {0}: {1}", path, e.Message);
            }
        }
        #endregion // Private Methods
    }
}
