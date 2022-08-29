namespace NovaEngine.Tests.DataStructures;

/// <summary>The <see cref="CircularBuffer{T}"/> tests.</summary>
[TestFixture]
internal class CircularBufferTests
{
    /*********
    ** Public Methods
    *********/
    /// <summary>Tests <see cref="CircularBuffer{T}.Size"/>.</summary>
    [Test]
    public void Size_BufferContainsLessElementsThanCapacity_ReturnsNumberOfElementsInBuffer()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.AreEqual(2, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.Size"/>.</summary>
    [Test]
    public void Size_BufferIsFull_ReturnsTheCapacity()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsFull"/>.</summary>
    [Test]
    public void IsFull_BufferIsFull_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        Assert.IsTrue(buffer.IsFull);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsFull"/>.</summary>
    [Test]
    public void IsFull_BufferIsNotFull_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.IsFalse(buffer.IsFull);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsFull"/>.</summary>
    [Test]
    public void IsFull_BufferWasFullButAnElementWasPopped_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PopFront();
        Assert.IsFalse(buffer.IsFull);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsEmpty"/>.</summary>
    [Test]
    public void IsEmpty_BufferIsEmpty_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.IsTrue(buffer.IsEmpty);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsEmpty"/>.</summary>
    [Test]
    public void IsEmpty_BufferIsNotEmpty_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.IsFalse(buffer.IsEmpty);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.IsEmpty"/>.</summary>
    [Test]
    public void IsEmpty_BufferContainedAnElementThatWasPopped_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        buffer.PopFront();
        Assert.IsTrue(buffer.IsEmpty);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntGet_BufferIsEmpty_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.Throws<IndexOutOfRangeException>(new(() => _ = buffer[0]));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntGet_IndexBetweenZeroAndSize_ReturnsElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.AreEqual(1, buffer[0]);
        Assert.AreEqual(2, buffer[1]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntGet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.Throws<IndexOutOfRangeException>(new(() => _ = buffer[-1]));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntGet_IndexMoreThanSize_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.Throws<IndexOutOfRangeException>(new(() => _ = buffer[1]));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntSet_BufferIsEmpty_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.Throws<IndexOutOfRangeException>(new(() => buffer[0] = 0));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntSet_IndexBetweenZeroAndSize_SetsElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer[0] = 10;
        buffer[1] = 11;
        Assert.AreEqual(10, buffer[0]);
        Assert.AreEqual(11, buffer[1]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntSet_IndexLessThanZero_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.Throws<IndexOutOfRangeException>(new(() => buffer[-1] = 0));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}"/>[<see langword="int"/>].</summary>
    [Test]
    public void IndexerIntSet_IndexMoreThanSize_ThrowsIndexOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.Throws<IndexOutOfRangeException>(new(() => buffer[1] = 0));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_CapacityLessThanOne_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(new(() => new CircularBuffer<int>(0)));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_CapacityIsAtLeastOne_SetsCapacity()
    {
        var buffer = new CircularBuffer<int>(5);
        Assert.AreEqual(5, buffer.Capacity);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_SpecifyNoInitialCollection_BufferIsEmpty()
    {
        var buffer = new CircularBuffer<int>(5);
        Assert.AreEqual(0, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_InitialCollectionLargerThanCapacity_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(new(() => new CircularBuffer<int>(1, new[] { 1, 2 })));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_SpecifyInitialCollection_BufferSizeIsInitialCollectionSize()
    {
        var buffer = new CircularBuffer<int>(5, new[] { 1, 2, 3 });
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}(int, IEnumerable{T})"/>.</summary>
    [Test]
    public void ConstructorIntIEnumerable_SpecifyInitialCollection_BufferContainsInitialCollection()
    {
        var buffer = new CircularBuffer<int>(5, new[] { 1, 2, 3 });
        Assert.AreEqual(1, buffer[0]);
        Assert.AreEqual(2, buffer[1]);
        Assert.AreEqual(3, buffer[2]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushFront(T)"/>.</summary>
    [Test]
    public void PushFront_BufferIsFull_OverridesLastElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        Assert.AreEqual(10, buffer[0]);
        Assert.AreEqual(1, buffer[1]);
        Assert.AreEqual(2, buffer[2]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushFront(T)"/>.</summary>
    [Test]
    public void PushFront_BufferIsFull_SizeRemainsCorrect()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushFront(T)"/>.</summary>
    [Test]
    public void PushFront_BufferIsNotFull_AddsElementToTheFront()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushFront(10);
        Assert.AreEqual(10, buffer[0]);
        Assert.AreEqual(1, buffer[1]);
        Assert.AreEqual(2, buffer[2]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushFront(T)"/>.</summary>
    [Test]
    public void PushFront_BufferIsNotFull_IncrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushFront(10);
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushBack(T)"/>.</summary>
    [Test]
    public void PushBack_BufferIsFull_OverridesFirstElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushBack(10);
        Assert.AreEqual(2, buffer[0]);
        Assert.AreEqual(3, buffer[1]);
        Assert.AreEqual(10, buffer[2]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushBack(T)"/>.</summary>
    [Test]
    public void PushBack_BufferIsFull_SizeRemainsCorrect()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushBack(10);
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushBack(T)"/>.</summary>
    [Test]
    public void PushBack_BufferIsNotFull_AddsElementToTheBack()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushBack(10);
        Assert.AreEqual(1, buffer[0]);
        Assert.AreEqual(2, buffer[1]);
        Assert.AreEqual(10, buffer[2]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PushBack(T)"/>.</summary>
    [Test]
    public void PushBack_BufferIsNotFull_IncrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushBack(10);
        Assert.AreEqual(3, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopFront()"/>.</summary>
    [Test]
    public void PopFront_BufferIsEmpty_ThrowsInvalidOperationException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.Throws<InvalidOperationException>(new(() => buffer.PopFront()));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopFront()"/>.</summary>
    [Test]
    public void PopFront_BufferIsNotEmpty_RemovesTheFirstElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopFront();
        Assert.AreEqual(2, buffer[0]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopFront()"/>.</summary>
    [Test]
    public void PopFront_BufferIsNotEmpty_DecrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopFront();
        Assert.AreEqual(1, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopBack()"/>.</summary>
    [Test]
    public void PopBack_BufferIsEmpty_ThrowsInvalidOperationException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.Throws<InvalidOperationException>(new(() => buffer.PopBack()));
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopBack()"/>.</summary>
    [Test]
    public void PopBack_BufferIsNotEmpty_RemovesTheLastElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopBack();
        Assert.AreEqual(1, buffer[0]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.PopBack()"/>.</summary>
    [Test]
    public void PopBack_BufferIsNotEmpty_DecrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopBack();
        Assert.AreEqual(1, buffer.Size);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.Clear()"/>.</summary>
    [Test]
    public void Clear_BufferIsCleared()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.Clear();
        Assert.AreEqual(0, buffer.Size);
        Assert.AreEqual(3, buffer.Capacity);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.ToArray()"/>.</summary>
    [Test]
    public void ToArray_BufferIsEmpty_ArrayIsEmpty()
    {
        var buffer = new CircularBuffer<int>(3);
        var array = buffer.ToArray();
        Assert.AreEqual(0, array.Length);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.ToArray()"/>.</summary>
    [Test]
    public void ToArray_BufferContainsElements_ArrayContainsCorrectElements()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        var array = buffer.ToArray();
        Assert.AreEqual(1, array[0]);
        Assert.AreEqual(2, array[1]);
    }

    /// <summary>Tests <see cref="CircularBuffer{T}.ToArray()"/>.</summary>
    [Test]
    public void ToArray_BufferContainsElementsThatWrapAroundToFrontOfInternalArray_ArrayContainsCorrectElements()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        var array = buffer.ToArray();
        Assert.AreEqual(10, array[0]);
        Assert.AreEqual(1, array[1]);
        Assert.AreEqual(2, array[2]);
    }
}
