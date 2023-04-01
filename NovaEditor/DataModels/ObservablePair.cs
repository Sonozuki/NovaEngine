using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NovaEditor.DataModels;

/// <summary>Represents a pair of objects that provides notifications when an item is added or removed.</summary>
internal sealed class ObservablePair<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
    where T : class
{
    /*********
    ** Events
    *********/
    /// <inheritdoc/>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler CollectionChanged;


    /*********
    ** Fields
    *********/
    /// <summary>The first element in the pair.</summary>
    private T _First;

    /// <summary>The second element in the pair.</summary>
    private T _Second;


    /*********
    ** Properties
    *********/
    /// <summary>The first element in the pair.</summary>
    public T First
    {
        get => _First;
        set
        {
            _First = value;
            OnPropertyChanged(nameof(First));
        }
    }

    /// <summary>The second element in the pair.</summary>
    public T Second
    {
        get => _Second;
        set
        {
            _Second = value;
            OnPropertyChanged(nameof(Second));
        }
    }

    /// <summary>Whether the pair is full.</summary>
    public bool IsFull => First != null && Second != null;


    /*********
    ** Indexers
    *********/
    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is less than 0 or more than 1.</exception>
    public T this[int index]
    {
        get
        {
            if (index == 0)
                return First;
            else if (index == 1)
                return Second;
            else
                throw new ArgumentOutOfRangeException(nameof(index), $"Must be between 0 => 1 (inclusive)");
        }
        set
        {
            T oldValue;

            if (index == 0)
            {
                oldValue = First;
                First = value;
            }
            else if (index == 1)
            {
                oldValue = Second;
                Second = value;
            }
            else
                throw new ArgumentOutOfRangeException(nameof(index), $"Must be between 0 => 1 (inclusive)");

            if (oldValue == null)
                OnCollectionChanged(NotifyCollectionChangedAction.Add, value);
            else if (value == null)
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, oldValue);
            else
                OnCollectionChanged(value, oldValue);
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Adds an item to the pair.</summary>
    /// <param name="item">The item to add.</param>
    /// <exception cref="InvalidOperationException">Thrown if the pair is full.</exception>
    public void Add(T item)
    {
        if (IsFull)
            throw new InvalidOperationException("Pair is full, cannot add another item.");

        OnCollectionChanged(NotifyCollectionChangedAction.Add, item);

        if (First == null)
            First = item;
        else
            Second = item;
    }

    /// <summary>Removes a specific item from the pair.</summary>
    /// <param name="item">The item to remove.</param>
    /// <returns><see langword="true"/>, if the item was successfully removed; otherwise, <see langword="false"/>.</returns>
    public bool Remove(T item)
    {
        if (First == item)
            First = null;
        else if (Second == item)
            Second = null;
        else
            return false;

        OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
        return true;
    }

    /// <summary>Removes an item at a specific index from the pair.</summary>
    /// <param name="index">The index of the item to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is less than 0 or more than 1.</exception>
    public void RemoveAt(int index) => this[index] = null;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        if (First != null)
            yield return First;

        if (Second != null)
            yield return Second;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    /*********
    ** Private Methods
    *********/
    /// <summary>Invokes the <see cref="PropertyChanged"/> event.</summary>
    /// <param name="propertyName">The name of the property to pass to the event invocation.</param>
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    /// <summary>Invokes the <see cref="CollectionChanged"/> event.</summary>
    /// <param name="action">The action to pass to the event invocation.</param>
    /// <param name="changedItem">The changed item to pass to the event invocation.</param>
    private void OnCollectionChanged(NotifyCollectionChangedAction action, T changedItem)
    {
        CollectionChanged?.Invoke(this, new(action, changedItem));
    }

    /// <summary>Invokes the <see cref="CollectionChanged"/> event.</summary>
    /// <param name="newItem">The new item to pass to the event invocation.</param>
    /// <param name="oldItem">The old item to pass to the event invocation.</param>
    private void OnCollectionChanged(T newItem, T oldItem)
    {
        CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Replace, newItem, oldItem));
    }
}
