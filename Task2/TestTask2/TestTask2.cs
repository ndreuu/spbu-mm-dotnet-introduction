using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Task2;

namespace TestTask2;

public class Tests
{
	private class MyTest<TResult> {
		internal TResult Expected { get; }
		internal Func<TResult> Function => _func;
		private Func<TResult> _func;

		public MyTest(TResult expected, Func<TResult> func) {
			Expected = expected;
			_func = func;
		}
	}

	[SetUp]
	public void Setup()
	{
	}
	       
	[Test]
	public void EnqueueOneTask() {
		var task = new MyTask<int>(() => {
			Thread.Sleep(100);
			return 2 * 2;
		});
		var threadPool = new MyThreadPool(4);

		threadPool.Enqueue(task);

		Assert.AreEqual(4, task.Result);
            
		threadPool.Dispose();
	}
	
	[Test]
	public void EnqueueManyTasks() {
		var threadPool = new MyThreadPool(4);
		var tasks = new List<MyTask<int>>();
		var multiplicationTests = new List<MyTest<int>>();

		for (int i = 0; i < 20; i++) {
			var currNum = i;
			multiplicationTests.Add(new MyTest<int>(currNum * currNum
				, () => {
					Thread.Sleep(100);
					return currNum * currNum;
				})
			);

			var task = new MyTask<int>(multiplicationTests[i].Function);
                
			tasks.Add(task);

			threadPool.Enqueue(task);
		}

		for (int i = 0; i < 20; i++) {
			Assert.AreEqual(multiplicationTests[i].Expected, tasks[i].Result);
			Assert.True(tasks[i].IsCompleted);
		}
		threadPool.Dispose();
	}

	
	[Test]
	public void ManyThreads() {
		var threadPool = new MyThreadPool(100);
		var tasks = new List<MyTask<int>>();
		var multiplicationTests = new List<MyTest<int>>();

		for (int i = 0; i < 10; i++) {
			var currNum = i;
			multiplicationTests.Add(new MyTest<int>(currNum * currNum
				, () => {
					Thread.Sleep(100);
					return currNum * currNum;
				})
			);

			var task = new MyTask<int>(multiplicationTests[i].Function);
                
			tasks.Add(task);

			threadPool.Enqueue(task);
		}

		for (int i = 0; i < 10; i++) {
			Assert.AreEqual(multiplicationTests[i].Expected, tasks[i].Result);
		}
		threadPool.Dispose();
	}


	[Test]
	public void ManyTasks() {
		var threadPool = new MyThreadPool(10);
		var tasks = new List<MyTask<int>>();
		var multiplicationTests = new List<MyTest<int>>();

		for (int i = 0; i < 100; i++) {
			var currNum = i;
			multiplicationTests.Add(new MyTest<int>(currNum * currNum
				, () => {
					Thread.Sleep(100);
					return currNum * currNum;
				})
			);

			var task = new MyTask<int>(multiplicationTests[i].Function);
                
			tasks.Add(task);

			threadPool.Enqueue(task);
		}

		for (int i = 0; i < 100; i++) {
			Assert.AreEqual(multiplicationTests[i].Expected, tasks[i].Result);
		}
		threadPool.Dispose();
	}

	
	[Test]
	public void EnqueueOneTaskWithContinuation() {
		var task = new MyTask<int>(() => {
			Thread.Sleep(100);
			return 2 * 2;
		});
		var contTask = task.ContinueWith(x => x + x);
		var threadPool = new MyThreadPool(4);

		threadPool.Enqueue(task);

		Assert.AreEqual(8, contTask.Result);
		threadPool.Dispose();
	}

	[Test]
	public void EnqueueOneTaskAndManyContinuations() {
		var threadPool = new MyThreadPool(4);
		var task = new MyTask<int>(() => {
			Thread.Sleep(100);
			return 2 * 2;
		});
		var contTask1 = task.ContinueWith(x => x * 2);
		var contTask2 = contTask1.ContinueWith(x => x + 1);

		threadPool.Enqueue(task);

		Assert.AreEqual(4, task.Result);
		Assert.AreEqual(8, contTask1.Result);
		Assert.AreEqual(9, contTask2.Result);
		threadPool.Dispose();
	}

	[Test]
	public void EnqueueTaskWhenPoolDisposed() {
		var threadPool = new MyThreadPool(4);
            
		var task = new MyTask<int>(() => {
			Thread.Sleep(100);
			return 2 * 2;
		});

		threadPool.Dispose();

		Assert.Throws<ObjectDisposedException>(() => threadPool.Enqueue(task));
	}
	
	[Test]
	public void TestThreadCount()
	{
		int expected = 4;
		HashSet<int> actualThreads = new();
		var thread = new MyThreadPool(expected);
		int n = 10;
		List<IMyTask<int>> functions = new List<IMyTask<int>>();
		
		for (int i = 0; i < n; i++)
		{
			Func<int> func = () =>
			{
				Thread.Sleep(100);
				return Thread.CurrentThread.ManagedThreadId;
			};
			IMyTask<int> task = new MyTask<int>(func);
			functions.Add(task);
			thread.Enqueue(task);
		}

		for (int i = 0; i < n; i++)
		{
			actualThreads.Add(functions[i].Result);
		}
	
		thread.Dispose();

		Assert.AreEqual(expected, actualThreads.Count);
	}

	[Test]
	public void TaskWhichThrowsException()
	{
		var thread = new MyThreadPool(4);

		var task = new MyTask<int>(() =>
		{
			Thread.Sleep(100);
			throw new Exception("Exception");
		});
		
		thread.Enqueue(task);
		
		Assert.Throws<AggregateException>(() =>
		{
			var _ = task.Result;
		});

		thread.Dispose();
	}
	
}