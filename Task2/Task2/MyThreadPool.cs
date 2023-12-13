using Task2;

public class MyThreadPool: IDisposable {
	private object _myLock = new();
	private Queue<IMyTask> _tasks;
	private CancellationTokenSource _cancelTokenSource;
	private CancellationToken _cancelToken;

	public MyThreadPool(int threadsNum) {
		_cancelTokenSource = new CancellationTokenSource();
		_cancelToken = _cancelTokenSource.Token;
		_tasks = new Queue<IMyTask>();
		threadsNum = threadsNum < 1 ? 1 : threadsNum;

		for (int i = 0; i < threadsNum; i++) {
			var thread = new Thread(() => {
				while (true)
				{
					IMyTask? task;
					lock (_myLock)
					{
						while (_tasks.Count == 0)
						{
							if (_cancelToken.IsCancellationRequested) return;
							Monitor.Wait(_myLock);
						}
						_tasks.TryDequeue(out task);
					}

					if (task is null) continue;
					task.Execute();
					if (task.HasContinuationTask)
					{
						Enqueue(task.ContinuationTask);
					}
				}
			});
                
			thread.Start();
		}
	}

	public void Enqueue(IMyTask task) {
		if (task == null) {
			throw new ArgumentNullException("Task must be not a null");
		}
            
		lock (_myLock) {
			if (_cancelToken.IsCancellationRequested) {
				throw new ObjectDisposedException("ThreadPool is disposed");
			}

			_tasks.Enqueue(task);
			Monitor.PulseAll(_myLock);
		}
	}

	private void Dispose(bool isDispose) {
		lock (_myLock) {
			if (!_cancelToken.IsCancellationRequested) {
				if (isDispose) {
					Monitor.PulseAll(_myLock);
				}

				_cancelTokenSource.Cancel();
			}
		}
	}

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

}