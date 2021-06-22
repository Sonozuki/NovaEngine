using NovaEngine.Serialisation;
using System.Collections;
using System.Collections.Generic;

namespace NovaEngine.Core
{
    /// <summary>Represents a collection of child <see cref="GameObject"/>s for use in <see cref="GameObject.Children"/>.</summary>
    /// <remarks>This automatically sets/unsets the parent of children where necessary.</remarks>
    public class ChildrenCollection : IList<GameObject>
    {
        /*********
        ** Fields
        *********/
        /// <summary>The game object whose children this collection contains.</summary>
        /// <remarks>This is used for automatically setting/unsetting the parent of children.</remarks>
        [Serialisable]
        private readonly GameObject Parent;

        /// <summary>The underlying collection of children.</summary>
        private readonly List<GameObject> Children = new();


        /*********
        ** Accessors
        *********/
        /// <inheritdoc/>
        public int Count => Children.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public GameObject this[int index]
        {
            get => Children[index];
            set
            {
                value.Parent = Parent;
                Children[index] = value;
            }
        }

        /// <inheritdoc/>
        public void Add(GameObject item)
        {
            Children.Add(item);
            item.Parent = Parent;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            foreach (var child in Children)
                child.Parent = null;

            Children.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(GameObject item) => Children.Contains(item);
        
        /// <inheritdoc/>
        public void CopyTo(GameObject[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        
        /// <inheritdoc/>
        public IEnumerator<GameObject> GetEnumerator() => Children.GetEnumerator();
        
        /// <inheritdoc/>
        public int IndexOf(GameObject item) => Children.IndexOf(item);
        
        /// <inheritdoc/>
        public void Insert(int index, GameObject item) => Children.Insert(index, item);

        /// <inheritdoc/>
        public bool Remove(GameObject item)
        {
            var wasRemoved = Children.Remove(item);
            if (wasRemoved)
                item.Parent = null;

            return wasRemoved;
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            Children[index].Parent = null;
            Children.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();


        /*********
        ** Internal Methods
        *********/
        /// <summary>Constructs an instance.</summary>
        /// <param name="parent">The game object whose children this collection is.</param>
        internal ChildrenCollection(GameObject parent)
        {
            Parent = parent;
        }


        /*********
        ** Private Methods
        *********/
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>Constructs an instance.</summary>
        private ChildrenCollection() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
