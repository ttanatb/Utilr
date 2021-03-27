using System.Collections.Generic;

namespace Utilr
{
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
