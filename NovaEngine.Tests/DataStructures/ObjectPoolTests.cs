namespace NovaEngine.Tests.DataStructures;

/// <summary>The <see cref="ObjectPool{T}"/> tests.</summary>
public class ObjectPoolTests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void AvailableCount_PoolIsEmpty_ReturnsZero()
    {
        using var pool = new ObjectPool<GameObject>(() => null);
        Assert.That(pool.AvailableCount, Is.EqualTo(0));
    }

    [Test]
    public void AvailableCount_PoolContainsObject_ReturnsOne()
    {
        using var gameObject = new GameObject("");
        using var pool = new ObjectPool<GameObject>(() => null);
        pool.ReturnObject(gameObject);
        Assert.That(pool.AvailableCount, Is.EqualTo(1));
    }

    [Test]
    public void GetObject_AvailableCountIsZero_CallsInstantiateObjectFunction()
    {
        var wasInstantiateFunctionCalled = false;

        using var pool = new ObjectPool<GameObject>(() =>
        {
            wasInstantiateFunctionCalled = true;
            return new("");
        });
        using var retrievedGameObject = pool.GetObject();

        Assert.That(wasInstantiateFunctionCalled, Is.True);
    }

    [Test]
    public void GetObject_AvailableCountMoreThanZero_DoesntCallInstantiateObjectFunction()
    {
        using var gameObject = new GameObject("");
        var wasInstantiateFunctionCalled = false;

        using var pool = new ObjectPool<GameObject>(() =>
        {
            wasInstantiateFunctionCalled = true;
            return new("");
        });
        pool.ReturnObject(gameObject);
        using var retrievedGameObject = pool.GetObject();
        
        Assert.That(wasInstantiateFunctionCalled, Is.False);
    }

    [Test]
    public void GetObject_InstantiateObjectFunctionReturnsNull_ThrowsInvalidOperationException()
    {
        using var pool = new ObjectPool<GameObject>(() => null);
        Assert.That(pool.GetObject, Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void ReturnObject_CallsResetObjectAction()
    {
        var wasResetActionCalled = false;

        using var pool = new ObjectPool<GameObject>(() => new(""), (_) => wasResetActionCalled = true);
        using var retrievedGameObject = pool.GetObject();
        pool.ReturnObject(retrievedGameObject);

        Assert.That(wasResetActionCalled, Is.True);
    }
}
