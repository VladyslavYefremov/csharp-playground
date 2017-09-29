# Events

### [Inheritance.cs](Inheritance.cs)

The following block of code causes the compilation error: `OnUpdate can only appear on the left side of -= and += operators`

```csharp
class Subject
{
    public event EventHandler OnUpdate;

    public virtual void PublishEvent()
    {
        Console.WriteLine("Subject publishes an event");
        OnUpdate?.Invoke(this, null);
    }
}

class DerivedSubject : Subject
{
    public override void PublishEvent()
    {
        Console.WriteLine("DerivedSubject publishes an event");
        OnUpdate?.Invoke(this, null); // compilation error
    }
}
public void Run()
{
    Subject subject = new DerivedSubject();

    subject.OnUpdate += () => Console.WriteLine("A subject was updated");

    subject.PublishEvent();
}
```

The reason is that the event:

```csharp
public event EventHandler OnUpdate;
```

Actually turns into something like:

```csharp
private EventHandler onUpdateDelegate;

public event EventHandler OnUpdate
{
    add { onUpdateDelegate += value; }
    remove { onUpdateDelegate -= value; }
}
```

That is why derived class cannot directly access to `Invoke` method of event delegate. 

For ability to `Invoke` event from derived classes `Subject` class must declare following method:
```csharp
protected virtual void Update(object sender, EventArgs e)
{
    var handler = OnUpdate;
    handler?.Invoke(this, e);
}
```

The complete example is in [Inheritance.cs](Inheritance.cs) file.