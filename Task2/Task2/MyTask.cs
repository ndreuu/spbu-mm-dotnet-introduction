using Task2;

public class MyTask<TResult> : IMyTask<TResult> {
	private volatile Exception _taskExecutionException;
	private Func<TResult> _taskFunc;

	public bool HasContinuationTask => ContinuationTask != null;
	private bool HasFailed => _taskExecutionException != null;

	public bool IsCompleted => _isCompleted;
	private volatile bool _isCompleted;

	public TResult Result {
		get {
			while (!_isCompleted && !HasFailed) { }

			if (HasFailed) {
				throw new AggregateException(_taskExecutionException);
			}

			return _result;
		}
	}
	
	private TResult _result;

	public IMyTask ContinuationTask => _continuationTask;
	private IMyTask _continuationTask;

	public MyTask(Func<TResult> taskFunc) {
		_taskFunc = taskFunc;
	}

	public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> continuation) {
		var nextTask = new MyTask<TNewResult>(() => continuation.Invoke(Result));
		_continuationTask = nextTask;
		return nextTask;
	}

	public void Execute() {
		try {
			TResult result = _taskFunc.Invoke();
			_result = result;
			_isCompleted = true;
		}
		catch (Exception e) {
			_taskExecutionException = e;
		}
	}
}