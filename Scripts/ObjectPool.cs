using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utilr
{
    /// <summary>
    /// Pool of Unity GameObjects, initializes a bunch of them in a disabled state.
    /// </summary>
    /// <typeparam name="T">Type can either be a MonoBehaviour or GameObject</typeparam>
    public class ObjectPool<T>
    {
        private List<T> m_pool;
        private Dictionary<int, bool> m_avail;
        private int m_nextAvailIndex = 0;

        public ObjectPool(IEnumerable<T> list)
        {
            m_pool = new List<T>(list);
            m_avail = new Dictionary<int, bool>(m_pool.Count);
            for (int i = 0; i < m_pool.Count; i++)
            {
                m_avail.Add(i, true);
            }
        }

        /// <summary>
        /// Type of T should be a subclass of GameObject.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="count"></param>
        /// <param name="parent"></param>
        public ObjectPool(GameObject prefab, int count, Transform parent = null)
        {
            Assert.IsTrue(typeof(T) == typeof(GameObject) 
                || (typeof(T).IsSubclassOf(typeof(Behaviour)) && prefab.TryGetComponent(out T _)),
                "Type T must either be GameObject or a subclass of MonoBehaviour. If it's a MonoBehaviour, the" +
                " prefab must have that script as a component.");

            m_pool = new List<T>(count);
            m_avail = new Dictionary<int, bool>(count);
            for (int i = 0; i < count; i++)
            {
                var gameObj = GameObject.Instantiate(prefab, parent);
                gameObj.name += $" ({i})";
                
                if (typeof(T) == typeof(GameObject))
                    m_pool.Add((T)(object)gameObj);
                else m_pool.Add(gameObj.GetComponent<T>());
                m_avail.Add(i, true);
            }
        }

        public (T Obj, int Index) GetNextAvailable()
        {
            int index = m_nextAvailIndex;
            var res = m_pool[index];
            m_avail[index] = false;

            m_nextAvailIndex = NextAvailIndex();
            return (Obj: res, Index: index);
        }

        public T Get(int index)
        {
            return m_pool[index];
        }

        public void SetAvailable(int index)
        {
            m_avail[index] = true;
        }

        public void Reset()
        {
            for (int i = 0; i < m_pool.Count; i++)
            {
                m_avail[i] = true;
            }
        }

        private int NextAvailIndex()
        {
            int currIndex = m_nextAvailIndex;
            for (int i = 0; i < m_pool.Count; i++)
            {
                currIndex++;
                if (currIndex >= m_pool.Count)
                    currIndex -= m_pool.Count;

                if (m_avail[currIndex])
                    return currIndex;
            }


            return -1;
        }

        public int Count { get { return m_pool.Count; } }
    }
}
