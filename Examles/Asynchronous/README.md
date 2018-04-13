# Asynchronous programming	

### Table of Contents
1. [Task creation and running](#task-creation-and-running)

## Task creation and running 	

### From constructor argument:

Using Delegate:
```csharp
Task task = new Task(delegate { Logger.Write("Task was created using constructor (Delegate)"); }) 

```

Using Lambda:
```csharp
Task task = new Task(() => Logger.Write("Task was created using constructor (Lambda)")) 

```

Running:
```csharp
task.Start();
```	

#### Pay attention:

It's not recommended to create a `Task` from constructor without a compelling reason. `TPL` takes a lot of care with synchronization and concurrency. It hides those complexity from client code. If a developer constructs a `Task` and `Starts` it, he need to think about synchronization and concurrency which obviously hard to handle.

### Task Factory 

```csharp
Task.Factory.StartNew(() => Logger.Write("Task was created using Task.Factory.StartNew method"));

```

Calling StartNew is functionally equivalent to creating a task by using one of its constructors, and then calling the `Task.Start` method to schedule the task for execution.

### Task.Run

```csharp
Task.Run(() => Logger.Write("Task was created using Task.Run method"));

```

The Run method was added in `.Net 4.5` to help with the increasingly frequent usage of async. This method allows developes to create and execute a task in a single method call and is a simpler alternative to the `StartNew` method. 

It creates a task with the following default values:
* Its cancellation token is CancellationToken.None.
* Its CreationOptions property value is TaskCreationOptions.DenyChildAttach.
* It uses the default task scheduler.

In other words `Task.Run` is a shorthand for `Task.Factory.StartNew` with specific safe arguments:
```csharp
Task.Factory.StartNew(
	action,
	CancellationToken.None, 
	TaskCreationOptions.DenyChildAttach, 
	TaskScheduler.Default
);
```