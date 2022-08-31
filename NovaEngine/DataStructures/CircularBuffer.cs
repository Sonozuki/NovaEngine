namespace NovaEngine.DataStructures;

/// <summary>Represents a fixed-length circular buffer.</summary>
/// <typeparam name="T">THe type of objects the buffer can contain.</typeparam>
public class CircularBuffer<T> : IEnumerable<T?>
{
    /*********
    ** Fields
    *********/
    /// <summary>The internal buffer of the circular buffer.</summary>
    private readonly T?[] Buffer;

    /// <summary>The index of the first element in the buffer.</summary>
    private int Start;

    /// <summary>The index of the last element in the buffer.</summary>
    private int End;


    /*********
    ** Accessors
    *********/
    /// <summary>The current number of elements in the buffer.</summary>
    public int Size { get; private set; }

    /// <summary>The maximum capacity of the buffer.</summary>
    public int Capacity { get; }

    /// <summary>Whether the buffer is at full capacity.</summary>
    /// <remarks>If the buffer is at max capacity and an element is pushed, then an element will get removed.</remarks>
    public bool IsFull => Size == Capacity;

    /// <summary>Whether the buffer is completely empty.</summary>
    public bool IsEmpty => Size == 0;

    /// <summary>Gets or sets the value at a specified position.</summary>
    /// <param name="index">The position index.</param>
    /// <returns>The value at the specified position.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="index"/> is outside of <see cref="Size"/>.</exception>
    public T? this[int index]
    {
        get
        {
            if (IsEmpty)
                throw new IndexOutOfRangeException($"Unable to access index {index}, buffer is empty.");
            if (index >= Size)
                throw new IndexOutOfRangeException($"Unable to access index {index}, buffer only contains {Size} element(s).");

            return Buffer[ConvertToInternalIndex(index)];
        }
        set
        {
            if (IsEmpty)
                throw new IndexOutOfRangeException($"Unable to access index {index}, buffer is empty.");
            if (index >= Size)
                throw new IndexOutOfRangeException($"Unable to access index {index}, buffer only contains {Size} element(s).");

            Buffer[ConvertToInternalIndex(index)] = value;
        }
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="capacity">The maximum capacity of the buffer.</param>
    /// <param name="initialContents">The initial contents of the buffer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="capacity"/> is less than one.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="initialContents"/> contains more elements than is allowed by <paramref name="capacity"/>.</exception>
    public CircularBuffer(int capacity, IEnumerable<T?>? initialContents = null)
    {
        if (capacity < 1)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Must be at least one.");

        var initialContentsArray = initialContents?.ToArray() ?? Array.Empty<T>();
        if (initialContentsArray.Length > capacity)
            throw new ArgumentException($"Contains more items than is allowed in the buffer by {nameof(capacity)}", nameof(initialContents));

        Capacity = capacity;
        Buffer = new T[capacity];

        Array.Copy(initialContentsArray, Buffer, initialContentsArray.Length);
        Size = initialContentsArray.Length;

        Start = 0;
        End = Size == capacity ? 0 : Size;
    }

    /// <summary>Pushes an element to the front of the buffer.</summary>
    /// <param name="element">The element to add.</param>
    /// <remarks>If the buffer is full, the element at the end will be popped.</remarks>
    public void PushFront(T? element)
    {
        if (IsFull)
        {
            DecrementIndex(ref Start);
            End = Start;
            Buffer[Start] = element;
        }
        else
        {
            DecrementIndex(ref Start);
            Buffer[Start] = element;
            Size++;
        }
    }

    /// <summary>Pushes an element to the back of the buffer.</summary>
    /// <param name="element">The buffer to add.</param>
    /// <remarks>If the buffer is full, the element at the front will be popped.</remarks>
    public void PushBack(T? element)
    {
        if (IsFull)
        {
            Buffer[End] = element;
            IncrementIndex(ref End);
            Start = End;
        }
        else
        {
            Buffer[End] = element;
            IncrementIndex(ref End);
            Size++;
        }
    }

    /// <summary>Removes the element at the front of the buffer.</summary>
    /// <returns>The value that was popped.</returns>
    /// <remarks>This decrements <see cref="Size"/> by one.</remarks>
    public T? PopFront()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cannot remove an element from an empty buffer.");

        var poppedValue = Buffer[Start];
        Buffer[Start] = default;
        Size--;

        IncrementIndex(ref Start);

        return poppedValue;
    }

    /// <summary>Removes the element at the end of the buffer.</summary>
    /// <returns>The value that was popped.</returns>
    /// <remarks>This decrements <see cref="Size"/> by one.</remarks>
    public T? PopBack()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Cannot remove an element from an empty buffer.");

        DecrementIndex(ref End);

        var popperdValue = Buffer[End];
        Buffer[End] = default;
        Size--;

        return popperdValue;
    }

    /// <summary>Clears the contents of the buffer.</summary>
    public void Clear()
    {
        Start = 0;
        End = 0;
        Size = 0;
        Array.Clear(Buffer);
    }

    /// <summary>Copies the contents of the buffer to an array.</summary>
    /// <returns>A new array with the content of the buffer.</returns>
    public T?[] ToArray()
    {
        var array = new T?[Size];
        var arrayOffset = 0;

        foreach (var segment in GetArraySegments())
        {
            Array.Copy(segment.Array!, segment.Offset, array, arrayOffset, segment.Count);
            arrayOffset += segment.Count;
        }

        return array;
    }

    /// <inheritdoc/>
    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var segment in GetArraySegments())
            for (int i = 0; i < segment.Count; i++)
                yield return segment.Array![segment.Offset + i];
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    /*********
    ** Private Methods
    *********/
    /// <summary>Converts a index for the circular buffer to an index for <see cref="Buffer"/>.</summary>
    /// <param name="index">The index of the circular buffer.</param>
    /// <returns>The index for <see cref="Buffer"/>.</returns>
    private int ConvertToInternalIndex(int index) => Start + (index < (Capacity - Start) ? index : index - Capacity);

    /// <summary>Increments an index by one, wrapping the index if necessary.</summary>
    /// <param name="index">The index to increment.</param>
    private void IncrementIndex(ref int index)
    {
        if (++index == Capacity)
            index = 0;
    }

    /// <summary>Decrements an index by one, wrapping the index if necessary.</summary>
    /// <param name="index">The index to decrement.</param>
    private void DecrementIndex(ref int index)
    {
        if (--index < 0)
            index = Capacity - 1;
    }

    /// <summary>Gets the two array segments that can be used to convert the circular buffer to a linear buffer.</summary>
    /// <returns>The two array segments that represent the buffer.</returns>
    private ArraySegment<T?>[] GetArraySegments()
    {
        var arraySegments = new ArraySegment<T?>[2];

        if (IsEmpty)
        {
            arraySegments[0] = new(Array.Empty<T>());
            arraySegments[1] = new(Array.Empty<T>());
        }
        else if (Start < End)
        {
            arraySegments[0] = new(Buffer, Start, End - Start);
            arraySegments[1] = new(Buffer, End, 0);
        }
        else
        {
            arraySegments[0] = new(Buffer, Start, Buffer.Length - Start);
            arraySegments[1] = new(Buffer, 0, End);
        }

        return arraySegments;
    }
}
