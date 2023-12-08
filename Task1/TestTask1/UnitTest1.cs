using System;
using NUnit.Framework;
using Task1;
namespace TestTask1;

class A
{
	public B[] Bs { get; set; }
}

class B
{
	public C[] Cs { get; set; }
}

class C
{
	public int[] D { get; set; }
}

public class Tests
{
	[SetUp]
	public void Setup()
	{

	}

	
	[Test]
	public void Test1()
	{
		var obj = new A
		{
			Bs = new[]
			{
				new B
				{
					Cs = new[]
					{
						new C { D = new[] { 42, 11 } },
						new C { D = new[] { 50 } }
					}
				}
			}
		};
		var getter = MyClass.CreateGetter<A, int>("Bs[0].Cs[0].D[1]");
		var result = getter(obj);
		
		Assert.AreEqual(result, obj.Bs[0].Cs[0].D[1]);
	}

	[Test]
	public void Test2()
	{
		var obj = new A
		{
			Bs = new[]
			{
				new B
				{
					Cs = new[]
					{
						new C { D = new[] { 42, 11 } },
						new C { D = new[] { 50 } }
					}
				}
			}
		};
		var getter = MyClass.CreateGetter<A, int>("Bs[0].Cs[1].D[0]");
		var result = getter(obj);
		
		Assert.AreEqual(result, obj.Bs[0].Cs[1].D[0]);
	}
	
	[Test]
	public void Test3()
	{
		var obj = new A
		{
			Bs = new[]
			{
				new B
				{
					Cs = new[]
					{
						new C { D = new[] { 42, 11 } },
						new C { D = new[] { 50 } }
					}
				},
				new B
				{
					Cs = new[]
					{
						new C { D = new[] { 52, 111 } },
						new C { D = new[] { 59, 111 } },
						new C { D = new[] { 60, 1 } }
					}
				}
			}
		};
		var getter = MyClass.CreateGetter<A, int>("Bs[1].Cs[2].D[1]");
		var result = getter(obj);
		
		Assert.AreEqual(result, obj.Bs[1].Cs[2].D[1]);
	}
	
}