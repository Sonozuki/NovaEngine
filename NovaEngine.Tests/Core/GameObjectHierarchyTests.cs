namespace NovaEngine.Tests.Core;

/// <summary>The <see cref="GameObject"/> hierarchy tests.</summary>
public class GameObjectHierarchyTests
{
    [Test]
    public void GameObjectParent_AddsGameObjectToChildrenOfParent()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        child.Parent = parent;

        Assert.That(parent.Children, Contains.Item(child));
    }

    [Test]
    public void GameObjectParent_GameObjectIsRemovedFromChildrenOfOldParentAndAddedToChildrenOfNewParent()
    {
        using var oldParent = new GameObject("old parent");
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        child.Parent = oldParent;

        Assert.That(oldParent.Children, Contains.Item(child));

        child.Parent = parent;

        Assert.Multiple(() =>
        {
            Assert.That(oldParent.Children, Has.Count.Zero);
            Assert.That(parent.Children, Contains.Item(child));
        });
    }

    [Test]
    public void GameObjectChildrenAdd_SetsParentOfChild()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Add(child);

        Assert.That(child.Parent, Is.SameAs(parent));
    }

    [Test]
    public void GameObjectChildrenInsert_SetsParentOfChild()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Insert(0, child);

        Assert.That(child.Parent, Is.SameAs(parent));
    }

    [Test]
    public void GameObjectChildrenRemove_UnsetsParentOfChild()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Add(child);

        Assert.That(child.Parent, Is.SameAs(parent));

        parent.Children.Remove(child);

        Assert.That(child.Parent, Is.Null);
    }

    [Test]
    public void GameObjectChildrenRemoveAt_UnsetsParentOfChild()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Add(child);

        Assert.That(child.Parent, Is.SameAs(parent));

        parent.Children.RemoveAt(0);

        Assert.That(child.Parent, Is.Null);
    }

    [Test]
    public void GameObjectChildrenIndexer_UnsetsParentOfOldChildAndSetsParentOfNewChild()
    {
        using var parent = new GameObject("parent");
        using var oldChild = new GameObject("old child");
        using var child = new GameObject("child");

        parent.Children.Add(oldChild);

        Assert.That(oldChild.Parent, Is.SameAs(parent));

        parent.Children[0] = child;

        Assert.Multiple(() =>
        {
            Assert.That(oldChild.Parent, Is.Null);
            Assert.That(child.Parent, Is.SameAs(parent));
        });
    }

    [Test]
    public void GameObjectChildrenClear_UnsetsParentFromAllChildren()
    {
        using var parent = new GameObject("parent");
        using var child1 = new GameObject("child1");
        using var child2 = new GameObject("child2");

        parent.Children.Add(child1);
        parent.Children.Add(child2);

        Assert.Multiple(() =>
        {
            Assert.That(child1.Parent, Is.SameAs(parent));
            Assert.That(child2.Parent, Is.SameAs(parent));
        });

        parent.Children.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(child1.Parent, Is.Null);
            Assert.That(child2.Parent, Is.Null);
        });
    }

    [Test]
    public void GameObjectParent_SetParentOfParentToChild_ThrowsInvalidOperationException()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        child.Parent = parent;
        Assert.That(() => parent.Parent = child, Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void GameObjectChildrenAdd_AddParentAsChildOnChild_ThrowsInvalidOperationException()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Add(child);
        Assert.That(() => child.Children.Add(parent), Throws.InstanceOf<InvalidOperationException>());
    }

    [Test]
    public void GameObjectChildrenInsert_InsertChildParentAsChild_ThrowsInvalidOperationException()
    {
        using var parent = new GameObject("parent");
        using var child = new GameObject("child");

        parent.Children.Add(child);
        Assert.That(() => child.Children.Insert(0, parent), Throws.InstanceOf<InvalidOperationException>());
    }
}
