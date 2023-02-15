namespace NovaEngine.Tests.DataStructures;

/// <summary>The <see cref="CircularBuffer{T}"/> tests.</summary>
internal class CircularBufferTests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void Size_BufferContainsLessElementsThanCapacity_ReturnsNumberOfElementsInBuffer()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.That(buffer.Size, Is.EqualTo(2));
    }

    [Test]
    public void Size_BufferIsFull_ReturnsTheCapacity()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void IsFull_BufferIsFull_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        Assert.That(buffer.IsFull, Is.True);
    }

    [Test]
    public void IsFull_BufferIsNotFull_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.That(buffer.IsFull, Is.False);
    }

    [Test]
    public void IsFull_BufferWasFullButAnElementWasPopped_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PopFront();
        Assert.That(buffer.IsFull, Is.False);
    }

    [Test]
    public void IsEmpty_BufferIsEmpty_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.That(buffer.IsEmpty, Is.True);
    }

    [Test]
    public void IsEmpty_BufferIsNotEmpty_ReturnsFalse()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.That(buffer.IsEmpty, Is.False);
    }

    [Test]
    public void IsEmpty_BufferContainedAnElementThatWasPopped_ReturnsTrue()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        buffer.PopFront();
        Assert.That(buffer.IsEmpty, Is.True);
    }

    [Test]
    public void IndexerIntGet_BufferIsEmpty_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.That(() => _ = buffer[0], Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void IndexerIntGet_IndexBetweenZeroAndSize_ReturnsElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(1));
            Assert.That(buffer[1], Is.EqualTo(2));
        });
    }

    [Test]
    public void IndexerIntGet_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.That(() => _ = buffer[-1], Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void IndexerIntGet_IndexMoreThanSize_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.That(() => _ = buffer[1], Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void IndexerIntSet_BufferIsEmpty_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.That(() => buffer[0] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void IndexerIntSet_IndexBetweenZeroAndSize_SetsElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer[0] = 10;
        buffer[1] = 11;
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(10));
            Assert.That(buffer[1], Is.EqualTo(11));
        });
    }

    [Test]
    public void IndexerIntSet_IndexLessThanZero_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.That(() => buffer[-1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void IndexerIntSet_IndexMoreThanSize_ThrowsArgumentOutOfRangeException()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1 });
        Assert.That(() => buffer[1] = 0, Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void ConstructorIntIEnumerable_CapacityLessThanOne_ThrowsArgumentOutOfRangeException()
    {
        Assert.That(() => new CircularBuffer<int>(0), Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void ConstructorIntIEnumerable_CapacityIsAtLeastOne_SetsCapacity()
    {
        var buffer = new CircularBuffer<int>(5);
        Assert.That(buffer.Capacity, Is.EqualTo(5));
    }

    [Test]
    public void ConstructorIntIEnumerable_SpecifyNoInitialCollection_BufferIsEmpty()
    {
        var buffer = new CircularBuffer<int>(5);
        Assert.That(buffer.Size, Is.Zero);
    }

    [Test]
    public void ConstructorIntIEnumerable_InitialCollectionLargerThanCapacity_ThrowsArgumentException()
    {
        Assert.That(() => new CircularBuffer<int>(1, new[] { 1, 2 }), Throws.InstanceOf<ArgumentException>());
    }

    [Test]
    public void ConstructorIntIEnumerable_SpecifyInitialCollection_BufferSizeIsInitialCollectionSize()
    {
        var buffer = new CircularBuffer<int>(5, new[] { 1, 2, 3 });
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void ConstructorIntIEnumerable_SpecifyInitialCollection_BufferContainsInitialCollection()
    {
        var buffer = new CircularBuffer<int>(5, new[] { 1, 2, 3 });
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(1));
            Assert.That(buffer[1], Is.EqualTo(2));
            Assert.That(buffer[2], Is.EqualTo(3));
        });
    }

    [Test]
    public void PushFront_BufferIsFull_OverridesLastElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(10));
            Assert.That(buffer[1], Is.EqualTo(1));
            Assert.That(buffer[2], Is.EqualTo(2));
        });
    }

    [Test]
    public void PushFront_BufferIsFull_SizeRemainsCorrect()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void PushFront_BufferIsNotFull_AddsElementToTheFront()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushFront(10);

        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(10));
            Assert.That(buffer[1], Is.EqualTo(1));
            Assert.That(buffer[2], Is.EqualTo(2));
        });
    }

    [Test]
    public void PushFront_BufferIsNotFull_IncrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushFront(10);
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void PushBack_BufferIsFull_OverridesFirstElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushBack(10);
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(2));
            Assert.That(buffer[1], Is.EqualTo(3));
            Assert.That(buffer[2], Is.EqualTo(10));
        });
    }

    [Test]
    public void PushBack_BufferIsFull_SizeRemainsCorrect()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushBack(10);
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void PushBack_BufferIsNotFull_AddsElementToTheBack()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushBack(10);
        Assert.Multiple(() =>
        {
            Assert.That(buffer[0], Is.EqualTo(1));
            Assert.That(buffer[1], Is.EqualTo(2));
            Assert.That(buffer[2], Is.EqualTo(10));
        });
    }

    [Test]
    public void PushBack_BufferIsNotFull_IncrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PushBack(10);
        Assert.That(buffer.Size, Is.EqualTo(3));
    }

    [Test]
    public void PopFront_BufferIsEmpty_ThrowsInvalidOperationException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.That(() => buffer.PopFront(), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void PopFront_BufferIsNotEmpty_RemovesTheFirstElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopFront();
        Assert.That(buffer[0], Is.EqualTo(2));
    }

    [Test]
    public void PopFront_BufferIsNotEmpty_ReturnsPoppedElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        var value = buffer.PopFront();
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void PopFront_BufferIsNotEmpty_DecrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopFront();
        Assert.That(buffer.Size, Is.EqualTo(1));
    }

    [Test]
    public void PopBack_BufferIsEmpty_ThrowsInvalidOperationException()
    {
        var buffer = new CircularBuffer<int>(3);
        Assert.That(() => buffer.PopBack(), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void PopBack_BufferIsNotEmpty_RemovesTheLastElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopBack();
        Assert.That(buffer[0], Is.EqualTo(1));
    }

    [Test]
    public void PopBack_BufferIsNotEmpty_ReturnsPoppedElement()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        var value = buffer.PopBack();
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void PopBack_BufferIsNotEmpty_DecrementsSize()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.PopBack();
        Assert.That(buffer.Size, Is.EqualTo(1));
    }

    [Test]
    public void Clear_BufferIsCleared()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        buffer.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(buffer.Size, Is.EqualTo(0));
            Assert.That(buffer.Capacity, Is.EqualTo(3));
        });
    }

    [Test]
    public void ToArray_BufferIsEmpty_ArrayIsEmpty()
    {
        var buffer = new CircularBuffer<int>(3);
        var array = buffer.ToArray();
        Assert.That(array.Length, Is.EqualTo(0));
    }

    [Test]
    public void ToArray_BufferContainsElements_ArrayContainsCorrectElements()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2 });
        var array = buffer.ToArray();
        Assert.Multiple(() =>
        {
            Assert.That(array[0], Is.EqualTo(1));
            Assert.That(array[1], Is.EqualTo(2));
        });
    }

    [Test]
    public void ToArray_BufferContainsElementsThatWrapAroundToFrontOfInternalArray_ArrayContainsCorrectElements()
    {
        var buffer = new CircularBuffer<int>(3, new[] { 1, 2, 3 });
        buffer.PushFront(10);
        var array = buffer.ToArray();
        Assert.Multiple(() =>
        {
            Assert.That(array[0], Is.EqualTo(10));
            Assert.That(array[1], Is.EqualTo(1));
            Assert.That(array[2], Is.EqualTo(2));
        });
    }
}
