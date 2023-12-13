namespace Task2;

public interface IMyTask
{
	public bool IsCompleted { get; }
	public bool HasContinuationTask { get; }

	public IMyTask ContinuationTask { get; }

	public void Execute();               
}

public interface IMyTask<TResult> : IMyTask {
	public TResult Result { get; }

	public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> task);
}