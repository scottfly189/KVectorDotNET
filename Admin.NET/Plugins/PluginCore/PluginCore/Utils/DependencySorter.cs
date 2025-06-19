// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！


using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace PluginCore.Utils
{
    /// <remarks>
    /// https://stackoverflow.com/questions/4106862/how-to-sort-depended-objects-by-dependency/
    /// 
    /// Definition: http://en.wikipedia.org/wiki/Topological_sorting
    /// Source code credited to: http://tawani.blogspot.com/2009/02/topological-sorting-and-cyclic.html    
    /// Original Java source code: http://www.java2s.com/Code/Java/Collections-Data-Structure/Topologicalsorting.htm
    /// </remarks>
    /// <author>ThangTran</author>
    /// <history>
    /// 2012.03.21 - ThangTran: rewritten based on <see cref="TopologicalSorter"/>.
    /// </history>
    public class DependencySorter<T>
    {
        //**************************************************
        //
        // Private members
        //
        //**************************************************

        #region Private members

        /// <summary>
        /// Gets the dependency matrix used by this instance.
        /// </summary>
        private readonly Dictionary<T, Dictionary<T, object>> _matrix = new Dictionary<T, Dictionary<T, object>>();

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Adds a list of objects that will be sorted.
        /// </summary>
        public void AddObjects(params T[] objects)
        {
            // --- Begin parameters checking code -----------------------------
            // Debug.Assert(objects != null);
            // Debug.Assert(objects.Length > 0);
            // --- End parameters checking code -------------------------------

            // add to matrix
            foreach (T obj in objects)
            {
                // add to dictionary
                _matrix.Add(obj, new Dictionary<T, object>());
            }
        }

        /// <summary>
        /// Sets dependencies of given object.
        /// This means <paramref name="obj"/> depends on these <paramref name="dependsOnObjects"/> to run.
        /// Please make sure objects given in the <paramref name="obj"/> and <paramref name="dependsOnObjects"/> are added first.
        /// </summary>
        public void SetDependencies(T obj, params T[] dependsOnObjects)
        {
            // --- Begin parameters checking code -----------------------------
            // Debug.Assert(dependsOnObjects != null);
            // --- End parameters checking code -------------------------------

            // set dependencies
            Dictionary<T, object> dependencies = _matrix[obj];
            dependencies.Clear();

            // for each depended objects, add to dependencies
            foreach (T dependsOnObject in dependsOnObjects)
            {
                dependencies.Add(dependsOnObject, null);
            }
        }

        /// <summary>
        /// Sorts objects based on this dependencies.
        /// Note: because of the nature of algorithm and memory usage efficiency, this method can be used only one time.
        /// </summary>
        public T[] Sort()
        {
            // prepare result
            List<T> result = new List<T>(_matrix.Count);

            // while there are still object to get
            while (_matrix.Count > 0)
            {
                // get an independent object
                T independentObject;
                if (!this.GetIndependentObject(out independentObject))
                {
                    // circular dependency found
                    throw new CircularReferenceException();
                }

                // add to result
                result.Add(independentObject);

                // delete processed object
                this.DeleteObject(independentObject);
            }

            // return result
            return result.ToArray();
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Returns independent object or returns NULL if no independent object is found.
        /// </summary>
        private bool GetIndependentObject(out T result)
        {
            // for each object
            foreach (KeyValuePair<T, Dictionary<T, object>> pair in _matrix)
            {
                // if the object contains any dependency
                if (pair.Value.Count > 0)
                {
                    // has dependency, skip it
                    continue;
                }

                // found
                result = pair.Key;
                return true;
            }

            // not found
            result = default(T);
            return false;
        }

        /// <summary>
        /// Deletes given object from the matrix.
        /// </summary>
        private void DeleteObject(T obj)
        {
            // delete object from matrix
            _matrix.Remove(obj);

            // for each object, remove the dependency reference
            foreach (KeyValuePair<T, Dictionary<T, object>> pair in _matrix)
            {
                // if current object depends on deleting object
                pair.Value.Remove(obj);
            }
        }


        #endregion
    }

    /// <summary>
    /// Represents a circular reference exception when sorting dependency objects.
    /// </summary>
    public class CircularReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularReferenceException"/> class.
        /// </summary>
        public CircularReferenceException()
            : base("Circular reference found.")
        {            
        }
    }
}

